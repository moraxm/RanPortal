using UnityEngine;
using System.Collections;

public class Tube : Obstacle 
{
    Coin[] m_coins;
    public override void Awake()
    {
        base.Awake();
        m_coins = GetComponentsInChildren<Coin>();
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
}
