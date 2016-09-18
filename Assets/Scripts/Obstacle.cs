using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

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
}
