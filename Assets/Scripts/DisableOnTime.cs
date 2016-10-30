using UnityEngine;
using System.Collections;

public class DisableOnTime : MonoBehaviour {

    public float timeToDisable;
    float m_acumTime = 0;

    public void OnEnable()
    {
        m_acumTime = 0;
    }

	// Update is called once per frame
	void Update () 
    {
        m_acumTime += Time.deltaTime;
        if (m_acumTime > timeToDisable)
            gameObject.SetActive(false);
	}
}
