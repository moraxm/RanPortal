using UnityEngine;
using System.Collections;
using System;

public class BallController : MonoBehaviour {

    public enum BALL_SKINS
    {
        BALL,
        BLACK,
        CANDY,
        CIRCUS,
        GREEN,
        PURPLE,
        RED,
        SKULL,
        SPIKE,
        SPIKE_RED,
    }
    private static BALL_SKINS m_currentSkin;
    public static int currentSkinIdx
    {
        get
        {
            return (int)m_currentSkin;
        }
        set
        {
            int length = Enum.GetNames(typeof(BALL_SKINS)).Length;
            if (value >= length)
                value = 0;
            else if (value < 0)
                value = length - 1;

            m_currentSkin = (BALL_SKINS)value;
            Persistance.SaveSkin(value);
        }
    }
    [Header("Particles")]
    public PlayParticlesCascade m_particleSystemPortals;
    public ParticleSystem m_particleSystemDeath;
    public MoveParticles m_particlesMove;
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
            m_collider.enabled = !value;
            if (!value)
                StartCoroutine(DisabledPortals(invulnerableTime));
            //if (GameManager.instance.state == GameManager.GameState.TELETRANSPORTING)
            //    m_particleSystemPortals.Play();
            //if (value)
            //    m_collider.enabled = false;
            //else
            //    StartCoroutine(EnabledTimed(m_collider, true, invulnerableTime));
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
    public PlayerSounds sounds
    {
        get { return m_sounds; }
    }
    private Animator m_animator;

	// Use this for initialization
	void Start () 
    {
        m_laneObject = GetComponent<LaneObject>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();
        m_sounds = GetComponent<PlayerSounds>();
        m_animator = GetComponent<Animator>();
        m_movingFlagFrames = 0;
        Reset();
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
                m_particlesMove.Play(LaneObject.LanePosition.CENTER, LaneObject.LanePosition.LEFT);
                m_laneObject.lane = LaneObject.LanePosition.LEFT;
                moving = true;
                break;
            case LaneObject.LanePosition.RIGHT:
                m_particlesMove.Play(LaneObject.LanePosition.RIGHT, LaneObject.LanePosition.CENTER);
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
                m_particlesMove.Play(LaneObject.LanePosition.CENTER, LaneObject.LanePosition.RIGHT);
                m_laneObject.lane = LaneObject.LanePosition.RIGHT;
                moving = true;
                break;
            case LaneObject.LanePosition.LEFT:
                m_particlesMove.Play(LaneObject.LanePosition.LEFT, LaneObject.LanePosition.CENTER);
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

    IEnumerator DisabledPortals(float time)
    {
        portalsAllowed = false;
        yield return new WaitForSeconds(time);
        portalsAllowed = true;
    }

    public void Kill()
    {
        m_sounds.PlayDeath(true);
        m_spriteRenderer.enabled = false;
        m_particleSystemDeath.Play();
        blocked = true;
    }

    public void Reset()
    {
        m_spriteRenderer.enabled = true;
        m_particleSystemDeath.Stop();
        m_animator.SetInteger("Index", Persistance.skin);
    }

    public bool portalsAllowed { get; private set; }
}
