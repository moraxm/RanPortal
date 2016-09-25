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
            OnPlayerEnter(collision);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            OnPlayerExit(collision);
        }
    }

    protected virtual void OnPlayerExit(Collider2D collision)
    {
        
    }

    protected virtual void OnPlayerEnter(Collider2D collision)
    {
        GameManager.instance.GameOver();
    }

}
