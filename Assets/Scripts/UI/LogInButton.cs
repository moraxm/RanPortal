using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogInButton : MonoBehaviour 
{
    public Color logout;
    public Sprite logoutSprite;
    public Color login;
    public Sprite loginSprite;
    public Color authenticating;

    Text m_textComponent;
    Image m_ImageComponent;

#if UNITY_WEBGL
    public void Awake()
    {
        gameObject.SetActive(false);
    }
#endif
    public void Start()
    {
        m_textComponent = GetComponentInChildren<Text>();
        m_ImageComponent = GetComponentInChildren<Image>();
        Update();
    }

    public void Update()
    {
        m_textComponent.text = PlayGamesServiceManager.instance.isAuthenticated ? "LOG OUT" : "LOG IN";
        if (PlayGamesServiceManager.instance.authenticating)
        {
            m_ImageComponent.color = authenticating;
            m_ImageComponent.sprite = logoutSprite;
        }
        else if (PlayGamesServiceManager.instance.isAuthenticated)
        {
            m_ImageComponent.color = login;
            m_ImageComponent.sprite = loginSprite;
        }
        else
        {
            m_ImageComponent.color = logout;
            m_ImageComponent.sprite = logoutSprite;
        }
    }

    public void Login()
    {
        if (PlayGamesServiceManager.instance.isAuthenticated)
            PlayGamesServiceManager.instance.LogOut();
        else
            PlayGamesServiceManager.instance.Authenticate();
    }
}
