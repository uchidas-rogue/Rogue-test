﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// moving object abstract
/// </summary>
public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.05f;
    private Rigidbody2D rb2d;
    private float inverseMoveTime;

    protected virtual void Start ()
    {
        this.rb2d = GetComponent<Rigidbody2D> ();
        this.inverseMoveTime = 1f / moveTime;
    }

    protected IEnumerator SmoothMovement (Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (float.Epsilon < sqrRemainingDistance)
        {
            Vector3 newPosition = Vector3.MoveTowards (rb2d.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition (newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        GameManager.Singleton.playersTurn = true;
    } 

    protected bool Move (int xDir, int yDir)
    {
        //移動先に何らかの物体があるかどうかチェックする。
        //何もない場合はSmoothMovementを呼んで移動する。
        //移動した場合はtrueを返す。
        Vector2 start = transform.position;
        Vector2 end = (start + new Vector2 (xDir, yDir));

        StartCoroutine (SmoothMovement (end));
        return true;
    }

    protected virtual void AttemptMove (int xDir, int yDir)
    {
        //MoveやOnCantMoveといった移動処理に関する一連の処理を呼び出す。
        //外部のクラスからこのオブジェクトを移動させるための入り口。
        Move(xDir, yDir);
    }
}