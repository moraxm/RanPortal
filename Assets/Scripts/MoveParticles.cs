using UnityEngine;
using System.Collections;

public class MoveParticles : MonoBehaviour 
{
    public ParticleSystem m_leftParticleSystem;
    public ParticleSystem m_centerParticleSystem;
    public ParticleSystem m_rightParticleSystem;

    public void Play(LaneObject.LanePosition current, LaneObject.LanePosition next)
    {
        if (current == LaneObject.LanePosition.CENTER)
        {
            if (next == LaneObject.LanePosition.LEFT)
            {
                m_centerParticleSystem.transform.rotation = Quaternion.LookRotation(Vector3.left);
            }
            else
            {
                m_centerParticleSystem.transform.rotation = Quaternion.LookRotation(Vector3.right);
            }
            m_centerParticleSystem.Play();
        }
        else if (current == LaneObject.LanePosition.LEFT)
        {
            m_leftParticleSystem.Play();
        }
        else if (current == LaneObject.LanePosition.RIGHT)
        {
            m_rightParticleSystem.Play();
        }
    }
}
