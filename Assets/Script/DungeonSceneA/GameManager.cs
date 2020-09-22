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
    public bool playersTurn = true; //trueならプレイヤー移動可能
    private TextMeshProUGUI floorNumText;
    private GameObject messageWindow;
    private GameObject charaNameWindow;
    private int floorNum = 0;

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

    static private void OnSceneLoaded (Scene arg0, LoadSceneMode arg1)
    {
        //get messagewindowß
        Singleton.messageWindow = GameObject.Find ("MessageWindow");
        Singleton.charaNameWindow = GameObject.Find ("CharaNameWindow");
        //disable messagewindow
        Singleton.messageWindow.SetActive (false);
        Singleton.charaNameWindow.SetActive (false);

        Singleton.floorNum++;
        Singleton.InitGame ();

        //Singleton.SetStringToMessageWindow(
        //    "階層が変わりました。" + "\n"
        //    + "ここは" + Singleton.floorNum + "Fです。");

    }

}