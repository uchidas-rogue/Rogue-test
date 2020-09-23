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
    private BoardManager boardScript;

    [HideInInspector]
    public bool _playersTurn = true; //trueならプレイヤー移動可能

    public bool playersTurn 
    {
        get 
        {
            return _playersTurn;
        }
        set
        {
            if (_playersTurn != value)
            {//if change false
                SendMiniMapString();
            }
            _playersTurn = value;
        }
    }
    private TextMeshProUGUI floorNumText;
    private GameObject messageWindow;
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
        this.floorNumText = GameObject.Find ("FloorNumText").GetComponent<TextMeshProUGUI> ();
        this.floorNumText.text = floorNum + "F";

        boardScript.SetupScene ();
    }

    private void SetStringToMessageWindow (string msg)
    {
        messageWindow.SetActive (true);
        messageWindow.GetComponent<MessageSend> ().message = msg;
    }

    private void SendMiniMapString ()
    {
        Transform player = GameObject.Find("Player").transform;
        mapString = "";
        for (int i = boardScript.width - 1; i >= 0; i--)
        {
            for (int j = 0; j < boardScript.height; j++)
            {
                if ( j == player.position.x && i== player.position.y)
                {
                    mapString += "●";
                }
                else if (boardScript.Maze[j, i] == 3)
                {
                    mapString += "X";
                }
                else if (boardScript.Maze[j, i] == 1 || boardScript.Maze[j, i] == 2)
                {
                    mapString += "□";
                }
                else
                {
                    mapString += "■";
                }
            }
            mapString += "\n";
        }

        GameObject.Find ("Maptext").GetComponent<TextMeshProUGUI> ().text = mapString;
    }

    static private void OnSceneLoaded (Scene arg0, LoadSceneMode arg1)
    {
        //get messagewindow
        Singleton.messageWindow = GameObject.Find ("MessageWindow");
        //disable messagewindow
        Singleton.messageWindow.SetActive (false);

        Singleton.floorNum++;
        Singleton.InitGame ();

        Singleton.SendMiniMapString ();

        Singleton.SetStringToMessageWindow ("ゲームが開始されました\nここから冒険は始まる！！");
        Singleton.SetStringToMessageWindow ("二回目の呼び出しです\nここから冒険は再び始まる！！");

    }

}