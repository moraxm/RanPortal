using UnityEngine;
using System.Collections;

public class LaneObject : MonoBehaviour 
{
    public enum LanePosition
    {
        LEFT,
        CENTER,
        RIGHT,
    }

	// Use this for initialization
	protected virtual void Start () {
        m_lane = LanePosition.CENTER;
	}

    protected LanePosition m_lane;
    public virtual LanePosition lane
    {
        set
        {
            m_lane = value;
            Vector3 pos = transform.position;
            switch (m_lane)
            {
                case LanePosition.LEFT:
                    pos.x = GameManager.instance.m_obstacleGenerator.spawmPositionLeft.transform.position.x;
                    break;
                case LanePosition.CENTER:
                    pos.x = GameManager.instance.m_obstacleGenerator.spawmPositionCenter.transform.position.x;
                    break;
                case LanePosition.RIGHT:
                    pos.x = GameManager.instance.m_obstacleGenerator.spawmPositionRight.transform.position.x;
                    break;
                default:
                    break;
            }
            transform.position = pos;
        }
        get
        {
            return m_lane;
        }
    }
}
