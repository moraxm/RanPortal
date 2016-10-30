using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    Button m_buttonComponent;
    public BallStore m_BallStore;
    // Use this for initialization
    void Start()
    {
        m_buttonComponent = GetComponent<Button>();
    }

    public void Update()
    {
        UpdateButtonStatus();
    }

    public void UpdateButtonStatus()
    {
        if (Persistance.isBallActive(BallController.currentSkinIdx))
        {
            m_buttonComponent.interactable = false;
        }
        else if (m_BallStore.enoughtCoins(BallController.currentSkinIdx))
        {
            m_buttonComponent.interactable = true;
        }
        else
        {
            m_buttonComponent.interactable = false;
        }
    }

    public void Buy()
    {
        m_BallStore.BuyBall(BallController.currentSkinIdx);
    }
}
