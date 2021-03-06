﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.Mathematics;
using UnityEngine.UI;

public class Grid 
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    private bool debug;
    public Grid(int width, int height, float cellSize, Vector3 originPosition, bool debug)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.debug = debug;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        if (this.debug)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 5, Color.white, TextAnchor.MiddleCenter);
                    debugTextArray[x, y].GetComponent<MeshRenderer>().sortingLayerName = "foreground";
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
        
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        gridArray[x, y] = value;

        if (debug)
        {
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];

        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldposition)
    {
        int x, y;
        GetXY(worldposition, out x, out y);
        return GetValue(x, y);
    }

    /// <summary>
    /// Return XY position centered in grid square
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Vector3 SnapPosition(Vector3 worldPosition)
    {
        int snappedX;
        int snappedY;
        Vector3 outPos;

        GetXY(worldPosition, out snappedX, out snappedY);

        outPos = new Vector3(snappedX+0.5f, snappedY+0.5f, worldPosition.z);
        return outPos;
    }
}
