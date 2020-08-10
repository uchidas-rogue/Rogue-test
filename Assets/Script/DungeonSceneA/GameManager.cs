using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// attach to GameManagerObject
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    private BoardManager boardScript;

    [HideInInspector]
    public bool playersTurn = true; //trueならプレイヤー移動可能

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
    }

    void InitGame ()
    {
        boardScript.SetupScene ();
    }

    /// <summary>
    /// ゲーム起動時に１度だけ実行
    /// sceneLoadedにonsceneloadeを追記
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Singleton.InitGame();
    }


}