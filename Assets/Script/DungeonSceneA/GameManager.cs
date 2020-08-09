using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// attach to GameManagerObject
///
public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    private BoardManager boardScript;

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
}