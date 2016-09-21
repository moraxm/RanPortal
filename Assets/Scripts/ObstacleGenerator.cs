using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour
{
    public float speed = 10;
    public float timeStep = 1;
    [Range(0,100)]
    public int portalProbability;
    [Range(0,100)]
    public int changePortalProbability;

    // Base objects for obstacles and portals
    public Portal portal;
    public Obstacle[] obstacles;
    public Portal m_randomPortal;

    // Spawn positions. Lanes
    public Transform spawmPositionLeft;
    public Transform spawmPositionCenter;
    public Transform spawmPositionRight;

    // For debug porpuse
    public Transform poolPosition;
    Transform m_objectPoolGO;
    Transform m_portalPoolGO;

    // Pools
    LinkedList<Obstacle> m_objectPool;
    public int m_maxObjectCount;
    Stack<Portal> m_portalPool;
    public int m_maxPortalCount;

    float m_acumTime = 0;
    
   

    // Use this for initialization
    void Start()
    {
        // Creación del pool de obstáculos
        m_objectPool = new LinkedList<Obstacle>();
        m_objectPoolGO = new GameObject().transform;
        m_objectPoolGO.name = "Object Pool";
        m_objectPoolGO.transform.SetParent(poolPosition.transform);
        m_objectPoolGO.transform.localPosition = Vector3.zero;
        for (int i = 0; i < m_maxObjectCount; ++i)
        {
            Obstacle obstacle = obstacles[Random.Range(0, obstacles.Length)];
            GameObject obs = GameObject.Instantiate(obstacle.gameObject);
            obs.transform.SetParent(m_objectPoolGO.transform);
            obs.transform.localPosition = Vector3.zero;

            m_objectPool.AddLast(obs.GetComponent<Obstacle>());
        }


        m_portalPool = new Stack<Portal>();
        m_portalPoolGO = new GameObject().transform;
        m_portalPoolGO.name = "Portal Pool";
        m_portalPoolGO.transform.SetParent(poolPosition.transform);
        m_portalPoolGO.transform.localPosition = Vector3.zero;
        for (int i = 0; i < m_maxPortalCount; ++i)
        {
            GameObject obs = GameObject.Instantiate(portal.gameObject);
            obs.transform.SetParent(m_portalPoolGO.transform);
            obs.transform.localPosition = Vector3.zero;
            m_portalPool.Push(obs.GetComponent<Portal>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_acumTime += Time.deltaTime;
        if (m_acumTime > timeStep)
        {
            m_acumTime -= timeStep;
            int lane = Random.Range(0, 3);
            int obstacleOrPortal = Random.Range(0, 100);
            Obstacle obs;
            if (obstacleOrPortal < portalProbability)
            {
                obs = GetPortal(lane);
                m_randomPortal = ChangeRandomPortal(obs as Portal);
            }
            else
            {
                obs = GetObstacle(lane);
            }
            obs.speed = speed;
        }
    }

    private Portal ChangeRandomPortal(Portal portalToChange)
    {
        if (m_randomPortal == null) return portalToChange;

        int changePortal = Random.Range(0, 100);
        if (changePortal < changePortalProbability)
        {
            return portalToChange;
        }
        else
        {
            return m_randomPortal;
        }
    }

    private Obstacle GetPortal(int lane)
    {
        Obstacle toReturn = m_portalPool.Pop();
        SetObstaclePosition(toReturn, lane);
        return toReturn;
    }

    private void SetObstaclePosition(Obstacle obstace, int lane)
    {
        switch (lane)
        {
            case 0:
                obstace.transform.position = spawmPositionLeft.position;
                obstace.transform.SetParent(spawmPositionLeft);
                break;
            case 1:
                obstace.transform.position = spawmPositionCenter.position;
                obstace.transform.SetParent(spawmPositionCenter);
                break;
            case 2:
                obstace.transform.position = spawmPositionRight.position;
                obstace.transform.SetParent(spawmPositionRight);
                break;
            default:
                break;
        }
    }

    private Obstacle GetObstacle(int lane)
    {
        Obstacle toReturn = null;
        int obstacleIdx = Random.Range(0, m_objectPool.Count);
        int i = 0;
        // Find the random obstacle in the list. O(N) but O(1) removing the element.
        foreach (Obstacle o in m_objectPool)
        {
            if (i == obstacleIdx)
            {
                toReturn = o;
                break;
            }
            ++i;
        }
        m_objectPool.Remove(toReturn);
        // TODO si el pool no tiene obstáculos y devuelve null

        // position
        SetObstaclePosition(toReturn, lane);

        return toReturn;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Set the obstacle to the pool again
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.speed = 0;
            if (obstacle is Portal)
            {

                m_portalPool.Push(obstacle as Portal);
                obstacle.transform.SetParent(m_portalPoolGO);
                obstacle.transform.localPosition = Vector3.zero;
            }
            else
            {
                m_objectPool.AddLast(obstacle);
                obstacle.transform.SetParent(m_objectPoolGO);
                obstacle.transform.localPosition = Vector3.zero;
            }
            
            
        }

    }

    internal Portal GetRandomPortal(Portal activePortal)
    {
        return m_randomPortal;
    }
}
