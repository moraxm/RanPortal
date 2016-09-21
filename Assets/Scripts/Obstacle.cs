using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour 
{
    public float speed
    {
        set;
        get;
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.down * speed * Time.deltaTime;
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
