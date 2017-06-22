using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("mouse 1 clicked");
        //    var players = GetComponentInParent<GameController>().GetComponents<PlayerInterface>();
        //    foreach(var player in players)
        //    {
        //        Debug.Log("player found");
        //        if (player.IsAi())
        //        {
        //            Debug.Log("not a human");
        //            continue;
        //        }
        //        var dude = (HumanPlayerControl)player;
        //        dude.setNextCol(1);
        //    }
        //    //GetComponent<HumanPlayerControl>().setNextCol(1);
            

        //}

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("mouse1 was clicked");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.transform.position.x + " was clicked");
                var game = GameObject.Find("DaObject");
                var b = game.GetComponent<BoardControl>();
                b.AddPiece((int)hit.collider.transform.position.x, "black");

                // whatever tag you are looking for on your game object
                if (hit.collider.tag == "Trigger")
                {
                    Debug.Log("---> Hit: ");
                }
            }
        }
    }
}
