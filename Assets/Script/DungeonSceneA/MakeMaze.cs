using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMaze
{
    public int[,] Maze;
    private int width;
    private int height;
    private Direction direction = Direction.right;
    private int x = 1;
    private int y = 1;
    private int roomSize;
    private int roomEntry;
    private int roomNumber;
    public MakeMaze(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
    private enum Direction
    {
        up = 1,
        down = -1,
        left = -2,
        right = 2
    }

    // check the position is in maze 
    private bool CheckInside0Position(int x, int y)
    {
        return ( x > -1 && x < width) && ( y > -1 && y < width) && (Maze[x,y] == 0);
    }
    
    // check the position where can dig 
    private bool CheckCanDig(int x, int y)
    {
        return ( CheckInside0Position(x,y+2*(int)Direction.up)
                || CheckInside0Position(x,y+2*(int)Direction.down)
                || CheckInside0Position(x+(int)Direction.left,y)
                || CheckInside0Position(x+(int)Direction.right,y)
               );
    }
    private bool CheckCanDig(int x, int y, Direction direction)
    {
        if (direction == Direction.up || direction == Direction.down)
        {
            return CheckInside0Position(x,y+2*(int)direction);
        }else //(direction == Direction.up || direction == Direction.down)
        {
            return CheckInside0Position(x+(int)direction,y);
        }
    }

    private bool CheckCanMakeRoom(int x, int y, int size, int entry)
    {
        return (CheckInside0Position(x-entry,y)
                && CheckInside0Position(x-entry+size-1,y)
                && CheckInside0Position(x-entry,y+(size-1)*(int)Direction.up)
                && CheckInside0Position(x-entry+size-1,y+(size-1)*(int)Direction.up)
                ) 
                ||
                (CheckInside0Position(x-entry,y)
                && CheckInside0Position(x-entry+size-1,y)
                && CheckInside0Position(x-entry,y+(size-1)*(int)Direction.down)
                && CheckInside0Position(x-entry+size-1,y+(size-1)*(int)Direction.down)
                ) 
                ||
                (CheckInside0Position(x,y-entry)
                && CheckInside0Position(x,y-entry+size-1)
                && CheckInside0Position(x+(size-1)*(int)Direction.left/2,y-entry)
                && CheckInside0Position(x+(size-1)*(int)Direction.left/2,y-entry+size-1)
                )
                ||
                (CheckInside0Position(x,y-entry)
                && CheckInside0Position(x,y-entry+size-1)
                && CheckInside0Position(x+(size-1)*(int)Direction.right/2,y-entry)
                && CheckInside0Position(x+(size-1)*(int)Direction.right/2,y-entry+size-1)
                );
    }

    private bool CheckCanMakeRoom(int x, int y, int size, int entry, Direction direction)
    {
        if (direction == Direction.up || direction == Direction.down)
        {
            return (CheckInside0Position(x-entry,y)
                    && CheckInside0Position(x-entry+size-1,y)
                    && CheckInside0Position(x-entry,y+(size-1)*(int)direction)
                    && CheckInside0Position(x-entry+size-1,y+(size-1)*(int)direction)
                    ); 
        }else //(direction == Direction.up || direction == Direction.down)
        {
            return (CheckInside0Position(x,y-entry)
                    && CheckInside0Position(x,y-entry+size-1)
                    && CheckInside0Position(x+(size-1)*(int)direction/2,y-entry)
                    && CheckInside0Position(x+(size-1)*(int)direction/2,y-entry+size-1)
                    );
        }
    }

    private bool CheckAnyDigPosition()
    {
        for (int i = 0; i < (width-1)/2; i++)
        {
            for (int j = 0; j < (height-1)/2; j++)
            {
                if (CheckCanDig(2*i+1, 2*j+1))
                {
                    return true;
                }
            }
        }
        return false;
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
                if (CheckCanDig(2*i+1, 2*j+1) && Maze[2*i+1,2*j+1] != 0)
                {
                    x = 2*i+1;
                    y = 2*j+1;
                    return;
                }
            }
        }
    }

    private void ChangeroomSizeAndroomEntry()
    {
        roomSize = Random.Range(4,8);
        if (roomSize % 2 == 0)
        {//odd num
            roomSize++;
        }
        roomEntry = Random.Range(0,roomSize+1);
        if (roomEntry % 2 == 1)
        {//even num
            roomEntry--;
        }
    }

    private void MakeRoom()
    {
        if (direction == Direction.up || direction == Direction.down)
        {
            for (int i = 0; i < roomSize; i++)
            {
                for (int j = 0; j < roomSize; j++)
                {
                    //(Maze[x-entry,y]),(Maze[x-entry,y+(size-1)])
                    //(Maze[x-entry+size-1,y]),(Maze[x-entry+size-1,y+(size-1)])
                    Maze[this.x-roomEntry+i,this.y+j*(int)direction] = 2;
                }
            }
            roomNumber++;
        }
        if (direction == Direction.left || direction == Direction.right)
        {
            for (int i = 0; i < roomSize; i++)
            {
                for (int j = 0; j < roomSize; j++)
                {
                    //(Maze[x,y-entry]),(Maze[x+(size-1),y-entry])
                    //(Maze[x,y-entry+size-1]),(Maze[x+(size-1),y-entry+size-1])
                    Maze[this.x+i*(int)direction/2,this.y-roomEntry+j] = 2;
                }
            }
            roomNumber++;
        }
    }

    private void MakePath()
    {
        if (direction == Direction.up || direction == Direction.down)
        {
            this.y+=(int)direction;
            Maze[this.x,this.y] = 1;
            this.y+=(int)direction;
            Maze[this.x,this.y] = 1;
        }
        if (direction == Direction.left || direction == Direction.right)
        {
            this.x+=(int)direction/2;
            Maze[this.x,this.y] = 1;
            this.x+=(int)direction/2;
            Maze[this.x,this.y] = 1;
        }
    }

    public void DigMaze()
    {
        Maze = new int[width,height];
        //entry position x,y oddnum,oddnum
        x=(width-1)/2 + 1;
        y=(height-1)/2 + 1;

        ChangeroomSizeAndroomEntry();
        if (CheckCanMakeRoom(this.x,this.y,this.roomSize,this.roomEntry))
        {
            while (!CheckCanMakeRoom(this.x,this.y,this.roomSize,this.roomEntry,this.direction))
            {
                ChangeDir();
            }
            MakeRoom();
        }
        
        int cnt = 0;

        while (cnt < 100)
        {
            ChangeroomSizeAndroomEntry();
            if (CheckCanMakeRoom(this.x,this.y,this.roomSize,this.roomEntry))
            {
                while (!CheckCanMakeRoom(this.x,this.y,this.roomSize,this.roomEntry,this.direction))
                {
                    ChangeDir();
                }
                MakeRoom();
            }
            else if (CheckCanDig(this.x,this.y))
            { 
                while (!CheckCanDig(this.x,this.y,this.direction))
                {
                    ChangeDir();
                }
                MakePath();
            }
            else
            {
                ChangePosition();
            }
            cnt++;
        }
    }
}
