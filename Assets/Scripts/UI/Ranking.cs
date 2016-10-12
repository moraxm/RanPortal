using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {

    [Range(1,3)]
    public int rankingPosition;
	// Use this for initialization
	void Start () 
    {
        Text textComponent = GetComponent<Text>();
        switch (rankingPosition)
        {
            case 1:
                textComponent.text = Persistance.ranking1.ToString();
                break;
            case 2:
                textComponent.text = Persistance.ranking2.ToString();
                break;
            case 3:
                textComponent.text = Persistance.ranking3.ToString();
                break;
            default:
                textComponent.text = "ranking position variable is bad set in the inspector";
                break;
        }
        
	}
	

}
