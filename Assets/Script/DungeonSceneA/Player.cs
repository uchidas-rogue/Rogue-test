using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected override void AttemptMove (int xDir, int yDir)
    {
        base.AttemptMove (xDir, yDir);
        GameManager.Singleton.playersTurn = false;
    }
}