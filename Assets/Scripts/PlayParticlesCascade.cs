using UnityEngine;
using System.Collections;

public class PlayParticlesCascade : MonoBehaviour {

    ParticleSystem[] particles;
    public void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    public void Play()
    {
        foreach (ParticleSystem p in particles)
        {
            p.Play();
        }
    }
}
