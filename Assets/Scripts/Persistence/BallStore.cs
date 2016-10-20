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

}
