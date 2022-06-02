using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TileEvent(Tile tile);
[Serializable]
public class Tile 
{

    public enum TileState
    {
        NONE,
        GRASS,
        ROAD,
        WATER
    }

    public event TileEvent OnValueChanged;
    TileState tileState;

    public int X;
    public int Y;

    public int rotatedX;
    public int rotatedY;

    #region pathfinding
    public bool isWalkable;
    public bool isRoad;
    public int Gcost;
    public int Hcost;
    public int Fcost;
    public Tile cameFromTile; 
    #endregion

    public int roadBitIndex;
    private int spriteIndex;

    public Transform building;

    public int SpriteIndex { get => spriteIndex; set => spriteIndex = value; }

    public void CalculateFCost()
    {
        Fcost = Gcost + Hcost;
    }

    public Tile(int x, int y)
    {
        this.X = x;
        this.Y = y;
        isWalkable = true;
        spriteIndex = 0;
    }

    public void SetTileSpriteIndex(int spriteIndex)
    {
        this.spriteIndex = spriteIndex;
        OnValueChanged?.Invoke(this);
    }

    public override string ToString()
    {
        return spriteIndex.ToString();
    }

}


