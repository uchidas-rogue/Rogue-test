using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// attach to GameManagerObject
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    [HideInInspector]
    private GameObject MessageWindowObject;
    [HideInInspector]
    private GameObject PlayerObject;
    [HideInInspector]
    private GameObject MiniMapObject;
    private BoardManager boardScript;

    private bool _playersTurn = true; //trueならプレイヤー移動可能

    [HideInInspector]
    public bool playersTurn
    {
        get
        {
            return _playersTurn;
        }
        set
        {
            if (_playersTurn != value)
            {
                SetMiniMapString ();
            }
            _playersTurn = value;
        }
    }
    private TextMeshProUGUI floorNumText;
    private int floorNum = 0;
    private string mapString;

    //Awake call when Game start
    void Awake ()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else if (Singleton != this)
        {
            Destroy (gameObject);
        }
        //the gameobject(GameManager is attached to) dont destroy while game awaking
        DontDestroyOnLoad (gameObject);

        //BoardManager
        boardScript = GetComponent<BoardManager> ();

        //sceneLoadedにonsceneloadeを追記
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void InitGame ()
    {
        GetObjectinInitGame ();

        //make map
        boardScript.SetupScene ();
        //make minimap
        Singleton.SetMiniMapString ();

        //disable messagewindow
        this.MessageWindowObject.SetActive (false);

        this.floorNum++;
        this.floorNumText = GameObject.Find ("FloorNumText").GetComponent<TextMeshProUGUI> ();
        this.floorNumText.text = floorNum + "F";

        Singleton.SetStringToMessageWindow ("ゲームが開始されました\nここから冒険は始まる！！");
        Singleton.SetStringToMessageWindow ("二回目の呼び出しです\nここから冒険は再び始まる！！");
    }

    private void GetObjectinInitGame ()
    {
        PlayerObject = GameObject.Find ("Player").gameObject;
        MessageWindowObject = GameObject.Find ("MessageWindow").gameObject;
        MiniMapObject = GameObject.Find ("MiniMap").gameObject;
    }

    private void SetStringToMessageWindow (string msg)
    {
        MessageWindowObject.SetActive (true);
        MessageWindowObject.GetComponent<MessageSend> ().message = msg;
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

    private void SetMiniMapString ()
    {
        mapString = "";

        TurnToWalked ((int) PlayerObject.transform.position.x, (int) PlayerObject.transform.position.y);

        for (int i = boardScript.width - 1; i >= 0; i--)
        {
            for (int j = 0; j < boardScript.height; j++)
            {
                if (j == PlayerObject.transform.position.x && i == PlayerObject.transform.position.y)
                { //player position
                    mapString += "<color=yellow>●</color>";
                }
                else if (boardScript.Maze[j, i, 0] == 3)
                { //exit position
                    if (boardScript.Maze[j, i, 1] == 1)
                    {
                        mapString += "<color=green>■</color>";
                    }
                    else
                    {
                        mapString += "   ";
                    }
                }
                else if (boardScript.Maze[j, i, 0] == 1 || boardScript.Maze[j, i, 0] == 2)
                { //floor position
                    if (boardScript.Maze[j, i, 1] == 1)
                    {
                        mapString += "<color=blue>■</color>";
                    }
                    else
                    {
                        mapString += "   ";
                    }
                }
                else
                { //wall position
                    if (boardScript.Maze[j, i, 1] == 1)
                    {
                        mapString += "■";
                    }
                    else
                    {
                        mapString += "   ";
                    }
                }
            }
            mapString += "\n";
        }
        MiniMapObject.GetComponent<MinimapControll> ().SetmapText (mapString);
    }

    static private void OnSceneLoaded (Scene arg0, LoadSceneMode arg1)
    {
        //get messagewindow
        //Singleton.MessageWindowObject = GameObject.Find ("MessageWindowObject");
        Singleton.InitGame ();
    }

}