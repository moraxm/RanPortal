using UnityEngine;
using System.Collections;

public class BonusCoin : Coin 
{
    protected override void CoinCollected()
    {
        GameManager.instance.CollectBonusCoin();
        hide = true;
    }
}
