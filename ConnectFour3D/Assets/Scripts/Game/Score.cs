using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    GameController gc;
    //    public winLoseText;
    Text txt;
    private int currentscore = 0;

    public GameObject parentObject;
    void Start()
    {
        //parentObject = GameObject.Find(transform.name + "WinLosePanel");
        txt = gameObject.GetComponent<Text>();
        txt.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.gameOver) {
            parentObject.transform.gameObject.active = true;
            if (gc.aiWon)
            {
                txt.text = "You Lose";
            }
            else
            {
                txt.text = "You Win!";
            }
        }
        else
        {
            parentObject.transform.gameObject.active = false;
        }
    }
}