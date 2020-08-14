using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// attach to GameManagerObject
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    private BoardManager boardScript;
    private bool first = true;

    [HideInInspector]
    public bool playersTurn = true; //trueならプレイヤー移動可能
    private TextMeshProUGUI floorNumText;
    private int floorNum = 1;

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
        InitGame ();
        //sceneLoadedにonsceneloadeを追記
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void InitGame ()
    {
        this.floorNumText = GameObject.Find("FloorNumText").GetComponent<TextMeshProUGUI>();
        this.floorNumText.text = floorNum + "F";
        boardScript.SetupScene ();
    }

    static private void OnSceneLoaded (Scene arg0, LoadSceneMode arg1)
    {
        if (!Singleton.first)
        {
            Singleton.floorNum++;
            Singleton.InitGame ();
        }
        Singleton.first = false;
    }

}