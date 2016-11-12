using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideoGemsButton : MonoBehaviour {

    Button m_buttonComponent;

    public void Awake()
    {
        m_buttonComponent = GetComponent<Button>();
    }

	// Update is called once per frame
	void Update () {
        m_buttonComponent.interactable = AdsManager.instance.isVideoGemsAvailable;
	}
}
