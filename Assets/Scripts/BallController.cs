using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    public float moveDistance;

    enum LanePosition
    {
        LEFT,
        CENTER,
        RIGHT,
    }
    LanePosition m_lanePosition;

	// Use this for initialization
	void Start () 
    {
        m_lanePosition = LanePosition.CENTER;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void moveLeft()
    {
        Vector3 newPos = transform.position;
        switch (m_lanePosition)
        {
            case LanePosition.CENTER:
                newPos.x -= moveDistance;
                m_lanePosition = LanePosition.LEFT;
                break;
            case LanePosition.RIGHT:
                newPos.x -= moveDistance;
                m_lanePosition = LanePosition.CENTER;
                break;
            default:
                break;
        }
        transform.position = newPos;
    }

    public void moveRight()
    {
        Vector3 newPos = transform.position;
        switch (m_lanePosition)
        {
            case LanePosition.CENTER:
                newPos.x += moveDistance;
                m_lanePosition = LanePosition.RIGHT;
                break;
            case LanePosition.LEFT:
                newPos.x += moveDistance;
                m_lanePosition = LanePosition.CENTER;
                break;
            default:
                break;
        }
        transform.position = newPos;
    }

}
