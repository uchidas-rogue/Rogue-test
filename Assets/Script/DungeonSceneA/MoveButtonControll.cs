using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonControll : MonoBehaviour
{
    public string DirectionString;
    public Sprite[] ButtonSprites;

    private PlayerControll playerScript;
    private bool isTurn;
    private bool isButtonDown;
    private int downCount = 0;

    private void MovePlayer ()
    {
        this.playerScript = GameManager.Singleton.PlayerObject.GetComponent<PlayerControll>();
        switch (DirectionString)
        {
            case "up":
                this.playerScript.Vertical = 1;
                break;
            case "down":
                this.playerScript.Vertical = -1;
                break;
            case "left":
                this.playerScript.Horizontal = -1;
                break;
            case "right":
                this.playerScript.Horizontal = 1;
                break;
            case "leftup":
                this.playerScript.Horizontal = -1;
                this.playerScript.Vertical = 1;
                break;
            case "leftdown":
                this.playerScript.Horizontal = -1;
                this.playerScript.Vertical = -1;
                break;
            case "rightup":
                this.playerScript.Horizontal = 1;
                this.playerScript.Vertical = 1;
                break;
            case "rightdown":
                this.playerScript.Horizontal = 1;
                this.playerScript.Vertical = -1;
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
            if (downCount > 50) { MovePlayer (); }
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