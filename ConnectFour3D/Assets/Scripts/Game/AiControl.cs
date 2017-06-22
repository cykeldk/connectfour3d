﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiControl : PlayerInterface {
    
    private string playerName;
    private string playerColor;
    private string opponentColor;

    private int confusion;
    private System.Random sysrand = new System.Random();
    private BoardInterface board;

    // Use this for initialization
    public AiControl (int confusion, BoardInterface board, string color) {
        this.confusion = confusion;
        this.board = board;
        this.playerColor = color;
        if (color.Equals("white"))
        {
            opponentColor = "black";
        }
        else
        {
            opponentColor = "white";
        }
        Debug.Log("AI confusion: " + confusion);
    }
	
	// Update is called once per frame

    public bool IsAi()
    {
        return true;
    }

    public string GetColor()
    {
        return playerColor;
    }

    public string GetName()
    {
        return "AI Level: " + (100-confusion);
    }

    
    public bool PlayTurn()
    {
        
        List<int> possibleColsToPlay = new List<int>();
        float tempScore = 0f;
        var blockCol = canOtherPlayerWin();
        //Debug.Log(playerColor + "'s block col returned block column: " + blockCol);
        if (blockCol >= 0)
        {
            int block = sysrand.Next(100);
            if (block > confusion)
            {
                board.AddPiece(blockCol, playerColor);
                Debug.Log(playerColor + "blocking col: " + blockCol);
                //Debug.Log(playerColor + " blocked the other players win :-)");
                return true;
            }
            //else
            //{
            Debug.Log(playerColor + " got confused");
            //}
        }
        
        

        for (int i = 0; i < board.GetHorizontalSize(); i++)
        {
            var score = board.CheckColScore(i, playerColor);

            if (score == tempScore)
            {
                // Debug.Log("adding " + i + " to possible cols");
                possibleColsToPlay.Add(i);
            }
            else if (score > tempScore)
            {
                // Debug.Log("score " + score + " is new highscore at " + i);
                possibleColsToPlay.Clear();
                possibleColsToPlay.Add(i);
                tempScore = score;
            }
            // Debug.Log("column " + i + " has a value of " + tempScore);
        }
        int indexToPlay = sysrand.Next(possibleColsToPlay.Count);
        //Debug.Log("amount: " + possibleColsToPlay.Count + " index chosen: " + possibleColsToPlay[indexToPlay] + " Highscore: " + tempScore);
        int colToPlay = possibleColsToPlay[indexToPlay];
        Debug.Log(playerColor + "playing col: " + colToPlay);
        board.AddPiece(colToPlay, playerColor);

        //Debug.Log("______________________________________" + playerName + " is playing column " + colToPlay);
        return true;
    }

    public int canOtherPlayerWin()
    {
        
        for (int i = 0; i < board.GetHorizontalSize(); i++)
        {
            var tmpScore = board.CheckColScore(i, opponentColor);
            Debug.Log("it was not " + playerColor + ", other players tmp score for " + i + " is " + tmpScore);
            if (tmpScore > 3)
            {
                return i;
            }
        }
        return -1;
    }

    public void SetNextCol(int clickInput)
    {
        return;
    }
}
