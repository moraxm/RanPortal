using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdatePoints : MonoBehaviour
{
    protected Text m_textComponent;
    // Use this for initialization
    void Start()
    {
        m_textComponent = GetComponent<Text>();
        if (!m_textComponent) enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        m_textComponent.text = GameManager.instance.points.ToString();
    }
}
