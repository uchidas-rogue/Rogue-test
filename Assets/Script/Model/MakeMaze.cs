﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMaze
{
    public int[,] Maze;
    private int width;
    private int height;
    private Direction direction;
    private int x;
    private int y;
    public MakeMaze(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
    private enum Direction
    {
        none = 0,
        up = 1,
        down = -1,
        left = -2,
        right = 2
    }

    private void ChangeDir()
    {
        switch (Random.Range(1,5))
        {
            case 1:
                direction = Direction.up;
                break;
            case 2:
                direction = Direction.down;
                break;
            case 3:
                direction = Direction.left;
                break;
            case 4:
                direction = Direction.right;
                break;
        }
    }

    private void ChangePosition()
    {
        while (true)
        {//change position where floor tiles exist
            x = Random.Range(0,width);
            y = Random.Range(0,height);
            if (x % 2 == 1 && y % 2 == 1 && Maze[x,y] != 0){break;}
        }
    }

    private bool CanDig(int x, int y)
    {
        return ((y + 2*(int)Direction.up > -1 && y + 2*(int)Direction.up < height) && (Maze[x,y+2*(int)Direction.up] == 0)
                || (y + 2*(int)Direction.down > -1 && y + 2*(int)Direction.down < height) && (Maze[x,y+2*(int)Direction.down]  == 0)
                || (x + (int)Direction.left > -1 && x + (int)Direction.left < width) && (Maze[x+(int)Direction.left,y]  == 0)
                || (x + (int)Direction.right > -1 && x + (int)Direction.right < width) && (Maze[x+(int)Direction.right,y]  == 0));
    }

    private bool ISAnyDigPosition()
    {
        bool flg = false;
        for (int i = 0; i < (width-1)/2; i++)
        {
            for (int j = 0; j < (height-1)/2; j++)
            {
                if (CanDig(2*i+1, 2*j+1))
                {
                    flg = true;
                }
            }
        }
        return flg;
    }

    public void DigMaze()
    {
        Maze = new int[width,height];
        //entry position x,y oddnum,oddnum
        x = 1;
        y = 1;
        //turn to floor tile
        Maze[x,y] = 1;

        while (ISAnyDigPosition())
        {
            ChangeDir();

            if (direction == Direction.up || direction == Direction.down)
            {
                if ((y + 2*(int)direction > -1 && y + 2*(int)direction < height) && (Maze[x,y+2*(int)direction]  == 0))
                {
                    y+=(int)direction;
                    Maze[x,y] = 1;
                    y+=(int)direction;
                    Maze[x,y] = 1;
                }
                else
                {
                    ChangePosition();
                }
            }
            if (direction == Direction.left || direction == Direction.right)
            {
                if ((x + (int)direction > -1 && x + (int)direction < width) && (Maze[x+(int)direction,y]  == 0))
                {
                    x+=(int)direction/2;
                    Maze[x,y] = 1;
                    x+=(int)direction/2;
                    Maze[x,y] = 1;
                }
                else
                {
                    ChangePosition();
                }
            }
        }
    }
}
