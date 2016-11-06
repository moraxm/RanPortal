using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogInButton : MonoBehaviour 
{
    public Color logout;
    public Color login;
    public Color authenticating;

    Text m_textComponent;
    Image m_ImageComponent;
    public void Start()
    {
        m_textComponent = GetComponentInChildren<Text>();
        m_ImageComponent = GetComponentInChildren<Image>();
        Update();
    }

    public void Update()
    {
        m_textComponent.text = PlayGamesServiceManager.instance.isAuthenticated ? "Log out" : "Log In";
        if (PlayGamesServiceManager.instance.authenticating)
            m_ImageComponent.color = authenticating;
        else if (PlayGamesServiceManager.instance.isAuthenticated)
            m_ImageComponent.color = login;
        else
            m_ImageComponent.color = logout;
    }

    public void Login()
    {
        if (PlayGamesServiceManager.instance.isAuthenticated)
            PlayGamesServiceManager.instance.LogOut();
        else
            PlayGamesServiceManager.instance.Authenticate();
    }
}
