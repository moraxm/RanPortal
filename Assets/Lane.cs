using UnityEngine;
using System.Collections;

public class Lane : MonoBehaviour 
{
    public SpriteRenderer backgroundSprite;
    public float length
    {
        get
        {
            return backgroundSprite.bounds.size.y;
        }
    }

}
