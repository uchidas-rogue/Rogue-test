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
    // GameManagerのシングルトン
    public static GameManager Singleton;

    // Public GameObjects
    #region Objects
    [HideInInspector]
    public GameObject MessageWindowObject;

    [HideInInspector]
    public GameObject PlayerObject;

    [HideInInspector]
    public GameObject MiniMapObject;

    [HideInInspector]
    public GameObject FloorNumTextObject;

    #endregion

    // Public Parameters
    #region Params
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
                MiniMapObject.GetComponent<MinimapControll> ().SetMiniMapString ();
            }
            _playersTurn = value;
        }
    }

    #endregion

    private BoardManager boardScript;
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
        // the gameobject(GameManager is attached to) dont destroy while game awaking
        DontDestroyOnLoad (gameObject);

        // BoardManager
        boardScript = GetComponent<BoardManager> ();

        // sceneLoadedにonsceneloadeを追記
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    ///  シーン開始時に必ず行う処理
    /// </summary>
    void InitGame ()
    {
        GetObjects ();

        // disable messagewindow
        this.MessageWindowObject.SetActive (false);

        // make map
        boardScript.SetupScene ();
        // make minimap
        MiniMapObject.GetComponent<MinimapControll> ().SetMiniMapString ();

        SetStringToFloorNumText();

        SetStringToMessageWindow ("ゲームが開始されました\nここから冒険は始まる！！");
        SetStringToMessageWindow ("二回目の呼び出しです\nここから冒険は再び始まる！！");
    }

    /// <summary>
    ///  シーンのオブジェクトを取得する
    /// </summary>
    private void GetObjects ()
    {
        PlayerObject = GameObject.Find ("Player").gameObject;
        MessageWindowObject = GameObject.Find ("MessageWindow").gameObject;
        MiniMapObject = GameObject.Find ("MiniMap").gameObject;
        FloorNumTextObject = GameObject.Find ("FloorNumText").gameObject;
    }

    private void SetStringToMessageWindow (string msg)
    {
        MessageWindowObject.GetComponent<MessageWindowControll> ().message = msg;
    }

    private void SetStringToFloorNumText()
    {
        this.floorNum++;
        FloorNumTextObject.GetComponent<TextMeshProUGUI> ().text = floorNum + "F";
    }

    static private void OnSceneLoaded (Scene arg0, LoadSceneMode arg1)
    {
        Singleton.InitGame ();
    }

}