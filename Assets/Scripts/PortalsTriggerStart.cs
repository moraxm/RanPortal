using UnityEngine;
using System.Collections;

public class PortalsTriggerStart : PortalsTrigger {

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Portal p = collision.GetComponent<Portal>();
        if (!p) return;

        m_obstacleGenerator.OnTriggerPortalCreated(p);
    }
}
