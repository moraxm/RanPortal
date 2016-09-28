using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdatePoints : MonoBehaviour
{
    Text m_textComponent;
    // Use this for initialization
    void Start()
    {
        m_textComponent = GetComponent<Text>();
        if (!m_textComponent) enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_textComponent.text = GameManager.instance.points.ToString();
    }
}
