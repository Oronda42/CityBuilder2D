using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    public Tile adjacentRoad;
   

    [SerializeField]
    public Tile containerTile;

    public Grid grid;

    public GameObject citizenPrefab;

    public void Init(Grid grid, Tile containerTile)
    {
        this.grid = grid;
        this.containerTile = containerTile;

        if (!IsRoadAdjacent(containerTile))
        {
            Debug.Log(" Building is not connected to a road");
        }
        else
        {
            Vector3 pos = grid.genericGrid.GetWorldPositionCartToIso(adjacentRoad.X, adjacentRoad.Y);
            Unit citizen =  Instantiate(citizenPrefab, pos, Quaternion.identity).GetComponent<Unit>();
            citizen.Init(grid, this);
        }
    }

    public bool IsRoadAdjacent(Tile tile)
    {
        //WEST
        if (tile.X - 1 >= 0)
        {
            Tile westTile = grid.genericGrid.GetGridObject(tile.X - 1, tile.Y);
            if (westTile.isRoad)
            {
                adjacentRoad = westTile;
                return true;
            }
        }

        //EAST
        if (tile.X + 1 < grid.GridSizeX)
        {
            Tile eastTile = grid.genericGrid.GetGridObject(tile.X + 1, tile.Y);
            if (eastTile.isRoad)
            {
                adjacentRoad = eastTile;
                return true;
            }
        }

        //SOUTH
        if (tile.Y - 1 >= 0)
        {
            Tile southTile = grid.genericGrid.GetGridObject(tile.X, tile.Y - 1);
            if (southTile.isRoad)
            {
                adjacentRoad = southTile;
                return true;
            }
        }

        //NORTH
        if (tile.Y + 1 < grid.GridSizeY)
        {
            Tile northTile = grid.genericGrid.GetGridObject(tile.X, tile.Y + 1);
            if (northTile.isRoad)
            {
                adjacentRoad = northTile;
                return true;
            }
        }

        return false;
    }

}
