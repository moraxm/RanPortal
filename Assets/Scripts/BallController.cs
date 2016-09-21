using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    LaneObject m_laneObject;
    public LaneObject laneObject
    {
        get { return m_laneObject; }
    }
    SpriteRenderer m_spriteRenderer;
    public SpriteRenderer spriteRenderer
    {
        get { return m_spriteRenderer; }
    }

	// Use this for initialization
	void Start () 
    {
        m_laneObject = GetComponent<LaneObject>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void moveLeft()
    {
        Debug.Log("MoveLeft");
        switch (m_laneObject.lane)
        {
            case LaneObject.LanePosition.CENTER:
                m_laneObject.lane = LaneObject.LanePosition.LEFT;
                break;
            case LaneObject.LanePosition.RIGHT:
                m_laneObject.lane = LaneObject.LanePosition.CENTER;
                break;
            default:
                break;
        }
    }

    public void moveRight()
    {
        switch (m_laneObject.lane)
        {
            case LaneObject.LanePosition.CENTER:
                m_laneObject.lane = LaneObject.LanePosition.RIGHT;
                break;
            case LaneObject.LanePosition.LEFT:
                m_laneObject.lane = LaneObject.LanePosition.CENTER;
                break;
            default:
                break;
        }
    }


    internal void Hide()
    {
        m_spriteRenderer.enabled = false;
    }

    internal void Show()
    {
        m_spriteRenderer.enabled = true;
    }
}
