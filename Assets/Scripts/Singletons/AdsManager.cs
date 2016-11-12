using UnityEngine;
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
    private int m_currentDeaths = 0;

    public int deathsToShowVideoGems = 2;
    private int m_currentDeathsGems = 0;
    public int gemsReward = 20;
    bool m_showingAdForGems = false;

    public bool isVideoGemsAvailable
    {
        get
        {
            return m_currentDeathsGems >= deathsToShowVideoGems;
        }
    }

    public void ShowAdVideoForGems()
    {
#if UNITY_ANDROID
        if (isVideoGemsAvailable)
        {
            m_currentDeathsGems = 0;
            m_showingAdForGems = true;
            if (!Advertisement.isShowing && !m_waitingToShow)
                StartCoroutine(ShowAdCoroutine());
        }
#endif
    }

    public void ShowAdVideo()
    {
#if UNITY_ANDROID
        ++m_currentDeathsGems;
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
#else
        GameManager.instance.FinishedAd();
#endif
    }

#if UNITY_ANDROID
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
#endif
#if UNITY_ANDROID
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                if (m_showingAdForGems)
                {
                    Persistance.SaveCoins(Persistance.coins + gemsReward);
                    m_showingAdForGems = false;
                }

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
#endif
}
