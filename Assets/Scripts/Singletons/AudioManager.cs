using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    AudioMixer m_mixer;
    static AudioManager m_instance;
    public static AudioManager instance
    {
        get
        {
            return m_instance;
        }
    }

    public void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this);
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public AudioMixer mixer
    {
        get { return m_mixer; }
    }

    public bool isAudioEnabled
    {
        get
        {
            float volume;
            if (mixer.GetFloat("FxVolume", out volume))
            {
                return !Mathf.Approximately(volume, -80);
            }
            return false;
        }
        set
        {
            mixer.SetFloat("FxVolume", value ? 0 : -80);
        }
    }

    public bool isMusicEnabled
    {
        get
        {
            float volume;
            if (mixer.GetFloat("MusicVolume", out volume))
            {
                return !Mathf.Approximately(volume, -80);
            }
            return false;
        }
        set
        {
            mixer.SetFloat("MusicVolume", value ? 0 : -80);
        }
    }
}
