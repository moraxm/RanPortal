using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    public ParticleSystem m_particleSystem;
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

    // Last frame moving?
    public bool moving
    {
        set;
        get;
    }
    private int m_movingFlagFrames;
    private PlayerSounds m_sounds;


	// Use this for initialization
	void Start () 
    {
        m_laneObject = GetComponent<LaneObject>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();
        m_sounds = GetComponent<PlayerSounds>();
        m_movingFlagFrames = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckMoving();
	}

    private void CheckMoving()
    {
        if (moving)
            ++m_movingFlagFrames;
        if (m_movingFlagFrames > 1)
        {
            m_sounds.PlayChangeLane();
            moving = false;
            m_movingFlagFrames = 0;
        }
    }

    public void moveLeft()
    {
        if (blocked) return;
        
        switch (m_laneObject.lane)
        {
            case LaneObject.LanePosition.CENTER:
                m_laneObject.lane = LaneObject.LanePosition.LEFT;
                moving = true;
                break;
            case LaneObject.LanePosition.RIGHT:
                m_laneObject.lane = LaneObject.LanePosition.CENTER;
                moving = true;
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
                moving = true;
                break;
            case LaneObject.LanePosition.LEFT:
                m_laneObject.lane = LaneObject.LanePosition.CENTER;
                moving = true;
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

    public void Kill()
    {
        m_sounds.PlayDeath(true);
        m_spriteRenderer.enabled = false;
        m_particleSystem.Play();
    }

    public void Reset()
    {
        m_spriteRenderer.enabled = true;
        m_particleSystem.Stop();
    }
}
