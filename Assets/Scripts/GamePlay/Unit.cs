using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
     PathFinding pathFinding;
    public Grid grid;
    public Building house;
    public float speed = 5;

    public void Init(Grid grid, Building house)
    {
        this.grid = grid;
        this.house = house;
        pathFinding = new PathFinding();
        pathFinding.Init(grid.genericGrid);

        List<Tile> path = new List<Tile>();
        if (CalculatePath(house.adjacentRoad, grid.genericGrid.GetGridObject(0, 0), out path))
        {
            StartCoroutine(Roam(path));
        }
       

    }

    IEnumerator Roam(List<Tile> path)
    {
        int i = 1;
        while(transform.position != grid.genericGrid.GetWorldPositionCartToIso(path[path.Count-1].X, path[path.Count - 1].Y))
        {
            transform.position = Vector3.MoveTowards(transform.position, grid.genericGrid.GetWorldPositionCartToIso(path[i].X, path[i].Y), Time.deltaTime *speed);

            if (transform.position == grid.genericGrid.GetWorldPositionCartToIso(path[i].X, path[i].Y))
            {
                i++;
            }
            yield return null;
        }

        path.Reverse();
        StartCoroutine(Roam(path));
    }

    private bool CalculatePath(Tile start, Tile end, out List<Tile> _path)
    {
        List<Tile> path = pathFinding.FindPath(start, end);
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(grid.genericGrid.GetWorldPositionCartToIso(new Vector3(path[i].X, path[i].Y) + new Vector3(grid.cellSize * 0.5f, grid.cellSize * 0.5f)), grid.genericGrid.GetWorldPositionCartToIso(new Vector3(path[i + 1].X, path[i + 1].Y) + new Vector3(grid.cellSize * 0.5f, grid.cellSize * 0.5f)), Color.red, 5f);

            }
            //for (int i = 0; i < path.Count; i++)
            //{
            //    Utils.CreateWorldText("path", i.ToString(), null, grid.genericGrid.GetWorldPositionCartToIso(new Vector3(path[i].X, path[i].Y) + new Vector3(grid.cellSize * 0.5f, grid.cellSize * 0.5f)), 5, Color.red, TextAnchor.MiddleCenter);
            //}
            _path = path;
            return true;

        }
        _path = null;
        return false;
    }
}
