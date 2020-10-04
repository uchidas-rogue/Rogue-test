using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;

public class MinimapControll : MonoBehaviour
{
    /// <summary>
    /// offsetMin > 0 offsetMax > 1 , 0(left,bottom)1(right,-top)
    /// </summary>
    private Vector2[] miniSize = { new Vector2 (1435, 450), new Vector2 (0, -370) };
    private Vector2[] pickupSize = { new Vector2 (0, 0), new Vector2 (0, 0) };
    private bool isPickup;
    private StringBuilder mapStringBuilder = new StringBuilder();

    #region Must Get in Awake 
    private RectTransform minimapRect;
    private TextMeshProUGUI mapTextGUI;
    private BoardManager boardScript;
    #endregion

    void Awake ()
    {
        minimapRect = GetComponent<RectTransform> ();
        mapTextGUI = transform.GetChild (0).gameObject.GetComponent<TextMeshProUGUI> ();
        boardScript = GameManager.Singleton.GetComponent<BoardManager>();
    }

    private void CheckFloor (int j, int i)
    {
        if (boardScript.Maze[j, i, 1] == 0 && boardScript.Maze[j, i, 0] == 2)
        {
            boardScript.Maze[j, i, 1] = 1;
            TurnToWalked (j, i);
        }
        else
        {
            boardScript.Maze[j, i, 1] = 1;
        }
    }

    private void TurnToWalked (int j, int i)
    {
        if (boardScript.Maze[j, i, 0] == 2)
        {
            CheckFloor (j - 1, i - 1);
            CheckFloor (j - 1, i);
            CheckFloor (j - 1, i + 1);
            CheckFloor (j, i - 1);
            CheckFloor (j, i + 1);
            CheckFloor (j + 1, i - 1);
            CheckFloor (j + 1, i);
            CheckFloor (j + 1, i + 1);
        }
        else
        {
            boardScript.Maze[j - 1, i - 1, 1] = 1;
            boardScript.Maze[j - 1, i, 1] = 1;
            boardScript.Maze[j - 1, i + 1, 1] = 1;
            boardScript.Maze[j, i - 1, 1] = 1;
            boardScript.Maze[j, i + 1, 1] = 1;
            boardScript.Maze[j + 1, i - 1, 1] = 1;
            boardScript.Maze[j + 1, i, 1] = 1;
            boardScript.Maze[j + 1, i + 1, 1] = 1;
        }
    }

    public void SetMiniMapString ()
    {
        mapStringBuilder.Clear();

        TurnToWalked ((int) GameManager.Singleton.PlayerObject.transform.position.x, (int) GameManager.Singleton.PlayerObject.transform.position.y);

        for (int i = boardScript.width - 1; i >= 0; i--)
        {
            for (int j = 0; j < boardScript.height; j++)
            {
                if (j == GameManager.Singleton.PlayerObject.transform.position.x && i == GameManager.Singleton.PlayerObject.transform.position.y)
                { //player position
                    mapStringBuilder.Append("<color=yellow>●</color>");
                }
                else if (boardScript.Maze[j, i, 0] == 3)
                { //exit position
                    if (boardScript.Maze[j, i, 1] == 1)
                    {
                        mapStringBuilder.Append("<color=green>■</color>");
                    }
                    else
                    {
                        mapStringBuilder.Append("   ");
                    }
                }
                else if (boardScript.Maze[j, i, 0] == 1 || boardScript.Maze[j, i, 0] == 2)
                { //floor position
                    if (boardScript.Maze[j, i, 1] == 1)
                    {
                        mapStringBuilder.Append("<color=blue>■</color>");
                    }
                    else
                    {
                        mapStringBuilder.Append("   ");
                    }
                }
                else
                { //wall position
                    if (boardScript.Maze[j, i, 1] == 1)
                    {
                        mapStringBuilder.Append("■");
                    }
                    else
                    {
                        mapStringBuilder.Append("   ");
                    }
                }
            }
            mapStringBuilder.AppendLine("");
        }
        mapTextGUI.text = mapStringBuilder.ToString();
    }

    private void ChangeMapSize ()
    {
        if (isPickup)
        {
            minimapRect.offsetMin = pickupSize[0];
            minimapRect.offsetMax = pickupSize[1];
            mapTextGUI.fontSize = (float) 70;
        }
        else
        {
            minimapRect.offsetMin = miniSize[0];
            minimapRect.offsetMax = miniSize[1];
            mapTextGUI.fontSize = (float) 20;
        }
    }

    public void OnClicked ()
    {
        isPickup = !isPickup;
        ChangeMapSize ();
    }
}