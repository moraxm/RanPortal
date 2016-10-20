using UnityEngine;
using System.Collections;

public class ChangeSkinUIStore : ChangeSkinUI {

    public override void NextSkin()
    {
        ++BallController.currentSkinIdx;
        if (Persistance.isBallActive(BallController.currentSkinIdx))
        {
            m_animator.SetInteger("Index", BallController.currentSkinIdx);
            Persistance.SaveSkin(BallController.currentSkinIdx);
        }
        else
        {
            m_animator.SetInteger("Index",-1);
        }
    }

    public override void PrevSkin()
    {
        --BallController.currentSkinIdx;
        if (Persistance.isBallActive(BallController.currentSkinIdx))
        {
            m_animator.SetInteger("Index", BallController.currentSkinIdx);
            Persistance.SaveSkin(BallController.currentSkinIdx);
        }
        else
        {
            m_animator.SetInteger("Index", -1);
        }
    }
}
