using UnityEngine;
using System.Collections;

public class ObstacleSet : Obstacle 
{
    Portal[] m_portals;
    AutomoveObject[] children;
    public override void Awake()
    {
        base.Awake();
        children = GetComponentsInChildren<AutomoveObject>();
        foreach (AutomoveObject o in children)
        {
            o.dontDestroy = true;
        }
        dontDestroy = false;
        m_portals = GetComponentsInChildren<Portal>();
        foreach (Portal p in m_portals)
        {
            p.GetComponent<LaneObject>().AutoDetectLane();
        }
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

    public override bool hide
    {
        get
        {
            return base.hide;
        }
        set
        {
            foreach (Obstacle o in children)
            {
                if (o != this)
                    o.hide = value;
            }
        }
    }

}
