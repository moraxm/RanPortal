using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour, ISpeedSource
{
    // Constants
    public const int c_CoinsToBonus = 3;

    [Header("Menus")]
    public GameObject m_gameOverMenu;
    [Header("Controllers")]
    public ObstacleGenerator m_obstacleGenerator;
    public BallController m_player;
    [Header("Count down events")]
    public UnityEvent on3CountDown;
    public UnityEvent on2CountDown;
    public UnityEvent on1CountDown;
    public UnityEvent on0CountDown;
    float m_acumTime;
    int m_second;

    [Header("Bonus events")]
    public UnityEvent onBonus;
    public UnityEvent onBonusCoinCollected;

    [Header("Speeds")]
    public float teletransportingSpeed = 40;
    public float normalSpeed = 10;
    public int pointsToIncrement = 100;
    public float incrementSpeed = 5;
    public float maxSpeed = 40;

    int m_currenWaveSpeed;
    // Values per game
    private int m_bonusCoins = 0;
    public int bonusCoins
    {
        get { return m_bonusCoins; }
    }
    private float m_currentSpeed = 0;
    private float m_aditionalSpeed;
    private float m_points;
    public int points
    {
        get
        {
            return (int) m_points;
        }
    }
    private int m_coins;
    public int coins
    {
        get
        {
            return m_coins;
        }
    }

    static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            return m_instance;
        }
    }

    public float aditionalSpeed
    {
        get { return m_aditionalSpeed; }
        set
        {
            m_aditionalSpeed = value;
        }
    }

    public float speed
    {
        get { return m_currentSpeed + m_aditionalSpeed; }
    }

    public void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this);
            m_instance.Start();
        }
        else
        {
            m_instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }

    public enum GameState
    {
        MENU,
        COUNT_DOWN,
        PLAYING,
        BONUS,
        TELETRANSPORTING,
        GAME_OVER
    }

    private GameState m_state;
    private Portal m_activePortal;
    public Portal activePortal
    {
        get { return m_activePortal; }
    }
    public GameState state
    {
        get
        {
            return m_state;
        }

        private set
        {
            m_state = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        m_obstacleGenerator = FindObjectOfType<ObstacleGenerator>();
        if (!m_obstacleGenerator)
            Debug.LogError("No obstacle generator found!");

        m_obstacleGenerator.speedSource = this;

        m_player = FindObjectOfType<BallController>();
        if (!m_obstacleGenerator)
            Debug.LogError("No player found!");

        m_currenWaveSpeed = 1;

        TransitionToCountDown();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints();
        UpdateSpeed();
        switch (state)
        {
            case GameState.COUNT_DOWN:
                UpdateCountDown();
                break;
            case GameState.MENU:
                UpdateMenu();
                break;
            case GameState.PLAYING:
                UpdatePlaying();
                break;
            case GameState.BONUS:
                UpdateBonus();
                break;
            case GameState.TELETRANSPORTING:
                UpdateTeletransporting();
                break;
            case GameState.GAME_OVER:
                UpdateGameOver();
                break;
            default:
                break;
        }
    }

    private void UpdateBonus()
    {

    }

    private void UpdateCountDown()
    {
        m_acumTime += Time.deltaTime;
        if (m_acumTime > 1)
        {
            m_acumTime -= 1;
            --m_second;
            switch (m_second)
            {
                case 2:
                    on2CountDown.Invoke();
                    break;
                case 1:
                    on1CountDown.Invoke();
                    break;
                case 0:
                    on0CountDown.Invoke();
                    m_currentSpeed = normalSpeed;
                    TransitionToPlaying();
                    break;
                default:
                    break;
            }
        }
    }

    private void UpdateSpeed()
    {
        if (speed >= maxSpeed) return;
        if (m_currenWaveSpeed * pointsToIncrement < m_points)
        {
            m_currentSpeed = m_currentSpeed + incrementSpeed;
            ++m_currenWaveSpeed;
        }
        
    }

    private void UpdatePoints()
    {
        m_points += speed * Time.deltaTime;
    }

    private void UpdateGameOver()
    {
    }

    private void UpdateTeletransporting()
    {
        if (m_otherPortal == null)
        {
            // Try again
            m_otherPortal = m_obstacleGenerator.GetRandomPortal(m_activePortal);
            if (m_otherPortal)
            {
                if (m_otherPortal.transform.position.y < m_player.transform.position.y)
                    m_otherPortal = null;
                else
                    m_otherPortal.gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
            }
        }
        else
        {
            if (m_otherPortal.transform.position.y <= m_player.transform.position.y)
            {
                // Transition to Playing
                m_player.laneObject.lane = m_otherPortal.GetComponent<LaneObject>().lane;
                aditionalSpeed = 0;
                TransitionToPlaying();
            }
        }
    }

    private void UpdatePlaying()
    {
        if (bonusCoins >= c_CoinsToBonus)
        {
            m_bonusCoins = 0;
            TransitionToBonus();
        }
    }

    private void TransitionToBonus()
    {
        aditionalSpeed = 0;
        onBonus.Invoke();
        m_obstacleGenerator.OnBonus();
    }

    private void UpdateMenu()
    {

    }

    internal void GameOver()
    {
        state = GameState.GAME_OVER;
        m_currentSpeed = 0;
        aditionalSpeed = 0;
        m_player.Kill();
        AdsManager.instance.ShowAdVideo();
        Persistance.SavePoints(points);
        Persistance.SaveCoins(coins);
    }


    internal void PlayerInPortal(Portal portal)
    {
        state = GameState.TELETRANSPORTING;
        aditionalSpeed = teletransportingSpeed;
        m_activePortal = portal;
        m_otherPortal = portal.nextPortal; // This could be null. If so, in the update we will find other portal.
        m_player.blocked = true;
        m_player.hide = true;
    }

    private void TransitionToPlaying()
    {
        m_player.blocked = false;
        m_player.hide = false;
        state = GameState.PLAYING;
        m_otherPortal = null;
        m_activePortal = null;
        aditionalSpeed = 0;
    }

    private void TransitionToCountDown()
    {
        on3CountDown.Invoke();
        m_acumTime = 0;
        m_second = 3;
        aditionalSpeed = 0;
        m_currentSpeed = 0;
        m_coins = 0;
        state = GameState.COUNT_DOWN;
        m_player.blocked = true;
    }

    private void ResetPoints()
    {
        m_points = 0;
    }

    public void Retry()
    {
        if (state == GameState.GAME_OVER)
        {
            m_obstacleGenerator.Restart();
            
            m_gameOverMenu.SetActive(false);
            m_player.Reset();
            m_player.laneObject.lane = LaneObject.LanePosition.CENTER;
            TransitionToCountDown();
            ResetPoints();
        }
    }

    public Portal m_otherPortal { get; set; }
    private int m_portalsSkiped;

    internal void FinishedAd()
    {
        m_gameOverMenu.SetActive(true);
    }


    internal void CollectCoin()
    {
        ++m_coins;
    }

    internal void CollectBonusCoin()
    {
        ++m_bonusCoins;
        onBonusCoinCollected.Invoke();
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
