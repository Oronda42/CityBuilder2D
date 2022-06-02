using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    public GenericGrid<Tile> grid;

    public List<Tile> openList;
    public List<Tile> closedList;


    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 21;


    public void Init(GenericGrid<Tile> grid)
    {
        this.grid = grid;
    }
    public List<Tile> FindPath(Tile start, Tile end)
    {
        openList = new List<Tile>();
        openList.Add(start);

        closedList = new List<Tile>();

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                Tile tile = grid.GetGridObject(x, y);
                tile.Gcost = int.MaxValue;
                tile.CalculateFCost();
                tile.cameFromTile = null;
            }
        }

        start.Gcost = 0;
        start.Hcost = CalculateDistance(start, end);
        start.CalculateFCost();


        while (openList.Count>0)
        {
            Tile currentTile = GetLowestFCostTile(openList);
            if (currentTile == end)
            {
                // GOAL
                return CalculatePath(end);
            }
            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach (Tile neighbourTile in GetNeighboursList(currentTile))
            {

                if (!neighbourTile.isWalkable)
                {
                    closedList.Add(neighbourTile);  
                    continue;
                }
               
               
                if (closedList.Contains(neighbourTile)) continue;

                int tentativeGcost = currentTile.Gcost + CalculateDistance(currentTile, neighbourTile);
                if (tentativeGcost<neighbourTile.Gcost)
                {
                    neighbourTile.cameFromTile = currentTile;
                    neighbourTile.Gcost = tentativeGcost;
                    neighbourTile.Hcost = CalculateDistance(neighbourTile, end);
                    neighbourTile.CalculateFCost();

                    if (!openList.Contains(neighbourTile))
                    {
                        openList.Add(neighbourTile);
                    }
                }
            }
        }

        // OUT OF TILE ON THE OPENLIST
        return null;
    }
    private List<Tile> GetNeighboursList(Tile currentTile)
    {
        List<Tile> neighbourList = new List<Tile>();

     
        if (currentTile.X - 1 >= 0)
        {
            // LEFT
            neighbourList.Add(grid.GetGridObject(currentTile.X - 1, currentTile.Y));
            // LEFT DOWN
            if (currentTile.Y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentTile.X - 1, currentTile.Y-1));
            // LEFT UP
            if (currentTile.Y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(currentTile.X - 1, currentTile.Y+1));
        }

        if (currentTile.X + 1 < grid.Width)
        {
            // Right
            neighbourList.Add(grid.GetGridObject(currentTile.X + 1, currentTile.Y));
            // Right DOWN
            if (currentTile.Y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentTile.X + 1, currentTile.Y - 1));
            // Right UP
            if (currentTile.Y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(currentTile.X + 1, currentTile.Y + 1));
        }

        //DOWN
        if (currentTile.Y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentTile.X, currentTile.Y - 1));
        //UP
        if (currentTile.Y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(currentTile.X, currentTile.Y + 1));

        return neighbourList;

    }
    private List<Tile> CalculatePath(Tile endTile)
    {
        List<Tile> path = new List<Tile>();
        path.Add(endTile);
        Tile currentTile = endTile;
        while(currentTile.cameFromTile != null)
        {
            path.Add(currentTile.cameFromTile);
            currentTile = currentTile.cameFromTile;
        }

        path.Reverse();
        return path;
       
    }
    private int CalculateDistance(Tile a, Tile b)
    {
        if (a != null && b != null)
        {
            int xDistance = Mathf.Abs(a.X - b.X);
            int yDistance = Mathf.Abs(a.Y - b.Y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            //return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }
        else return 0;
        
    }
    private Tile GetLowestFCostTile(List<Tile> TileList)
    {
        Tile lowestFCostTile = TileList[0];
        for (int i = 0; i < TileList.Count; i++)
        {
            if (TileList[i].Fcost<lowestFCostTile.Fcost)
            {
                lowestFCostTile = TileList[i];
            }
        }
        return lowestFCostTile;
    }


}
