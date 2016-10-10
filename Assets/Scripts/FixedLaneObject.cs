using UnityEngine;
using System.Collections;

public class FixedLaneObject : LaneObject
{
    public LaneObject.LanePosition m_fixedPosition;

    public override LanePosition lane
    {
        get
        {
            return base.lane;
        }
        set
        {
            base.lane = m_fixedPosition;
        }
    }

    protected override void Start()
    {
        base.Start();
        m_lane = m_fixedPosition;
    }
}
