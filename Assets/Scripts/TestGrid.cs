using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Diagnostics;

public class TestGrid : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize;

    private Grid grid;
    private void Start()
    {
        grid = new Grid(width, height, cellSize, transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 69) ;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

    public Vector3 SnapPosition(Vector3 worldPosition)
    {
        return grid.SnapPosition(worldPosition + transform.position) + new Vector3(0.5f, 0.5f, 0f);
    }
}
