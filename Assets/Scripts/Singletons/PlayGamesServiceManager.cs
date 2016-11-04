using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class PlayGamesServiceManager : MonoBehaviour
{

    static PlayGamesServiceManager m_instance;
    private bool m_waitingToShow;
    public static PlayGamesServiceManager instance
    {
        get
        {
            if (!m_instance)
            {
                GameObject o = new GameObject("PlayGamesServiceManager");
                o.AddComponent<PlayGamesServiceManager>();
            }
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

    public void Start()
    {
        // Select the Google Play Games platform as our social platform implementation
        GooglePlayGames.PlayGamesPlatform.Activate();
    }

    public bool isAuthenticated
    {
        get
        {
            return Social.localUser.authenticated;
        }
    }

    public bool authenticating
    {
        get;
        private set;
    }

    public void ShowLeaderboard()
    {
        if (isAuthenticated && !authenticating)
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_score);
        else if (!isAuthenticated)
        {
            Authenticate();
        }
    }

    public void Authenticate()
    {
        if (authenticating) return;

        if (!isAuthenticated)
        {
            authenticating = true;
            Social.localUser.Authenticate(OnAuthenticateFinished);
        }
    }

    public void LogOut()
    {
        if (isAuthenticated)
            ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
    }

    private void OnAuthenticateFinished(bool success)
    {
        authenticating = false;
        //Social.ReportScore(1000, GPGSIds.leaderboard_score, null);
        //Social.ReportScore(500, GPGSIds.leaderboard_score, OnScoreReported);
        //Social.ShowLeaderboardUI();
    }

    private void OnScoreReported(bool success)
    {
        //Social.ShowLeaderboardUI();
    }

    public void ReportScore(long score)
    {
        if (isAuthenticated)
            Social.ReportScore(score, GPGSIds.leaderboard_score, OnScoreReported);
    }
}
