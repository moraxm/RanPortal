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

    public void UpdateButtonStatus()
    {
        m_buttonComponent.interactable = m_BallStore.enoughtCoins(BallController.currentSkinIdx) || Persistance.isBallActive(BallController.currentSkinIdx);
    }
}
