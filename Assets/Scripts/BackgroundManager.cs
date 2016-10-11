using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour
{
    public float pieceSize = 2.4f;
    public AutomoveObject[] backgroundPiecesSet;
    public AutomoveObject upperPiece;

    // Use this for initialization
    void Start()
    {
        float maxY = 0;
        upperPiece = null;
        foreach (AutomoveObject backgroundPiece in backgroundPiecesSet)
        {
            if (backgroundPiece.transform.position.y > maxY)
            {
                maxY = backgroundPiece.transform.position.y;
                upperPiece = backgroundPiece;
            }
            backgroundPiece.speedSource = GameManager.instance;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        

        AutomoveObject obj = collision.GetComponent<AutomoveObject>();
        if (obj)
        {
            if (obj.CompareTag("DontReuse"))
            {
                obj.speedSource = null;
            }
            else
            {
                obj.transform.position = upperPiece.transform.position + Vector3.up * pieceSize;
                upperPiece = obj;
            }
        }
    }



}
