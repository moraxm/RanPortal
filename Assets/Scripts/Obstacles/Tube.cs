using UnityEngine;
using System.Collections;

public class Tube : Obstacle 
{
    Coin[] m_coins;
    private SpriteRenderer[] m_sprites;
    public override void Awake()
    {
        base.Awake();
        m_coins = GetComponentsInChildren<Coin>();
        m_sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    protected override void OnPlayerEnterFront(Collider2D collision)
    {
        GameManager.instance.m_player.blocked = true;
    }

    protected override void OnPlayerExitFront(Collider2D collision)
    {
        GameManager.instance.m_player.blocked = false;
    }

    internal override void Reset()
    {
        base.Reset();
        foreach (Coin c in m_coins)
        {
            c.Reset();
        }
    }

    public override bool hide
    {
        get
        {
            return base.hide;
        }
        set
        {
            base.hide = value;
            foreach (SpriteRenderer sr in m_sprites)
            {
                sr.enabled = !value;
            }
        }
    }
}
