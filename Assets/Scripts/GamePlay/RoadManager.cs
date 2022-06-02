using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    //// Start is called before the first frame update

    //Grid roadGrid;
    //public void Init(Grid roadGrid)
    //{
    //    this.roadGrid = roadGrid;
    //}


    //[Flags]
    //public enum Direction
    //{
    //    None = 0,
    //    North = 1,
    //    West = 1 << 1,
    //    East = 1 << 2,
    //    South = 1 << 3,
    //}

//    public void UpdateNearbyTiles(Tile tile)
//    {
//        int spriteIndex;
//        //WEST
//        if (tile.X - 1 >= 0)
//        {
//            Tile westTile = roadGrid.genericGrid.GetGridObject(tile.X - 1, tile.Y);
//            if (westTile.isRoad)
//            {
//                int bitflag = CalculateBitFlag(westTile);
//                if (bitflag != westTile.roadBitIndex)
//                {
//                    westTile.roadBitIndex = bitflag;
//                    roadGrid.gridVisual.roadDictionary.TryGetValue(bitflag, out spriteIndex);
//                    roadGrid.gridVisual.SetTileSprite(westTile, spriteIndex);
//                }
//            }
//        }

//        //EAST
//        if (tile.X + 1 < roadGrid.GridSizeX)
//        {
//            Tile eastTile = roadGrid.genericGrid.GetGridObject(tile.X + 1, tile.Y);
//            if (eastTile.isRoad)
//            {
//                int bitflag = CalculateBitFlag(eastTile);
//                if (bitflag != eastTile.roadBitIndex)
//                {
//                    eastTile.roadBitIndex = bitflag;
//                    roadGrid.gridVisual.roadDictionary.TryGetValue(bitflag, out spriteIndex);
//                    roadGrid.gridVisual.SetTileSprite(eastTile, spriteIndex);
//                }
//            }
//        }

//        //SOUTH
//        if (tile.Y - 1 >= 0)
//        {
//            Tile southTile = roadGrid.genericGrid.GetGridObject(tile.X, tile.Y - 1);
//            if (southTile.isRoad)
//            {
//                int bitflag = CalculateBitFlag(southTile);
//                if (bitflag != southTile.roadBitIndex)
//                {
//                    southTile.roadBitIndex = bitflag;
//                    roadGrid.gridVisual.roadDictionary.TryGetValue(bitflag, out spriteIndex);
//                    roadGrid.gridVisual.SetTileSprite(southTile, spriteIndex);
//                }
//            }
//        }

//        //NORTH
//        if (tile.Y + 1 < roadGrid.GridSizeY)
//        {
//            Tile northTile = roadGrid.genericGrid.GetGridObject(tile.X, tile.Y + 1);
//            if (northTile.isRoad)
//            {
//                int bitflag = CalculateBitFlag(northTile);
//                if (bitflag != northTile.roadBitIndex)
//                {
//                    northTile.roadBitIndex = bitflag;
//                    roadGrid.gridVisual.roadDictionary.TryGetValue(bitflag, out spriteIndex);
//                    roadGrid.gridVisual.SetTileSprite(northTile, spriteIndex);
//                }
//            }
//        }
//    }
//    public int CalculateBitFlag(Tile tile)
//    {
//        Direction direction = Direction.None;

//        // WEST
//        if (tile.X - 1 >= 0)
//            if (roadGrid.genericGrid.GetGridObject(tile.X - 1, tile.Y).isRoad)
//                direction |= Direction.West;

//        // EAST
//        if (tile.X + 1 < roadGrid.GridSizeX)
//            if (roadGrid.genericGrid.GetGridObject(tile.X + 1, tile.Y).isRoad)
//                direction |= Direction.East;

//        //SOUTH
//        if (tile.Y - 1 >= 0)
//            if (roadGrid.genericGrid.GetGridObject(tile.X, tile.Y - 1).isRoad)
//                direction |= Direction.South;

//        //NORTH
//        if (tile.Y + 1 < roadGrid.GridSizeY)
//            if (roadGrid.genericGrid.GetGridObject(tile.X, tile.Y + 1).isRoad)
//                direction |= Direction.North;

//        return (int)(direction);
//    }
}
