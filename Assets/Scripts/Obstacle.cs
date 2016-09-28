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
            Vector3 dir = collision.transform.position - transform.position;
            BallController bc = collision.GetComponent<BallController>();
            Debug.Log(dir);
            OnPlayerEnterFront(collision);
            //if (dir.normalized == Vector3.down)
            //    OnPlayerEnterFront(collision);
            //else
            //    OnPlayerEnterLateral(collision);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            Vector3 dir = collision.transform.position - transform.position;
            Debug.Log(dir);
            OnPlayerExitFront(collision);
            //if (dir.normalized == Vector3.down)
            //    OnPlayerExitFront(collision);
            //else
            //    OnPlayerExitLateral(collision);
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
        OnPlayerEnterFront(collision);
    }

}
