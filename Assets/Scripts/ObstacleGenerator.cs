﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour
{
    public ISpeedSource speedSource;
    public float waveSpace = 3;
    [Range(0, 100)]
    public int changePortalProbability;

    // Base objects for obstacles and portals
    public Obstacle[] obstacles;
    public Obstacle bonusObstacle;
    public Obstacle bonusCoin;
    [Range(0, 100)]
    public float bonusCoinProbability;
    bool m_bonusCoinFlag;

    // Random portals
    Portal m_randomPortal; // Portal without next portal that we have to find a next portal reference

    // Spawn positions. Lanes
    public Transform spawmPositionLeft;
    public Transform spawmPositionCenter;
    public Transform spawmPositionRight;
    public Transform bonusSpawmPosition;

    // For debug porpuse
    public Transform poolPosition;
    Transform m_objectPoolGO;
    Transform m_portalPoolGO;
    public int fixedObstacleIdx = -1;

    // Pools
    List<Obstacle> m_ObstaclesProbabilityList;
    Dictionary<Obstacle, List<PoolObject>> m_obstaclePool;
    class PoolObject
    {
        public PoolObject(Obstacle o)
        {
            obstacle = o;
            inUse = false;
        }
        public Obstacle obstacle;
        public bool inUse = false;
    }
    Dictionary<Obstacle, PoolObject> m_obstaclesInUse;

    public int m_maxObjectCount;

    float m_acumSpace = 0;
    private bool m_onBonus;

    // Use this for initialization
    void Start()
    {
        m_obstaclesInUse = new Dictionary<Obstacle, PoolObject>();
        m_obstaclePool = new Dictionary<Obstacle, List<PoolObject>>();
        m_ObstaclesProbabilityList = new List<Obstacle>();
        m_objectPoolGO = new GameObject().transform;
        m_objectPoolGO.name = "Object Pool";
        m_objectPoolGO.transform.SetParent(poolPosition.transform);
        m_objectPoolGO.transform.localPosition = Vector3.zero;

        if (fixedObstacleIdx > -1)
        {
            Obstacle o = obstacles[fixedObstacleIdx];
            m_ObstaclesProbabilityList.Add(o);
            GameObject currentObstacleGO = new GameObject();
            currentObstacleGO.name = o.gameObject.name;
            currentObstacleGO.transform.SetParent(m_objectPoolGO);
            currentObstacleGO.transform.localPosition = Vector3.zero;
            GameObject obs = InstantiateGameObject(o.gameObject, currentObstacleGO.transform);
            PoolObject pO = new PoolObject(obs.GetComponent<Obstacle>());
            List<PoolObject> list = new List<PoolObject>();
            list.Add(pO); // At least one element for each obstacle
            m_obstaclePool.Add(o, list);
        }
        else
        {
            foreach (Obstacle o in obstacles)
            {
                int probabilityLenght = Mathf.CeilToInt((float)o.probability * obstacles.Length / 100);
                for (int i = 0; i < probabilityLenght; ++i)
                {
                    m_ObstaclesProbabilityList.Add(o);
                }
                AddObstacleToPool(o);
            }

            // Bonus coin
            AddObstacleToPool(bonusCoin);
        }
    }

    void AddObstacleToPool(Obstacle o)
    {
        GameObject currentObstacleGO = new GameObject();
        currentObstacleGO.name = o.gameObject.name;
        currentObstacleGO.transform.SetParent(m_objectPoolGO);
        currentObstacleGO.transform.localPosition = Vector3.zero;
        GameObject obs = InstantiateGameObject(o.gameObject, currentObstacleGO.transform);
        PoolObject pO = new PoolObject(obs.GetComponent<Obstacle>());
        List<PoolObject> list = new List<PoolObject>();
        list.Add(pO); // At least one element for each obstacle
        m_obstaclePool.Add(o, list);
    }

    GameObject InstantiateGameObject(GameObject other, Transform parent)
    {
        GameObject obs = GameObject.Instantiate(other.gameObject);
        obs.transform.SetParent(parent.transform);
        obs.transform.localPosition = Vector3.zero;
        return obs;
    }

    // Update is called once per frame
    void Update()
    {
        m_acumSpace += Time.deltaTime * speedSource.speed;
        if (m_acumSpace > waveSpace)
        {
            m_acumSpace -= waveSpace;
            int lane = Random.Range(0, 3);
            int probability = Random.Range(0, 100);
            Obstacle obs;
            obs = GetObstacle(lane, probability);
            obs.speedSource = GameManager.instance;
            waveSpace = obs.size;
        }
    }

    private void SetObstaclePosition(Obstacle obstace, int lane)
    {
        switch (lane)
        {
            case 0:
                obstace.transform.position = spawmPositionLeft.position;
                obstace.laneObject.lane = LaneObject.LanePosition.LEFT;
                //obstace.transform.SetParent(spawmPositionLeft);
                break;
            case 1:
                obstace.transform.position = spawmPositionCenter.position;
                obstace.laneObject.lane = LaneObject.LanePosition.CENTER;
                //obstace.transform.SetParent(spawmPositionCenter);
                break;
            case 2:
                obstace.transform.position = spawmPositionRight.position;
                obstace.laneObject.lane = LaneObject.LanePosition.RIGHT;
                //obstace.transform.SetParent(spawmPositionRight);
                break;
            default:
                break;
        }
    }

    private PoolObject GetObstacleFromPool(Obstacle type)
    {
        List<PoolObject> currentObjects = m_obstaclePool[type];
        foreach (PoolObject pO in currentObjects)
        {
            if (!pO.inUse)
            {
                return pO;
            }
        }
        if (currentObjects.Count < m_maxObjectCount)
        {
            GameObject obs = InstantiateGameObject(type.gameObject, m_obstaclePool[type][0].obstacle.transform.parent);
            PoolObject newPO = new PoolObject(obs.GetComponent<Obstacle>());

            currentObjects.Add(newPO);
            return newPO;
        }
        else
        {
            // The pool is full. We can not create new objects
            return null;
        }
    }

    private Obstacle GetObstacle(int lane, int probability)
    {
        if (m_onBonus)
        {
            m_onBonus = false;
            SetObstaclePosition(bonusObstacle, lane);
            Vector3 pos = bonusObstacle.transform.position;
            pos.y = bonusSpawmPosition.transform.position.y;
            bonusObstacle.transform.position = pos;
            bonusObstacle.Reset();
            return bonusObstacle;
        }
        else
        {
            int idx = probability * m_ObstaclesProbabilityList.Count / 100;
            Obstacle obstacleType = m_ObstaclesProbabilityList[idx];
            if (m_bonusCoinFlag)
            {
                int r = Random.Range(0, 100);
                if (r < bonusCoinProbability)
                {
                    obstacleType = bonusCoin;
                    m_bonusCoinFlag = false;
                }
            }
            else
            {
                m_bonusCoinFlag = true;
            }
            PoolObject pO = GetObstacleFromPool(obstacleType);
            pO.obstacle.Reset();
            pO.inUse = true;
            if (m_obstaclesInUse.ContainsKey(pO.obstacle)) Debug.LogError("Algo ha ido mal");

            m_obstaclesInUse.Add(pO.obstacle, pO);
            // position
            SetObstaclePosition(pO.obstacle, lane);
            return pO.obstacle;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Set the obstacle to the pool again
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle == null) return;
        //obstacle.Reset();
        if (obstacle.dontDestroy) return;
        if (obstacle != bonusObstacle)
        {
            ReUseObstacle(obstacle);
        }
        else
        {
            m_onBonus = false;
            GameManager.instance.FinishedBonus();
        }
    }

    private void ReUseObstacle(Obstacle obstacle)
    {
        if (obstacle != null)
        {
            obstacle.speedSource = null;
            if (m_obstaclesInUse.ContainsKey(obstacle))
            {
                PoolObject pO = m_obstaclesInUse[obstacle];
                pO.inUse = false;
                m_obstaclesInUse.Remove(pO.obstacle);
            }
        }
    }

    public void Restart()
    {
        Obstacle[] obstacles = m_objectPoolGO.GetComponentsInChildren<Obstacle>();
        foreach (Obstacle o in obstacles)
        {
            ReUseObstacle(o);
			if (!o.dontDestroy)
        		SetObstaclePosition(o, 0);
        }
        m_bonusCoinFlag = true;
        m_onBonus = false;
    }

    public void OnBonus()
    {
        m_onBonus = true;
        // Hide other obstacles
        OnBonusExplosion();
    }

    private void OnBonusExplosion()
    {
        foreach (var a in m_obstaclesInUse)
        {
            if (a.Value.inUse)
            {
                a.Value.obstacle.hide = true;
            }
        }
        m_acumSpace = waveSpace;
    }

    // Called at the begining of the repawm obstacle, if the obstale is a portal.
    internal void OnTriggerPortalCreated(Portal p)
    {
        if (m_randomPortal == null)
        {
            if (p.maxNextPortalToTeletransport == -1) return; // If the portal has fixed next portal, we don´t have nothing to do here
            // At start, first case when ther is no portals and this is the first portal or when portal with fixed next
            // portal has been dectected.
            m_randomPortal = p;
        }
        else
        {
            // We have already a portal without next portal so we are going to update its reference
            m_randomPortal.nextPortal = p;
            if (p.maxNextPortalToTeletransport == -1)
            {
                m_randomPortal = null;
            }
            else
            {
                m_randomPortal = p;
            }
        }
        
    }
}
