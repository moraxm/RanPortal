using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MuteButton : MonoBehaviour 
{
    public AudioMixer m_mixer;

    const string MUSIC_VOLUME_PARAMETER_NAME = "MusicVolume";
    const string FX_VOLUME_PARAMETER_NAME = "FxVolume";

    public void MuteMusic()
    {
        MuteParameter(MUSIC_VOLUME_PARAMETER_NAME);
    }

    public void MuteFx()
    {
        MuteParameter(FX_VOLUME_PARAMETER_NAME);
    }

    private void MuteParameter(string parameterName)
    {
        float volume;
        if (m_mixer.GetFloat(parameterName, out volume))
        {
            if (Mathf.Approximately(volume, 0))
            {
                // The volume is on, so mute
                m_mixer.SetFloat(parameterName, -80);
            }
            else
            {
                // The volume is muted, so turn on
                m_mixer.SetFloat(parameterName, 0);
            }
        }
    }

}
