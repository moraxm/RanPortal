using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour 
{
    public ObstacleGenerator obstacleGenerator
    {
        set;
        get;
    }

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
	
	// Update is called once per frame
	void Update () {
        if (obstacleGenerator)
            transform.position += Vector3.down * obstacleGenerator.speed * Time.deltaTime;
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            OnPlayerEnter(collision);
        }
    }

    protected virtual void OnPlayerEnter(Collider2D collision)
    {
        GameManager.instance.GameOver();
    }

}
