using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// attach to PlayerObject
/// </summary>
public class Player : MovingObject
{
    private void Update ()
    {
        if (!GameManager.Singleton.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
        vertical = (int) (Input.GetAxisRaw ("Vertical"));

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove (horizontal, vertical);
        }
    }

    /// <summary>
    /// colliderのistriggerがオンのものと衝突した時呼ばれる
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other){
    if(other.tag == "Stairs") { //追加!!
        Invoke("Restart",0.5f);
        this.enabled = false;
    } 
}

    protected override void AttemptMove (int xDir, int yDir)
    {
        GameManager.Singleton.playersTurn = false;
        base.AttemptMove (xDir, yDir);
    }

    private void Restart ()
    {
        //reload scene
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex, LoadSceneMode.Single);
    }
}