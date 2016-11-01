using UnityEngine;
using System.Collections;

public class PCInput : MonoBehaviour {
	
    public BallController ballController;
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ballController.moveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ballController.moveRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.CollectBonusCoin();
        }
    }
}
