using UnityEngine;
using System.Collections;

public class ObstacleSet : Obstacle 
{
    Portal[] m_portals;
    public override void Awake()
    {
        base.Awake();
        AutomoveObject[] children = GetComponentsInChildren<AutomoveObject>();
        foreach (AutomoveObject o in children)
        {
            o.dontDestroy = true;
        }
        dontDestroy = false;
        m_portals = GetComponentsInChildren<Portal>();
    }

    public override Portal GetPortal()
    {
		if (m_portals == null || m_portals.Length == 0) return null;
        int idx = Random.Range(0, m_portals.Length);
        return m_portals[idx];
    }

    protected override void OnPlayerEnterFront(Collider2D collision)
    {
        // Do nothing
    }

    protected override void OnPlayerEnterLateral(Collider2D collision)
    {
        // Do nothing
    }

    protected override void OnPlayerExitFront(Collider2D collision)
    {
        // Do nothing
    }

    protected override void OnPlayerExitLateral(Collider2D collision)
    {
        // Do nothing
    }

}
