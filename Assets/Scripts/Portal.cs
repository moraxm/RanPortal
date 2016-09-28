using UnityEngine;
using System.Collections;

public class Portal : Obstacle 
{
    protected override void OnPlayerEnterFront(Collider2D collision)
    {
        GameManager.instance.PlayerInPortal(this);
    }
}
