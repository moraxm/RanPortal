using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Obstacle : AutomoveObject 
{

    LaneObject m_laneObject;
    public LaneObject laneObject
    {
        get { return m_laneObject; }
    }

	// Use this for initialization
	void Start () 
    {
        m_laneObject = GetComponent<LaneObject>();
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            BallController bc = collision.GetComponent<BallController>();
            if (bc.moving)
                OnPlayerEnterLateral(collision);
            else
                OnPlayerEnterFront(collision);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            BallController bc = collision.GetComponent<BallController>();
            if (bc.moving)
                OnPlayerExitLateral(collision);
            else
                OnPlayerExitFront(collision);
        }
    }

    protected virtual void OnPlayerExitLateral(Collider2D collision)
    {
        
    }

    protected virtual void OnPlayerExitFront(Collider2D collision)
    {
        
    }

    protected virtual void OnPlayerEnterFront(Collider2D collision)
    {
        GameManager.instance.GameOver();
    }

    protected virtual void OnPlayerEnterLateral(Collider2D collision)
    {
        GameManager.instance.GameOver();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    

}
