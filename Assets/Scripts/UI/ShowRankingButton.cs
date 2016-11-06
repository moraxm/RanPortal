using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowRankingButton : MonoBehaviour {
    private Button m_ButtonComponent;

	// Use this for initialization
	void Start () {
        m_ButtonComponent = GetComponent<Button>();
        Update();
	}
	
	// Update is called once per frame
	void Update () {
        m_ButtonComponent.interactable = PlayGamesServiceManager.instance.isAuthenticated;
	}

    public void Show()
    {
        PlayGamesServiceManager.instance.ShowLeaderboard();
    }
}
