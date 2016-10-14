using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour 
{
    public AudioClip m_deathSound;
    public AudioClip m_teletrasportingSound;
    public AudioClip m_bonusSound;
    public AudioClip[] m_changeLaneSounds;
    private AudioSource m_audioSource;

	// Use this for initialization
	void Start () 
    {
        m_audioSource = gameObject.AddComponent<AudioSource>();
        m_audioSource.loop = false;
        m_audioSource.playOnAwake = false;
	}
	
    public void PlayDeath(bool play)
    {
        m_audioSource.loop = false;
        m_audioSource.clip = m_deathSound;
        PlayAudioSource(play);
    }

    private void PlayAudioSource(bool play)
    {
        m_audioSource.Stop();
        if (play)
            m_audioSource.Play();
    }

    public void PlayTeletrasporting(bool play)
    {
        m_audioSource.loop = true;
        m_audioSource.clip = m_teletrasportingSound;
        PlayAudioSource(play);
    }

    public void PlayBonus(bool play)
    {
        m_audioSource.loop = true;
        m_audioSource.clip = m_bonusSound;
        PlayAudioSource(play);
    }

    public void PlayChangeLane()
    {
        if (m_changeLaneSounds.Length > 0)
        {
            m_audioSource.loop = false;
            m_audioSource.PlayOneShot(m_changeLaneSounds[Random.Range(0,m_changeLaneSounds.Length)]);
        }
        
    }
}
