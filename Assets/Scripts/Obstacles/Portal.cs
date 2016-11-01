using UnityEngine;
using System.Collections;

public class Portal : Obstacle 
{
    [SerializeField]
    PlayParticlesCascade m_particles;
    [SerializeField]
    private Portal m_nextPortal;
    [SerializeField]
    [Range(0,10)]
    private int m_maxNextPOrtalToTeletransport = 3;
    public int maxNextPortalToTeletransport
    {
        get { return m_maxNextPOrtalToTeletransport; }
    }
    public Portal nextPortal
    {
        get { return m_nextPortal; }
        set { m_nextPortal = value; }
    }

    protected override void OnPlayerEnterFront(Collider2D collision)
    {
        GameManager.instance.PlayerInPortal(this);
        m_particles.Play();
    }

    public override Portal GetPortal()
    {
        return this;
    }
    public override void Awake()
    {
        base.Awake();
        gameObject.layer = LayerMask.NameToLayer("Portal");
        if (!m_particles)
            m_particles = GetComponentInChildren<PlayParticlesCascade>();
        if (nextPortal)
            m_maxNextPOrtalToTeletransport = -1;
    }
    internal override void Reset()
    {
        base.Reset();
        gameObject.layer = LayerMask.NameToLayer("Portal");
        if (maxNextPortalToTeletransport >= 0)
            m_nextPortal = null;
    }
}
