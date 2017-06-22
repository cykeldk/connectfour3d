using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("mouse1 was clicked");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider.transform.position.x + " was clicked");
                int humanInput = (int) hit.collider.transform.position.x;
                var game = (GameController) GameObject.FindObjectOfType<GameController>();
                game.TestMyShit();
                if (!game.currentPlayer.IsAi())
                {
                    game.currentPlayer.SetNextCol(humanInput);
                }
            }
        }
    }
}
