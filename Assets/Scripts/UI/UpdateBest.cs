using UnityEngine;
using System.Collections;

public class UpdateBest : UpdatePoints {

    protected override void Update()
    {
        m_textComponent.text = Persistance.ranking1.ToString();
    }
}
