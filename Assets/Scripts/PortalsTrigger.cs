using UnityEngine;
using System.Collections;

public class PortalsTrigger : MonoBehaviour 
{
    public ObstacleGenerator m_obstacleGenerator;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Portal p = collision.GetComponent<Portal>();
        if (!p) return;

        m_obstacleGenerator.OnTriggerPortal(p);
    }
}
