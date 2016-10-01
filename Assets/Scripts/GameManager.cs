using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour, ISpeedSource
{
    [Header("Menus")]
    public GameObject m_gameOverMenu;
    [Header("Controllers")]
    public ObstacleGenerator m_obstacleGenerator;
    public BallController m_player;
    [Header("Speeds")]
    public float teletransportingSpeed = 40;
    public float normalSpeed = 10;
    [Range(0,1)]
    public float speedIncrementWithPointsFactor = 0.25f;
    // Values per game
    private float m_currentSpeed = 0;
    private float m_points;
    public int points
    {
        get
        {
            return (int) m_points;
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

    public enum GameState
    {
        MENU,
        PLAYING,
        TELETRANSPORTING,
        GAME_OVER
    }

    private GameState m_state;
    private Portal m_activePortal;
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
        state = GameState.PLAYING;
        if (!m_obstacleGenerator)
            m_obstacleGenerator = FindObjectOfType<ObstacleGenerator>();

        if (!m_obstacleGenerator)
            Debug.LogError("No obstacle generator found!");

        m_obstacleGenerator.speedSource = this;
        speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints();
        UpdateSpeed();
        switch (state)
        {
            case GameState.MENU:
                UpdateMenu();
                break;
            case GameState.PLAYING:
                UpdatePlaying();
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

    private void UpdateSpeed()
    {
        speed = m_currentSpeed;
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
            if (m_otherPortal.transform.position.y < m_player.transform.position.y)
                m_otherPortal = null;
            else
                m_otherPortal.gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
        }
        else
        {
            if (m_otherPortal.transform.position.y <= m_player.transform.position.y)
            {
                // Transition to Playing
                m_player.laneObject.lane = m_otherPortal.GetComponent<LaneObject>().lane;
                TransitionToPlaying();
            }
        }
    }

    private void UpdatePlaying()
    {
        
    }

    private void UpdateMenu()
    {
    }

    internal void GameOver()
    {
        state = GameState.GAME_OVER;
        speed = 0;
        m_obstacleGenerator.Restart();
        AdsManager.instance.ShowAdVideo();
    }

    internal void PlayerInPortal(Portal portal)
    {
        state = GameState.TELETRANSPORTING;
        speed = teletransportingSpeed;
        m_activePortal = portal;
        m_player.blocked = true;
        m_player.hide = true;
    }

    private void TransitionToPlaying()
    {
        speed = normalSpeed;
        m_player.blocked = false;
        m_player.hide = false;
        state = GameState.PLAYING;
        m_otherPortal = null;
        m_activePortal = null;
    }

    private void ResetPoints()
    {
        m_points = 0;
    }

    public void Retry()
    {
        if (state == GameState.GAME_OVER)
        {
            m_gameOverMenu.SetActive(false);
            m_player.laneObject.lane = LaneObject.LanePosition.CENTER;
            TransitionToPlaying();
            ResetPoints();
        }
    }

    public Portal m_otherPortal { get; set; }

    public float speed
    {
        get { return m_currentSpeed; }
        set
        {
            if (value > 0) m_currentSpeed = value + points * speedIncrementWithPointsFactor * 0.001f;
            else m_currentSpeed = 0;
        }
    }

    internal void FinishedAd()
    {
        m_gameOverMenu.SetActive(true);
    }
}
