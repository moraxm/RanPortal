using UnityEngine;
using System.Collections;

public class Shooter : AutomoveObject 
{
    [System.Serializable]
    public class ShooterColor
    {
        public GameObject gameObject;
        public AudioClip audioClip;
    }
    Vector3 m_initialPosition;
    AudioSource m_audioSource;

    public ShooterColor[] colors;
    public override void Awake()
    {
        base.Awake();
        m_initialPosition = transform.position;
        m_audioSource = GetComponent<AudioSource>();
    }

    public void Reset()
    {
        speedSource = GameManager.instance;
        transform.position = m_initialPosition;
        foreach (ShooterColor go in colors)
        {
            go.gameObject.SetActive(true);
        }
    }

    public void PlaySound(int idx)
    {
        if (idx >= 0 && idx < colors.Length)
        {
            m_audioSource.clip = colors[idx].audioClip;
            m_audioSource.Play();
        }
    }
}
