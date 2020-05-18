using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int height = 11;
    public int width = 11;

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

    private enum Direction
    {
        none = 0,
        up = 1,
        down = 2,
        left = 3,
        right = 4
    }

    private int[,] Maze;

    private void DigMaze()
    {
        Maze = new int[width,height];

        Direction Dir = Direction.none;
        int x = 0;
        int y = 0;

        Maze[x,y] = 1;

        for (int i=0;i<121;i++)
        {
            while (Dir == Direction.none)
            {
                Dir = (Direction) Random.Range(0,4);
            }
            switch (Dir)
            {
                case Direction.up:
                    if ((y + 2 > -1 && y + 2 < height) && (Maze[x,y+2] != 1))
                    {
                        y++;
                        Maze[x,y] = 1;
                        y++;
                        Maze[x,y] = 1;
                    }
                    else
                    {               
                        while (true)
                        {
                            x = Random.Range(0,width-1);
                            y = Random.Range(0,height-1);
                            if (x % 2 == 0 && y % 2 == 0)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case Direction.down:
                    if ((y - 2 > -1 && y - 2 < height) && (Maze[x,y-2] != 1))
                    {
                        y--;
                        Maze[x,y] = 1;
                        y--;
                        Maze[x,y] = 1;
                    }
                    else
                    {
                        while (true)
                        {
                            x = Random.Range(0,width-1);
                            y = Random.Range(0,height-1);
                            if (x % 2 == 0 && y % 2 == 0)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case Direction.right:
                    if ((x + 2 > -1 && x + 2 < width) && (Maze[x+2,y] != 1))
                    {
                        x++;
                        Maze[x,y] = 1;
                        x++;
                        Maze[x,y] = 1;
                    }
                    else
                    {
                        while (true)
                        {
                            x = Random.Range(0,width-1);
                            y = Random.Range(0,height-1);
                            if (x % 2 == 0 && y % 2 == 0)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case Direction.left:
                    if ((x - 2 > -1 && x - 2 < width) && (Maze[x-2,y] != 1))
                    {
                        x--;
                        Maze[x,y] = 1;
                        x--;
                        Maze[x,y] = 1;
                    }
                    else
                    {
                        while (true)
                        {
                            x = Random.Range(0,width-1);
                            y = Random.Range(0,height-1);
                            if (x % 2 == 0 && y % 2 == 0)
                            {
                                break;
                            }
                        }
                    }
                    break;
            }  
        }
    }

    private void BoardSetup2()
    {
        //create parent object
        this.boardHolder = new GameObject("Board").transform;
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //floor
                //if floor at random [0] => [Random.Range(0, floorTiles.Length)]
                if (Maze[x,y] == 1)
                {
                    GameObject toInstantiate = floorTiles[0];

                    //Clone the Tiles
                    GameObject instance = Instantiate(toInstantiate, 
                                                        new Vector3(x, y, 0), 
                                                        Quaternion.identity,
                                                        boardHolder) as GameObject;
                }
                
            }
        }
    }

    public void SetupScene()
    {
        //this.height = Random.Range(5,10);
        //this.width = Random.Range(5,10);
        DigMaze();
        BoardSetup2();
    }

}
