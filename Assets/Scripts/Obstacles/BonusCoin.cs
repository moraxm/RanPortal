using UnityEngine;
using System.Collections;

public class BonusCoin : Coin 
{
    protected override void CoinCollected()
    {
        GameManager.instance.CollectBonusCoin();
        gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
        m_spriteRenderer.enabled = false;
    }
}
