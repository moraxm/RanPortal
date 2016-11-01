using UnityEngine;
using System.Collections;

public class PortalsTriggerStart : MonoBehaviour
{
    public ObstacleGenerator m_obstacleGenerator;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Portal p = collision.GetComponent<Portal>();
        if (!p) return;

        m_obstacleGenerator.OnTriggerPortalCreated(p);
    }
}
