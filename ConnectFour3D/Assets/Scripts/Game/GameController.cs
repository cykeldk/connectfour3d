using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public BoardInterface board;
    public int VerticalSize;
    public int HorizontalSize;
    public GameObject FieldPrefab;
    public GameObject white;
    public GameObject black;
    public int lastCol;
    public int scoreToWin;

    public PlayerInterface currentPlayer;
    public PlayerInterface waitingPlayer;
    private bool gameOver = false;
    private bool turnPlayed = false;
    int turnNumber = 0;


    // variables used for testing
    //public int addPieces;
    //public bool colsTested = false;
    //public int[] colsToTest = {5};



    // Use this for initialization
    void Start()
    {
        var difficulty = PreGameSettings.getDifficulty();
        board = new BoardControl(6, 4, FieldPrefab, white, black);
        currentPlayer = new AiControl(difficulty, board, "white");
        waitingPlayer = new HumanPlayerControl(board, "player1", "black");
    }



    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {

            turnPlayed = currentPlayer.PlayTurn();
            if (turnPlayed)
            {
                if (checkWin())
                {
                    Debug.Log("------------------------------------------------" + currentPlayer.GetColor() + " wins");
                    if (currentPlayer.IsAi())
                    {
                        GameOverState.gameResult = "The AI level " + PreGameSettings.getDifficulty() + " won";
                    }
                    else
                    {
                        GameOverState.gameResult = "Congratulations " + currentPlayer.GetName() + " you beat AI level " + PreGameSettings.getDifficulty();
                    }
                    
                    gameOver = true;
                    //Canvas.FindObjectOfType<GUIText>().text = "the winner is " + currentPlayer.GetColor();
                    //GetComponent<Canvas>().GetComponent<GUIText>().text = 
                }
                else
                {
                    turnNumber++;
                    togglePlayer();
                    if (turnNumber >= HorizontalSize * VerticalSize)
                    {
                        GameOverState.gameResult = "";
                        gameOver = true;
                    }
                }
            }
        }
        else
        {
            SceneManager.LoadScene(0);
            // show win, loss, draw and ask to play again..

        }

        //if (currentPlayer.Equals("white"))
        //{
        //    int lastCol = PlayTurn();

        //    if (checkWin(lastCol))
        //    {
        //        Debug.Log("------------------------------------------------" + currentPlayer + " wins");
        //    }
        //    else
        //    {
        //        currentPlayer = "black";
        //    }
        //}
        //else
        //{
        //    int lastCol = PlayTurn();
        //    if (checkWin(lastCol))
        //    {
        //        Debug.Log("------------------------------------------------" + currentPlayer + " wins");
        //    }
        //    currentPlayer = "white";
        //}
    }


    private bool checkWin()
    {
        int[][] directions = board.GetDirections();
        // Debug.Log("checking for winner on col" + col);
        for (int col = 0; col < HorizontalSize; col++)
        {
            for (int i = 0; i < directions.Length / 2; i++)
            {
                var direction1 = directions[i];
                var direction2 = directions[directions.Length - (i + 1)];
                int row = board.findRowInCol(col) - 1;
                var scoreDirection1 = board.checkScoreFromPoint(1, col, row, direction1[0], direction1[1], currentPlayer.GetColor());
                var scoreDirection2 = board.checkScoreFromPoint(1, col, row, direction2[0], direction2[1], currentPlayer.GetColor());
                var tempScore = (scoreDirection1 + scoreDirection2 - 1);
                //Debug.Log("Point [" + col + ", " + row + "] Direction: [x:" + direction1[0] + "; y: " + direction1[1] + "] has a score of: " + scoreDirection1);
                //Debug.Log("Point [" + col + ", " + row + "] Direction: [x:" + direction2[0] + "; y: " + direction2[1] + "] has a score of: " + scoreDirection2);
                //Debug.Log("score direction1: " + scoreDirection1);
                //Debug.Log("score opposite: " + scoreDirection2);
                if (tempScore >= scoreToWin)
                {
                    
                    // Debug.Log(playerName + " is the winner");
                    return true;
                }
            }
        }
        return false;
    }






    /*
    private float checkVertical(int col, int row)
    {
        int column = col;
        float res = 0.0f;
        if (row > 0)
        {
            if (fields[column][row - 1].GetPlayer().Equals(currentPlayer))
            {
                res += 1f;
                return res += checkVertical(col, row - 1);
            }
        }
        return res;
    }


    private float checkHorizontal(int col, int row, bool increment)
    {
        float res = 0.0f;
        if (row > VerticalSize - 1) return res;
      
        string side;
        if (increment) side = "right";
        else side = "left";

        // Debug.Log("checking horizontal " + side + " from [" + (col - 1)+ ", " + row + "]");
      
        int column = col - 1;
        // changed below to minus two to find "off-by-1" error... ... ...
        if (increment && column < HorizontalSize - 1)
        {
            if (fields[column + 1][row].GetPlayer().Equals(currentPlayer))
            {
                res += 1f;

                return res += checkHorizontal(col + 1, row, increment);
            }
        }
        if (!increment && column > 0)
        {
            if (fields[column - 1][row].GetPlayer().Equals(currentPlayer))
            {
                res += 1f;
                return res += checkHorizontal(col - 1, row, increment);
            }
        }
        return res;
    }

    private float checkDiagonalUp(int col, int row, bool increment)
    {
        int column = col - 1;
        float res = 0.0f;
        if (increment && column < HorizontalSize - 1 && row < VerticalSize - 1)
        {
            if (fields[column + 1][row + 1].GetPlayer().Equals(currentPlayer))
            {
                res += 1f;
                return res += checkDiagonalUp(col + 1, row + 1, increment);
            }
        }
        if (!increment && column > 0 && row > 0)
        {
            if (fields[column - 1][row - 1].GetPlayer().Equals(currentPlayer))
            {
                res += 1f;
                return res += checkDiagonalUp(col - 1, row - 1, increment);
            }
        }
        return res;
    }

    private float checkDiagonalDown(int col, int row, bool increment)
    {
        int column = col - 1;
        float res = 0.0f;
        if (increment && column > 0 && row < VerticalSize - 1)
        {
            if (fields[column - 1][row + 1].GetPlayer().Equals(currentPlayer))
            {
                res += 1f;
                return res += checkDiagonalDown(col - 1, row + 1, increment);
            }

        }
        if (!increment && column < VerticalSize && row > 0)
        {
            if (fields[column + 1][row - 1].GetPlayer().Equals(currentPlayer))
            {
                res += 1f;
                return res += checkDiagonalDown(col + 1, row - 1, increment);
            }
        }
        return res;
    }
    */


    private void togglePlayer()
    {
        PlayerInterface tmpPlayer = waitingPlayer;
        //Debug.Log("tmp player is ai? " + tmpPlayer.IsAi());
        waitingPlayer = currentPlayer;
        //Debug.Log("waitingPlayer player is ai? " + waitingPlayer.IsAi());
        currentPlayer = tmpPlayer;
        //Debug.Log("currentPlayer player is ai? " + currentPlayer.IsAi());
    }

    public void TestMyShit()
    {
        //Debug.Log("ShitTest Worked");
    }
    /*
    private float scoreForCenter(int col, int row)
    {
        int vertical = Mathf.Min(row, Mathf.Abs(VerticalSize - row));
        int horizontal = Mathf.Min(col, Mathf.Abs(HorizontalSize - col));
        float tempScore = vertical + horizontal;
        if (tempScore <= 0) tempScore = 0.01f;
        // return 1 - 1 / tempScore;
        return 0;
    }
    */

}
