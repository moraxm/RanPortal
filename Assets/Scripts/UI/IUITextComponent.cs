using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IUITextComponent : MonoBehaviour {

    protected Text m_textComponent;
    // Use this for initialization
    void Start()
    {
        m_textComponent = GetComponent<Text>();
        if (!m_textComponent) enabled = false;
    }

    public virtual void UpdateText()
    {

    }

}
