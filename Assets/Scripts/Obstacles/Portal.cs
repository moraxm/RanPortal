using UnityEngine;
using System.Collections;

public class Portal : Obstacle 
{
    [SerializeField]
    PlayParticlesCascade m_particles;
    [SerializeField]
    private Portal m_nextPortal;
    [SerializeField]
    private int m_maxNextPOrtalToTeletransport = 3;
    public int maxNextPortalToTeletransport
    {
        get { return m_maxNextPOrtalToTeletransport; }
    }
    public Portal nextPortal
    {
        get { return m_nextPortal; }
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
    }
    internal override void Reset()
    {
        base.Reset();
        gameObject.layer = LayerMask.NameToLayer("Portal");
    }
}
