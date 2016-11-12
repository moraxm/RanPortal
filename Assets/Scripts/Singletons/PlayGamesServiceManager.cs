using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class PlayGamesServiceManager : MonoBehaviour
{
#if UNITY_WEBGL
    // Properties
    private bool _isConnected;
    private int _userId;
    private string _userName;
    private string _gameAuthToken;
#endif
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
            Start();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Start()
    {
        // Select the Google Play Games platform as our social platform implementation
#if UNITY_ANDROID
        GooglePlayGames.PlayGamesPlatform.Activate();
#elif UNITY_WEBGL

#endif
    }

    public bool isAuthenticated
    {
        get
        {
#if UNITY_ANDROID 
            return Social.localUser.authenticated;
#elif UNITY_WEBGL
            return _isConnected;
#else
            return false;
#endif
        }
    }

    public bool authenticating
    {
        get;
        private set;
    }

    public void ShowLeaderboard()
    {
#if UNITY_ANDROID
        if (isAuthenticated && !authenticating)
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_score);
        else if (!isAuthenticated)
        {
            Authenticate();
        }
#endif
    }

    public void Authenticate()
    {
        if (authenticating) return;

        if (!isAuthenticated)
        {
            authenticating = true;
#if UNITY_ANDROID 
            Social.localUser.Authenticate(OnAuthenticateFinished);
#elif UNITY_WEBGL
            // Begin the API loading process if available
            //Application.ExternalEval(
            //    "if (typeof(kongregateUnitySupport) != 'undefined') {" +
            //    "    kongregateUnitySupport.initAPI('" + gameObject.name + "', 'OnKongregateAPILoaded');" +
            //    "}"
            //);
#endif
        }
    }

    public void LogOut()
    {
#if UNITY_ANDROID
        if (isAuthenticated)
            ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
#endif
        Persistance.SaveAuthenticated(false);
    }

    private void OnAuthenticateFinished(bool success)
    {
        authenticating = false;

        Persistance.SaveAuthenticated(true);
        // Update leaderBoards
        Social.ReportScore(Persistance.ranking1, GPGSIds.leaderboard_score, OnScoreReported);
        //Social.ReportScore(1000, GPGSIds.leaderboard_score, null);
        //Social.ReportScore(500, GPGSIds.leaderboard_score, OnScoreReported);
        //Social.ShowLeaderboardUI();
    }

#if UNITY_WEBGL
    public void OnKongregateAPILoaded(string __userInfoString)
    {
        // Is connected
        authenticating = false;
        _isConnected = true;

        // Splits the user info parameter
        string[] userParams = __userInfoString.Split('|');
        _userId = int.Parse(userParams[0]);
        _userName = userParams[1];
        _gameAuthToken = userParams[2];
    }
#endif

    private void OnScoreReported(bool success)
    {
        //Social.ShowLeaderboardUI();
    }

    public void ReportScore(long score)
    {
        if (isAuthenticated)
#if UNITY_ANDROID
            Social.ReportScore(score, GPGSIds.leaderboard_score, OnScoreReported);
#elif UNITY_WEBGL
        {
            string leaderboard = "score";
            //Application.ExternalCall("kongregate.stats.submit", leaderboard, (int)score);
        }
#else
            return;
#endif
    }
}
