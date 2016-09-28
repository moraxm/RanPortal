using UnityEngine;
using System.Collections;

public class Tube : Obstacle 
{
    protected override void OnPlayerEnterFront(Collider2D collision)
    {
        GameManager.instance.m_player.blocked = true;
    }

    protected override void OnPlayerExitFront(Collider2D collision)
    {
        GameManager.instance.m_player.blocked = false;
    }
}
