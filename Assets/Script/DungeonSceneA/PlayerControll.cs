using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// attach to PlayerObject
/// </summary>
public class PlayerControll : MovingObject
{
    [HideInInspector]
    public int Horizontal;
    [HideInInspector]
    public int Vertical;
    public Sprite[] playerSprites;
    [HideInInspector]
    public bool isTurn;

    private void Update ()
    {
        if (!GameManager.Singleton.playersTurn) return;

        // int Horizontal = 0;
        // int Vertical = 0;

        // Horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
        // Vertical = (int) (Input.GetAxisRaw ("Vertical"));

        if (Horizontal != 0 || Vertical != 0)
        {
            // Change player Sprite
            if (Vertical == 1)
            {
                base.spriteRenderer.sprite = playerSprites[(int) spriteDir.up];
            }
            else if (Vertical == -1)
            {
                base.spriteRenderer.sprite = playerSprites[(int) spriteDir.down];
            }
            else if (Horizontal == -1)
            {
                base.spriteRenderer.sprite = playerSprites[(int) spriteDir.left];
            }
            else if (Horizontal == 1)
            {
                base.spriteRenderer.sprite = playerSprites[(int) spriteDir.right];
            }

            if (!isTurn) { AttemptMove (Horizontal, Vertical); }
            Horizontal = 0;
            Vertical = 0;
        }
    }

    /// <summary>
    /// colliderのistriggerがオンのものと衝突した時呼ばれる
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Stairs")
        { //追加!!
            Invoke ("Restart", 0.5f);
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