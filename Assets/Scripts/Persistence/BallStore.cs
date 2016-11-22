using UnityEngine;
using System.Collections;

public class BallStore : MonoBehaviour {

    public int[] prizes;

    public int getPrize(int idx)
    {
        if (idx <= prizes.Length && idx >= 0)
            return prizes[idx];
        else return -1;
    }

    public bool enoughtCoins(int idx)
    {
        return Persistance.coins >= getPrize(idx);
    }

    public void BuyBall(int idx)
    {
        if (!enoughtCoins(idx))
            Debug.LogError("Not enought coins for buy that ball");

        int currentCoins = Persistance.coins;
        currentCoins -= getPrize(idx);
        Persistance.UnlockBall(idx);
        Persistance.SaveCoins(currentCoins);
        Persistance.SaveSkin(idx);
    }

    public void close()
    {
        if (!Persistance.isBallActive(BallController.currentSkinIdx))
            BallController.currentSkinIdx = 0;
    }
}
