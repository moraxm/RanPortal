using UnityEngine;
using System.Collections;

public class Coin : Obstacle {

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
        hide = true;
    }

    internal override void Reset()
    {
        base.Reset();
        hide = false;
    }
}
