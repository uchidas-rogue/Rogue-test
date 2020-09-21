using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    public string DirectionString;
    public Sprite[] ButtonSprites;

    private Player playerScript;
    private bool isButtonDown;
    private int downCount = 0;

    private void MovePlayer ()
    {
        this.playerScript = GameObject.Find ("Player").GetComponent<Player> ();
        switch (DirectionString)
        {
            case "up":
                this.playerScript.vertical = 1;
                break;
            case "down":
                this.playerScript.vertical = -1;
                break;
            case "left":
                this.playerScript.horizontal = -1;
                break;
            case "right":
                this.playerScript.horizontal = 1;
                break;
            case "leftup":
                this.playerScript.horizontal = -1;
                this.playerScript.vertical = 1;
                break;
            case "leftdown":
                this.playerScript.horizontal = -1;
                this.playerScript.vertical = -1;
                break;
            case "rightup":
                this.playerScript.horizontal = 1;
                this.playerScript.vertical = 1;
                break;
            case "rightdown":
                this.playerScript.horizontal = 1;
                this.playerScript.vertical = -1;
                break;
            case "turn":
                this.playerScript.isTurn = !this.playerScript.isTurn;
                if (this.playerScript.isTurn)
                {
                    GetComponent<Image>().sprite = ButtonSprites[1];
                }
                else
                {
                    GetComponent<Image>().sprite = ButtonSprites[0];
                }
                break;
        }
    }

    void Update ()
    {
        if (isButtonDown)
        { //長押し判別のためのウエイト
            downCount++;
            if (downCount > 100) { MovePlayer (); }
        }
    }

    public void OnClicked ()
    {
        if (!isButtonDown) { MovePlayer (); }
    }

    public void OnButtonDown ()
    {
        isButtonDown = true;
    }

    public void OnButtonUp ()
    {
        isButtonDown = false;
        downCount = 0;
    }

}