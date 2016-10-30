using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeSkinUI : MonoBehaviour {

    [Range(0,2)]
    public float animationSpeed;
    protected Image m_imageComponent;
    protected Animator m_animator;
	// Use this for initialization
	void Awake () 
    {
        m_imageComponent = GetComponent<Image>();
        m_animator = GetComponent<Animator>();
        m_animator.speed = animationSpeed;
	}

    public void OnEnable()
    {
        UpdateSkin();
    }



    public virtual void NextSkin()
    {
        do
        {
            ++BallController.currentSkinIdx;
        }
        while (!Persistance.isBallActive(BallController.currentSkinIdx));
        UpdateSkin();
    }

    public virtual void PrevSkin()
    {
        do
        {
            --BallController.currentSkinIdx;
        }
        while (!Persistance.isBallActive(BallController.currentSkinIdx));
        UpdateSkin();
    }

    public virtual void UpdateSkin()
    {
        m_animator.SetInteger("Index", BallController.currentSkinIdx);
    }
}
