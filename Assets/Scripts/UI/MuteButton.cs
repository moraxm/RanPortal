using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour 
{
    public Sprite m_muteImage;
    public Sprite m_noMuteImage;
    public string m_mixerParamenterName;
    private Image m_ImageComponent;

    public void Start()
    {
        m_ImageComponent = GetComponent<Image>();
        m_ImageComponent.sprite = isMuted ? m_muteImage : m_noMuteImage;
    }

    public void Mute()
    {
        if (isMuted)
        {
            // The volume is muted, so turn on
            AudioManager.instance.mixer.SetFloat(m_mixerParamenterName, 0);
            m_ImageComponent.sprite = m_noMuteImage;
        }
        else
        {
            // The volume is on, so mute
            AudioManager.instance.mixer.SetFloat(m_mixerParamenterName, -80);
            m_ImageComponent.sprite = m_muteImage;
        }
        Persistance.SaveAudioSettings();
    }

    private bool isMuted
    {
        get
        {
            float volume;
            if (AudioManager.instance.mixer.GetFloat(m_mixerParamenterName, out volume))
            {
                return Mathf.Approximately(volume, -80);
            }
            return false;
        }
        
    }
}
