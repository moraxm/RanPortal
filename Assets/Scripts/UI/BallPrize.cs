using UnityEngine;
using System.Collections;

public class BallPrize : IUITextComponent
{
    public BallStore store;
    public Color availableColor;
    public Color notAvailableColor;

    public override void UpdateText()
    {
        if (Persistance.isBallActive(BallController.currentSkinIdx))
        {
            m_textComponent.text = "";
        }
        else
        {
            int prize = store.getPrize(BallController.currentSkinIdx);
            m_textComponent.color = Persistance.coins >= prize ? availableColor : notAvailableColor;
            m_textComponent.text = prize.ToString();
        }
    }


}
