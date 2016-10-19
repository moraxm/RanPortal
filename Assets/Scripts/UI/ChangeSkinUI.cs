using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeSkinUI : MonoBehaviour {

    [Range(0,2)]
    public float animationSpeed;
    Image m_imageComponent;
    Animator m_animator;
	// Use this for initialization
	void Start () 
    {
        m_imageComponent = GetComponent<Image>();
        m_animator = GetComponent<Animator>();
        m_animator.speed = animationSpeed;
	}

    public void NextSkin()
    {
        ++GameManager.instance.currentSkinIdx;
        m_animator.SetInteger("Index", GameManager.instance.currentSkinIdx);
    }

    public void PrevSkin()
    {
        --GameManager.instance.currentSkinIdx;
        m_animator.SetInteger("Index", GameManager.instance.currentSkinIdx);
    }
}
