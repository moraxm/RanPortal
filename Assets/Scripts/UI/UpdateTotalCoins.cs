using UnityEngine;
using System.Collections;

public class UpdateTotalCoins : UpdatePoints {

    protected override void Update()
    {
        m_textComponent.text = Persistance.coins.ToString();
    }
}
