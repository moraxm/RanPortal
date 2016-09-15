using UnityEngine;
using System.Collections;

public class LaneGenerator : MonoBehaviour {

    public GameObject[] lanes;
    public GameObject firstLane;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject getLane()
    {
        if (lanes.Length == 0) return null;
        int length = lanes.Length;
        int randomLaneIdx = Random.Range(0,length);
        return lanes[randomLaneIdx];
    }

    public GameObject getFirstLane()
    {
        return firstLane;
    }
}
