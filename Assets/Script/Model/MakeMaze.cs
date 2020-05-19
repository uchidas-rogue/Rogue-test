using System.Collections;
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
        while (true)
        {
            direction = (Direction) Random.Range(-2,2);
            if (direction != Direction.none){break;}
        }
    }

    private void ChangePosition()
    {
        while (true)
        {//change position where floor tiles exist
            x = Random.Range(0,width-1);
            y = Random.Range(0,height-1);
            if (x % 2 == 1 && y % 2 == 1 && Maze[x,y] == 1){break;}
        }
    }

    public void DigMaze()
    {
        Maze = new int[width,height];
        //entry position
        x = width - 2;
        y = height - 2;
        //deploy floor tile
        Maze[x,y] = 1;

        for (int i=0;i<1000;i++)
        {
            ChangeDir();

            if (direction == Direction.up || direction == Direction.down)
            {
                if ((y + 2*(int)direction > -1 && y + 2*(int)direction < height) && (Maze[x,y+2*(int)direction] != 1))
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
            else if (direction == Direction.left || direction == Direction.right)
            {
                if ((x + (int)direction > -1 && x + (int)direction < height) && (Maze[x+(int)direction,y] != 1))
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
