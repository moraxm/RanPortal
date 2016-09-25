using UnityEngine;
using System.Collections;

public class Tube : Obstacle 
{
    protected override void OnPlayerEnter(Collider2D collision)
    {
        GameManager.instance.m_player.blocked = true;
    }

    protected override void OnPlayerExit(Collider2D collision)
    {
        GameManager.instance.m_player.blocked = false;
    }
}
