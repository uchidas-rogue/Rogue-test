using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMaze
{
    public int[,] Maze;
    private int width;
    private int height;
    private Direction direction;
    private int x = 1;
    private int y = 1;

    private int roomNumber;
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
        for (int i = 0; i < (width-1)/2; i++)
        {
            for (int j = 0; j < (height-1)/2; j++)
            {
                if (CanDig(2*i+1, 2*j+1) && Maze[2*i+1,2*j+1] != 0)
                {
                    x = 2*i+1;
                    y = 2*j+1;
                    return;
                }
            }
        }
    }

    private void ChangePosition0()
    {
        while (true)
        {//change position where floor tiles exist
            x = Random.Range(0,width);
            y = Random.Range(0,height);
            if (x % 2 == 1 && y % 2 == 1 && Maze[x,y] == 0){break;}
        }
    }

    private bool CanDig(int x, int y)
    {
        return ((direction == Direction.up || direction == Direction.down) && (y + 2*(int)direction > -1 && y + 2*(int)direction < height) && (Maze[x,y+2*(int)direction] == 0)
                || (direction == Direction.left || direction == Direction.right) && (x + (int)direction > -1 && x + (int)direction < width) && (Maze[x+(int)direction,y] == 0));
    }

    bool canmakeVir;
    bool canmakeHor;

    private bool CanMakeRoom(int size, int entry)
    {
        canmakeVir = ((direction == Direction.up || direction == Direction.down)
                && ((x-entry > -1 && x-entry+(size-1) < width) && (y-(size-1) > -1 && y+(size-1) < height)
                && (Maze[x-entry,y] == 0) && (Maze[x-entry,y+(size-1)*(int)direction] == 0)
                && (Maze[x-entry+(size-1),y] == 0) && (Maze[x-entry+(size-1),y+(size-1)*(int)direction] == 0)));

        canmakeHor = ((direction == Direction.left || direction == Direction.right)
                && ((y-entry > -1 && y-entry+(size-1) < height) && (x-(size-1) > -1 && x+(size-1)< width)
                && (Maze[x,y-entry] == 0) && (Maze[x+(size-1)*(int)direction/2,y-entry] == 0)
                && (Maze[x,y-entry+(size-1)] == 0) && (Maze[x+(size-1)*(int)direction/2,y-entry+(size-1)] == 0)));

        return (canmakeHor || canmakeVir);
    }

    private bool ISAnyDigPosition()
    {
        for (int i = 0; i < (width-1)/2; i++)
        {
            for (int j = 0; j < (height-1)/2; j++)
            {
                if (CanDig(2*i+1, 2*j+1))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool MakeRoom()
    {
        int size = Random.Range(2,8);
        if (size % 2 == 0)
        {
            size++;
        }
        int entry = Random.Range(0,size+1);
        if (entry % 2 == 1)
        {
            entry--;
        }

        if (CanMakeRoom(size,entry))
        {
            if (direction == Direction.up || direction == Direction.down)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        //(Maze[x-entry,y]),(Maze[x-entry,y+(size-1)])
                        //(Maze[x-entry+size-1,y]),(Maze[x-entry+size-1,y+(size-1)])
                        Maze[x-entry+i,y+j*(int)direction] = 2;
                    }
                }
                roomNumber++;
            }
            if (direction == Direction.left || direction == Direction.right)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        //(Maze[x,y-entry]),(Maze[x+(size-1),y-entry])
                        //(Maze[x,y-entry+size-1]),(Maze[x+(size-1),y-entry+size-1])
                        Maze[x+i*(int)direction/2,y-entry+j] = 2;
                    }
                }
                roomNumber++;
            }
        }

        return CanMakeRoom(size,entry);
    }

    private bool MakePath()
    {
        if (CanDig(x,y))
        {
            if (direction == Direction.up || direction == Direction.down)
            {
                y+=(int)direction;
                Maze[x,y] = 1;
                y+=(int)direction;
                Maze[x,y] = 1;
            }
            if (direction == Direction.left || direction == Direction.right)
            {
                x+=(int)direction/2;
                Maze[x,y] = 1;
                x+=(int)direction/2;
                Maze[x,y] = 1;
            }
        }

        return CanDig(x,y);
    }

    public void DigMaze()
    {
        Maze = new int[width,height];
        //entry position x,y oddnum,oddnum
        x=(width-1)/2 + 1;
        y=(height-1)/2 + 1;

        MakeRoom();

        int cnt = 0;

        while (cnt < 100)
        {
            if (!MakeRoom())
            {
                ChangeDir();
                if (!MakePath())
                {
                    ChangePosition();
                }
            }
            cnt++;
        }

        


        // while (ISAnyDigPosition())
        // {
        //     ChangeDir();
        //     if (Random.Range(0,2) == 0)
        //     {
        //         MakeRoom();
        //     }
        //     else
        //     {
        //         MakePath();
        //     }
        //     if (!CanDig(x,y))
        //     {
        //         ChangePosition();
        //     }
        // }
    }
}
