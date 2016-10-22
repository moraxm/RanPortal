using UnityEngine;
using System.Collections;

public class Coin : Obstacle {

    protected SpriteRenderer m_spriteRenderer;
    public override void Awake()
    {
        base.Awake();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnPlayerEnterFront(Collider2D collision)
    {
        CoinCollected();
    }

    protected override void OnPlayerEnterLateral(Collider2D collision)
    {
        CoinCollected();
    }

    
    protected virtual void CoinCollected()
    {
        GameManager.instance.CollectCoin();
        gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
        m_spriteRenderer.enabled = false;
    }

    internal override void Reset()
    {
        base.Reset();
        m_spriteRenderer.enabled = true;
    }
}
