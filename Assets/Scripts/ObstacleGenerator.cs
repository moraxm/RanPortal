using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour
{

    public float speed = 10;
    public float timeStep = 1;

    // Base objects for obstacles and portals
    public Portal portal;
    public Obstacle[] obstacles;

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
        Debug.Log(m_acumTime);
        m_acumTime += Time.deltaTime;
        if (m_acumTime > timeStep)
        {
            m_acumTime -= timeStep;
            int lane = Random.Range(0, 3);
            Obstacle obs = GetObstacle(lane);
            obs.speed = speed;
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
        switch (lane)
        {
            case 0:
                toReturn.transform.position = spawmPositionLeft.position;
                toReturn.transform.SetParent(spawmPositionLeft);
                break;
            case 1:
                toReturn.transform.position = spawmPositionCenter.position;
                toReturn.transform.SetParent(spawmPositionCenter);
                break;
            case 2:
                toReturn.transform.position = spawmPositionRight.position;
                toReturn.transform.SetParent(spawmPositionRight);
                break;
            default:
                break;
        }

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

}
