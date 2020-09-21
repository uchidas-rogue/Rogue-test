using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    public string DirectionString;

    private Player playerScript;

    public void OnClicked ()
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
        }
    }
}