using UnityEngine;
using System.Collections;

public class ChangeSkinBall : MonoBehaviour {
    Animator m_animator;
    int m_index;

	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        m_index = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void NextAnimation()
    {
        ++m_index;
        m_animator.SetInteger("Index", m_index);
    }
}
