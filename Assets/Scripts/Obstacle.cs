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
	// Use this for initialization
	void Start () {

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
