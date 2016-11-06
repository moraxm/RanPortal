using UnityEngine;
using System.Collections;

public class StartAnimation : MonoBehaviour {
    public Transform startPosition;
    public Transform finalPosition;
    public float time = 1;
    float m_acumTime = 0;

    public void OnEnable()
    {
        m_acumTime = 0;
        RestartPosition();
    }

	// Update is called once per frame
	void Update () 
    {
        m_acumTime += Time.deltaTime;
        float f = m_acumTime / time;
        transform.position = Vector3.Lerp(startPosition.position, finalPosition.position, f);
        if (f > 1) enabled = false;
	}

    public void RestartPosition()
    {
        transform.position = startPosition.position;
    }
}
