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
        gc = GetComponent<GameController>();
        parentObject = GameObject.Find("WinLosePanel");
        txt = gameObject.GetComponent<Text>();
        txt.text = GameOverState.gameResult;
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.gameOver) {
            parentObject.SetActive(true);
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
            parentObject.SetActive(false);
        }
    }
}