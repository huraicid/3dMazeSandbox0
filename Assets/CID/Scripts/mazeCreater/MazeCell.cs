using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class MazeCellModel
{
    public enum Wall { Top, Bottom, Left, Right }
    public bool visited = false;
    private Dictionary<Wall, bool> walls = new Dictionary<Wall, bool> {
        { Wall.Top, true },
        { Wall.Bottom, true },
        { Wall.Left, true },
        { Wall.Right, true }
    };

    public void RemoveWall(Wall wall)
    {
        walls[wall] = false;
    }

    public bool HasWall(Wall wall)
    {
        return walls[wall];
    }

    public bool isCulDeSac()
    {
        int wallCount = 0;
        foreach (Wall wall in Enum.GetValues(typeof(Wall)))
        {
            if (!walls[wall]) { wallCount++; }
        }

        if(wallCount == 1) { return true; }

        return false;
    }

    public Wall getCulDeSacEnterDirection()
    {
        foreach(Wall wall in Enum.GetValues(typeof(Wall))) {
            if (!walls[wall])
            {
                return wall;
            }
        }

        return Wall.Top;
    }
}

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject[] wallAarray = new GameObject[] { };

    public void Setup(MazeCellModel mazeCellModel)
    {
        wallAarray[(int)MazeCellModel.Wall.Top].SetActive(mazeCellModel.HasWall(MazeCellModel.Wall.Top));

        for (int i = 0; i < (int)MazeCellModel.Wall.Right + 1; i++)
        {
            wallAarray[i].SetActive(mazeCellModel.HasWall((MazeCellModel.Wall)i));
        }
    }
}

