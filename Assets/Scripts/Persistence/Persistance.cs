﻿using UnityEngine;
using System.Collections;

public class Persistance : MonoBehaviour
{
    public const string SG_POINTS_ID = "Points";
    public const string SG_BALLS_ID = "Balls";
    public const string SG_CURRENT_SKIN_ID = "Skin";
    public const string SG_RANKING_1_ID = "Ranking1";
    public const string SG_RANKING_2_ID = "Ranking2";
    public const string SG_RANKING_3_ID = "Ranking3";
    public const string SG_COINS_ID = "Coins";

    public static int ranking1
    {
        get {return PlayerPrefs.GetInt(SG_RANKING_1_ID);}
    }
    public static int ranking2
    {
        get { return PlayerPrefs.GetInt(SG_RANKING_2_ID); }
    }
    public static int ranking3
    {
        get { return PlayerPrefs.GetInt(SG_RANKING_3_ID); }
    }
    public static int balls
    {
        get { return PlayerPrefs.GetInt(SG_BALLS_ID); }
    }
    public static bool isBallActive(int idx)
    {
        int mask = (int)Mathf.Pow(2,idx);
        return (balls & mask) != 0;
    }

    public static int skin
    {
        get { return PlayerPrefs.GetInt(SG_CURRENT_SKIN_ID); }
    }

    public static int coins 
    { 
        get { return PlayerPrefs.GetInt(SG_COINS_ID);}
    }


    public static void SavePoints(int points)
    {
        int first = 0;
        int second = 0;
        int third = 0;
        if (PlayerPrefs.HasKey(SG_RANKING_1_ID)) first = PlayerPrefs.GetInt(SG_RANKING_1_ID);
        if (PlayerPrefs.HasKey(SG_RANKING_2_ID)) second = PlayerPrefs.GetInt(SG_RANKING_2_ID);
        if (PlayerPrefs.HasKey(SG_RANKING_3_ID)) third = PlayerPrefs.GetInt(SG_RANKING_3_ID);
        if (points > first) SetFirst(points);
        else if (points > second) SetSecond(points);
        else if (points > third) SetThird(points);
        PlayerPrefs.Save();
        PlayGamesServiceManager.instance.ReportScore(points);
    }

    private static void SetThird(int points)
    {
        PlayerPrefs.SetInt(SG_RANKING_3_ID, points);
    }

    private static void SetSecond(int points)
    {
        int prevSecond = PlayerPrefs.GetInt(SG_RANKING_2_ID);
        PlayerPrefs.SetInt(SG_RANKING_2_ID, points);
        SetThird(prevSecond);
    }

    private static void SetFirst(int points)
    {
        int prevFirst = PlayerPrefs.GetInt(SG_RANKING_1_ID);
        PlayerPrefs.SetInt(SG_RANKING_1_ID, points);
        SetSecond(prevFirst);
    }

    public static void UnlockBall(int idx)
    {
        int mask = (int)Mathf.Pow(2,idx);
        int result = mask | balls;
        PlayerPrefs.SetInt(SG_BALLS_ID, result);
        PlayerPrefs.Save();
    }

    internal static void SaveCoins(int coins)
    {
        PlayerPrefs.SetInt(SG_COINS_ID, coins);
        PlayerPrefs.Save();
    }

    internal static void SaveSkin(int skin)
    {
        PlayerPrefs.SetInt(SG_CURRENT_SKIN_ID, skin);
        PlayerPrefs.Save();
    }

    public void Awake()
    {
        // HACK para probar tienda
        //PlayerPrefs.SetInt(SG_BALLS_ID, 0);
        //PlayerPrefs.Save();
        //if (Persistance.balls == 0)
        //{
        //    Persistance.UnlockBall(0);
        //}
        //Persistance.SaveCoins(50000);
    }

    
}
