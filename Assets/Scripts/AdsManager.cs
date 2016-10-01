﻿using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {

    static AdsManager m_instance;
    private bool m_waitingToShow;
    public static AdsManager instance
    {
        get
        {
            return m_instance;
        }
    }

    public void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this);
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public int deathsToShow = 1;
    private int m_currentDeaths;

    public void ShowAdVideo()
    {
        ++m_currentDeaths;
        if (m_currentDeaths >= deathsToShow)
        {
            m_currentDeaths = 0;
            if (!Advertisement.isShowing && !m_waitingToShow)
                StartCoroutine(ShowAdCoroutine());
        }
        else
        {
            HandleShowResult(ShowResult.Finished);
        }
    }

    private IEnumerator ShowAdCoroutine()
    {
        m_waitingToShow = true;
        while (!Advertisement.isInitialized || !Advertisement.IsReady())
        {
            yield return new WaitForSeconds(0.5f);
        }

        var options = new ShowOptions { resultCallback = HandleShowResult };
        m_waitingToShow = false;
        Advertisement.Show("video", options);
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }

        GameManager.instance.FinishedAd();
    }
}