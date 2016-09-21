﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public ObstacleGenerator m_obstacleGenerator;
    public BallController m_player;
    public float teletransportingSpeed = 40;
    public float normalSpeed = 10;
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

        m_obstacleGenerator.speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
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

    private void UpdateGameOver()
    {
        throw new System.NotImplementedException();
    }

    private void UpdateTeletransporting()
    {
        if (m_otherPortal == null)
        {
            // Try again
            m_otherPortal = m_obstacleGenerator.GetRandomPortal(m_activePortal);
            if (m_otherPortal.transform.position.y < m_player.transform.position.y)
                m_otherPortal = null;
        }
        else
        {
            if (m_otherPortal.transform.position.y <= m_player.transform.position.y)
            {
                // Transition to Playing
                m_player.laneObject.lane = m_otherPortal.GetComponent<LaneObject>().lane;
                m_obstacleGenerator.speed = normalSpeed;
                m_player.Show();
                state = GameState.PLAYING;
                m_otherPortal = null;
                m_activePortal = null;
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
    }

    internal void PlayerInPortal(Portal portal)
    {
        state = GameState.TELETRANSPORTING;
        m_obstacleGenerator.speed = teletransportingSpeed;
        m_activePortal = portal;
        m_player.Hide();
    }

    public Portal m_otherPortal { get; set; }
}
