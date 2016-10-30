using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BonusUI : MonoBehaviour 
{
    public Image[] m_greenImages;

    public void UpdateUI()
    {
        int idx = GameManager.instance.bonusCoins - 1;
        if (idx > m_greenImages.Length) return;
        for (int i = 0; i < m_greenImages.Length; ++i)
        {
            m_greenImages[i].enabled = i <= idx;

        }

    }

}
