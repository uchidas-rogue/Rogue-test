﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attach to GameManagerObject
/// </summary>
public class BoardManager : MonoBehaviour
{
    public int height;
    public int width;

    [HideInInspector]
    public int[, , ] Maze;

    //floor prefab 
    public GameObject[] floorTiles;
    //wall prefab
    public GameObject[] wallTiles;
    //stairs prefab
    public GameObject stairsTile;
    //Tiles parent object 
    private Transform boardHolder;

    private void SetTiles (GameObject Tiles, int x, int y)
    {
        GameObject toInstantiate = Tiles;
        //Clone the Tiles
        GameObject instance = Instantiate (toInstantiate,
            new Vector3 (x, y, 0),
            Quaternion.identity,
            boardHolder) as GameObject;
    }

    private void BoardSetup ()
    {
        //create parent object
        this.boardHolder = new GameObject ("Board").transform;

        MakeMaze makeMaze = new MakeMaze (width, height);
        makeMaze.DigMaze ();

        this.Maze = makeMaze.Maze;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                switch (this.Maze[x, y, 0])
                {
                    case 0:
                        SetTiles (wallTiles[8], x, y);
                        break;
                    case 1:
                        SetTiles (floorTiles[0], x, y);
                        break;
                    case 2:
                        SetTiles (floorTiles[0], x, y);
                        break;
                    case 3:
                        SetTiles (stairsTile, x, y);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void SetupScene ()
    {
        BoardSetup ();
    }

}