using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    public float invulnerableTime = 0.5f;
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
    Collider2D m_collider;
    public Collider2D collider2D
    {
        get { return m_collider; }
    }

    public bool hide
    {
        get { return m_spriteRenderer.enabled; }
        set 
        { 
            m_spriteRenderer.enabled = !value;
            if (value)
                m_collider.enabled = false;
            else
                StartCoroutine(EnabledTimed(m_collider, true, invulnerableTime));
        }
    }

    // Move blocked ?
    public bool blocked
    {
        set;
        get;
    }

	// Use this for initialization
	void Start () 
    {
        m_laneObject = GetComponent<LaneObject>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();
        blocked = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void moveLeft()
    {
        if (blocked) return;
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
        if (blocked) return;
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

    IEnumerator EnabledTimed(Behaviour obj, bool enable, float time)
    {
        yield return new WaitForSeconds(time);
        obj.enabled = enable;
    }
}
