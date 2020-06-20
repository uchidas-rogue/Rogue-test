using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int height;
    public int width;

    //floor prefab 
    public GameObject[] floorTiles;
    //wall prefab
    public GameObject[] wallTiles;
    //Tiles parent object 
    private Transform boardHolder;

    private void BoardSetup()
    {
        //create parent object
        this.boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < width + 1; x++)
        {
            for (int y = -1; y < height + 1; y++)
            {
                //floor
                //if floor at random [0] => [Random.Range(0, floorTiles.Length)]
                GameObject toInstantiate = floorTiles[0];
                
                if(x == -1 && y == height)
                {//leftup
                    toInstantiate = wallTiles[0];
                }
                else if (x == -1 && y == -1)
                {//leftdown
                    toInstantiate = wallTiles[1];
                }
                else if (x == width && y == -1)
                {//rightdown
                    toInstantiate = wallTiles[2];
                }
                else if (x == width && y == height)
                {//rightup
                    toInstantiate = wallTiles[3];
                }
                else if(x == -1)
                {//left
                    toInstantiate = wallTiles[4];
                }
                else if (y == -1)
                {//down
                    toInstantiate = wallTiles[5];
                }
                else if (x == width)
                {//right
                    toInstantiate = wallTiles[6];
                }
                else if (y == height)
                {//up
                    toInstantiate = wallTiles[7];
                }
                //Clone the Tiles
                GameObject instance = Instantiate(toInstantiate, 
                                                    new Vector3(x, y, 0), 
                                                    Quaternion.identity,
                                                    boardHolder
                                                    ) as GameObject;
            }
        }
    }

    private void SetTiles(GameObject Tiles, int x, int y)
    {
        GameObject toInstantiate = Tiles;
        //Clone the Tiles
        GameObject instance = Instantiate(toInstantiate, 
                                            new Vector3(x, y, 0), 
                                            Quaternion.identity,
                                            boardHolder) as GameObject;
    }

    private void BoardSetup2()
    {
        //create parent object
        this.boardHolder = new GameObject("Board").transform;

        MakeMaze makeMaze = new MakeMaze(width,height);
        makeMaze.DigMaze();
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (makeMaze.Maze[x,y] == 1)
                {
                    //floor
                    //if floor at random [0] => [Random.Range(0, floorTiles.Length)]
                    SetTiles(floorTiles[0],x,y);
                }
                else if (makeMaze.Maze[x,y] == 2)
                {
                    SetTiles(floorTiles[0],x,y);
                }
                
            }
        }
    }

    public void SetupScene()
    {
        BoardSetup2();
    }

}
