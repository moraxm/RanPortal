using UnityEngine;
using System.Collections;

public class UpdateCoins : UpdatePoints {

    protected override void Update()
    {
        m_textComponent.text = GameManager.instance.coins.ToString();
    }
}
