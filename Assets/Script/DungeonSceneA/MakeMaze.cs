﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// logic of makemaze
/// </summary>
public class MakeMaze
{
    public int[, , ] Maze;
    private int width;
    private int height;
    private Direction direction = Direction.right;
    private int x = 1;
    private int y = 1;
    private int roomWidth;
    private int roomHeiht;
    private int roomEntryX;
    private int roomEntryY;
    private int roomNumber;
    private List<int[]> FloorPosList = new List<int[]> ();
    private List<int[]> StairsSuggestList = new List<int[]> ();
    public MakeMaze (int width, int height)
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
    private bool CheckInside0Position (int x, int y)
    {
        return (x > -1 && x < this.width) &&
            (y > -1 && y < this.width) &&
            (this.Maze[x, y, 0] == 0);
    }

    // check the position where can dig 
    private bool CheckCanDig (int x, int y)
    {
        return (CheckInside0Position (x, y + 2 * (int) Direction.up) ||
            CheckInside0Position (x, y + 2 * (int) Direction.down) ||
            CheckInside0Position (x + (int) Direction.left, y) ||
            CheckInside0Position (x + (int) Direction.right, y)
        );
    }
    private bool CheckCanDig (int x, int y, Direction direction)
    {
        if (direction == Direction.up || direction == Direction.down)
        {
            return CheckInside0Position (x, y + 2 * (int) direction);
        }
        else //(direction == Direction.left || direction == Direction.right)
        {
            return CheckInside0Position (x + (int) direction, y);
        }
    }

    private bool CheckCanMakeRoom (int x, int y, int sizeX, int sizeY, int entryX, int entryY)
    {
        return (CheckInside0Position (x - entryX, y) &&
                CheckInside0Position (x - entryX + sizeX - 1, y) &&
                CheckInside0Position (x - entryX, y + (sizeY - 1) * (int) Direction.up) &&
                CheckInside0Position (x - entryX + sizeX - 1, y + (sizeY - 1) * (int) Direction.up)
            ) ||
            (CheckInside0Position (x - entryX, y) &&
                CheckInside0Position (x - entryX + sizeX - 1, y) &&
                CheckInside0Position (x - entryX, y + (sizeY - 1) * (int) Direction.down) &&
                CheckInside0Position (x - entryX + sizeX - 1, y + (sizeY - 1) * (int) Direction.down)
            ) ||
            (CheckInside0Position (x, y - entryY) &&
                CheckInside0Position (x, y - entryY + sizeY - 1) &&
                CheckInside0Position (x + (sizeX - 1) * (int) Direction.left / 2, y - entryY) &&
                CheckInside0Position (x + (sizeX - 1) * (int) Direction.left / 2, y - entryY + sizeY - 1)
            ) ||
            (CheckInside0Position (x, y - entryY) &&
                CheckInside0Position (x, y - entryY + sizeY - 1) &&
                CheckInside0Position (x + (sizeX - 1) * (int) Direction.right / 2, y - entryY) &&
                CheckInside0Position (x + (sizeX - 1) * (int) Direction.right / 2, y - entryY + sizeY - 1)
            );
    }

    private bool CheckCanMakeRoom (int x, int y, int sizeX, int sizeY, int entryX, int entryY, Direction direction)
    {
        if (direction == Direction.up || direction == Direction.down)
        {
            return (CheckInside0Position (x - entryX, y) &&
                CheckInside0Position (x - entryX + sizeX - 1, y) &&
                CheckInside0Position (x - entryX, y + (sizeY - 1) * (int) direction) &&
                CheckInside0Position (x - entryX + sizeX - 1, y + (sizeY - 1) * (int) direction)
            );
        }
        else //(direction == Direction.left || direction == Direction.right)
        {
            return (CheckInside0Position (x, y - entryY) &&
                CheckInside0Position (x, y - entryY + sizeY - 1) &&
                CheckInside0Position (x + (sizeX - 1) * (int) direction / 2, y - entryY) &&
                CheckInside0Position (x + (sizeX - 1) * (int) direction / 2, y - entryY + sizeY - 1)
            );
        }
    }

    private void CheckFloorPosition ()
    {
        FloorPosList.Clear ();
        for (int i = 0; i < (this.width - 1) / 2; i++)
        {
            for (int j = 0; j < (this.height - 1) / 2; j++)
            {
                if (Maze[2 * i + 1, 2 * j + 1, 0] != 0)
                {
                    FloorPosList.Add (new int[2] { 2 * i + 1, 2 * j + 1 });
                }
            }
        }
    }

    private void CheckStairsSuggestPosition ()
    {
        CheckFloorPosition ();
        foreach (var item in FloorPosList)
        {
            if ((Maze[item[0] + 1, item[1], 0] + Maze[item[0] - 1, item[1], 0] +
                    Maze[item[0], item[1] + 1, 0] + Maze[item[0], item[1] - 1, 0]) == 1)
            {
                StairsSuggestList.Add (item);
            }
        }
    }

    private bool CheckAnyDigPosition ()
    {
        for (int i = 0; i < (this.width - 1) / 2; i++)
        {
            for (int j = 0; j < (this.height - 1) / 2; j++)
            {
                if (CheckCanDig (2 * i + 1, 2 * j + 1))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void ChangeDir ()
    {
        switch (Random.Range (0, 4))
        {
            case 0:
                this.direction = Direction.up;
                break;
            case 1:
                this.direction = Direction.down;
                break;
            case 2:
                this.direction = Direction.left;
                break;
            case 3:
                this.direction = Direction.right;
                break;
        }
    }

    private void ChangePosition ()
    {
        CheckFloorPosition ();
        int randomListNum = Random.Range (0, FloorPosList.Count);
        this.x = FloorPosList[randomListNum][0];
        this.y = FloorPosList[randomListNum][1];
    }

    private void ChangeroomSize ()
    {
        this.roomWidth = Random.Range (4, 8);
        if (this.roomWidth % 2 == 0)
        { //odd num
            this.roomWidth++;
        }
        this.roomHeiht = Random.Range (4, 8);
        if (this.roomHeiht % 2 == 0)
        { //odd num
            this.roomHeiht++;
        }
    }

    private void ChangeroomEntry ()
    {
        this.roomEntryX = Random.Range (0, this.roomWidth + 1);
        if (this.roomEntryX % 2 == 1)
        { //even num
            this.roomEntryX--;
        }
        this.roomEntryY = Random.Range (0, this.roomHeiht + 1);
        if (this.roomEntryY % 2 == 1)
        { //even num
            this.roomEntryY--;
        }
    }

    private void MakeRoom ()
    {
        if (direction == Direction.up || direction == Direction.down)
        {
            for (int i = 0; i < roomWidth; i++)
            {
                for (int j = 0; j < roomHeiht; j++)
                {
                    //(Maze[x-entry,y]),(Maze[x-entry,y+(size-1)])
                    //(Maze[x-entry+size-1,y]),(Maze[x-entry+size-1,y+(size-1)])
                    Maze[this.x - roomEntryX + i, this.y + j * (int) direction, 0] = 2;
                }
            }
            roomNumber++;
        }
        if (direction == Direction.left || direction == Direction.right)
        {
            for (int i = 0; i < roomWidth; i++)
            {
                for (int j = 0; j < roomHeiht; j++)
                {
                    //(Maze[x,y-entry]),(Maze[x+(size-1),y-entry])
                    //(Maze[x,y-entry+size-1]),(Maze[x+(size-1),y-entry+size-1])
                    Maze[this.x + i * (int) direction / 2, this.y - roomEntryY + j, 0] = 2;
                }
            }
            roomNumber++;
        }
    }

    private void MakePath ()
    {
        if (direction == Direction.up || direction == Direction.down)
        {
            this.y += (int) direction;
            Maze[this.x, this.y, 0] = 1;
            this.y += (int) direction;
            Maze[this.x, this.y, 0] = 1;
        }
        if (direction == Direction.left || direction == Direction.right)
        {
            this.x += (int) direction / 2;
            Maze[this.x, this.y, 0] = 1;
            this.x += (int) direction / 2;
            Maze[this.x, this.y, 0] = 1;
        }
    }

    private void MakeStairs ()
    {
        CheckStairsSuggestPosition ();
        int randomListNum = Random.Range (0, StairsSuggestList.Count);
        Maze[StairsSuggestList[randomListNum][0], StairsSuggestList[randomListNum][1], 0] = 3;

    }

    public void DigMaze ()
    {
        Maze = new int[this.width, this.height, 2];

        ChangeroomSize ();
        if (CheckCanMakeRoom (this.x, this.y, this.roomWidth, this.roomHeiht, this.roomEntryX, this.roomEntryY))
        {
            while (!CheckCanMakeRoom (this.x, this.y, this.roomWidth, this.roomHeiht, this.roomEntryX, this.roomEntryY, this.direction))
            {
                ChangeDir ();
            }
            MakeRoom ();
        }

        int cnt = 0;

        while (cnt < 500)
        {
            ChangeDir ();
            ChangeroomSize ();
            ChangeroomEntry ();

            if (CheckCanMakeRoom (this.x, this.y, this.roomWidth, this.roomHeiht, this.roomEntryX, this.roomEntryY))
            {
                while (!CheckCanMakeRoom (this.x, this.y, this.roomWidth, this.roomHeiht, this.roomEntryX, this.roomEntryY, this.direction))
                {
                    ChangeDir ();
                }
                MakeRoom ();
            }
            else if (CheckCanDig (this.x, this.y, this.direction))
            {
                MakePath ();
            }
            else
            {
                ChangePosition ();
            }
            cnt++;
        }

        MakeStairs ();
    }
}