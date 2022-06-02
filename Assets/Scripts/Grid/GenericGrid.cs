using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class GenericGrid<T>
{
    private int width;
    private int height;
    public float cellSize;
    public Vector3 originPosition;
    public T[,] gridArray;
    public bool showDebug;

   
    public TextMeshPro[,] debugTextArray;

    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public float CellSize { get => cellSize; set => cellSize = value; }


    public GenericGrid(int width, int height, float cellSize, Vector3 originPosition, Func<int,int,T> constructor)
    {
       
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        debugTextArray = new TextMeshPro[width, height];
        gridArray = new T[width, height];

     

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = constructor(x,y);
            }
        }

      
        //bool showDebug = false;
        //if (showDebug)
        //{
        //    for (int x = 0; x < gridArray.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < gridArray.GetLength(1); y++)
        //        {
        //           // debugTextArray[x, y] = Utils.CreateWorldText("GridData "+ x+" "+y+" ",gridArray[x, y]?.ToString(), null, GetWorldPositionCartToIso(x, y) + new Vector3(0, cellSize * 0.5f)+ originPosition, 2, Color.white, TextAnchor.MiddleCenter);

        //            Debug.DrawLine(GetWorldPositionCartToIso(x, y), GetWorldPositionCartToIso(x, y + 1), Color.white, 100f);
        //            Debug.DrawLine(GetWorldPositionCartToIso(x, y), GetWorldPositionCartToIso(x + 1, y), Color.white, 100f);
        //        }
        //    }
        //    Debug.DrawLine(GetWorldPositionCartToIso(0, height), GetWorldPositionCartToIso(width, height), Color.white, 100f);
        //    Debug.DrawLine(GetWorldPositionCartToIso(width, 0), GetWorldPositionCartToIso(width, height), Color.white, 100f);
        //}
    }
    public Vector3 GetWorldPositionCartToIso(int x, int y)
    {
        float isoX = x - y;
        float isoY = (x + y) * 0.5f;

        return new Vector3(isoX, isoY) * CellSize + originPosition;
    }
    public Vector3 GetWorldPositionCartToIso(Vector3 Pos)
    {
        float isoX = Pos.x - Pos.y;
        float isoY = (Pos.x + Pos.y) * 0.5f;

        return new Vector3(isoX, isoY) * CellSize + originPosition;
    }
    public Vector3 GetWorldPositionIsoToCart(int isoX, int isoY)
    {
        float cartX = (2 * isoY + isoX) / 2;
        float cartY = (2 * isoY - isoX) / 2;

        return new Vector3(cartX, cartY) * CellSize + originPosition;
    }
    public Vector3 GetWorldPositionIsoToCart(Vector3 isoPos)
    {
        float cartX = (2 * isoPos.y + isoPos.x) / 2;
        float cartY = (2 * isoPos.y - isoPos.x) / 2;

        return new Vector3(cartX, cartY) * CellSize + originPosition;
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        Vector3 v = GetWorldPositionIsoToCart(worldPosition);
        x = Mathf.FloorToInt(v.x);
        y = Mathf.FloorToInt(v.y);

        //Debug.Log("GetXY : " + x + " ," + y);
    }
    public void SetGridObject(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            gridArray[x, y] = value;
        }
    }
    public void SetGridObject(Vector3 worldPosition, T value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }
    public T GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
            return gridArray[x, y];
        else
            return default(T);
    }
    public T GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
    public void OnGridObjectModified(T GridObject)
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x, y].text = gridArray[x, y].ToString();
            }
        }
    }

   

}
