using UnityEngine;
using System.Collections;

public class ObstacleSet : Obstacle 
{
    public override void Awake()
    {
        base.Awake();
        AutomoveObject[] children = GetComponentsInChildren<AutomoveObject>();
        foreach (AutomoveObject o in children)
        {
            o.dontDestroy = true;
        }
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
