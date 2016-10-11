using UnityEngine;
using System.Collections;

public class Shooter : AutomoveObject 
{
    Vector3 m_initialPosition;
    public GameObject[] colors;
    public override void Awake()
    {
        base.Awake();
        m_initialPosition = transform.position;
    }

    public void Reset()
    {
        speedSource = GameManager.instance;
        transform.position = m_initialPosition;
        foreach (GameObject go in colors)
        {
            go.SetActive(true);
        }
    }
}
