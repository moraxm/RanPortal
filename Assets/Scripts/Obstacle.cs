using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour 
{
    public UnityEvent onCollisionPlayer;
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

    public void OnCollisionEnter2D(Collision2D collision)
    {

    }

    
}
