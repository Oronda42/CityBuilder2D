using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid terrainGrid;
    public Grid buildingGrid;

    public int gridWidthAndHeight;
    
   

    void Awake()
    {
        Init();

    }

    public void Init()
    {
        terrainGrid = transform.Find("Terrain").GetComponentInChildren<Grid>();
        //buildingGrid = transform.Find("Buildings").GetComponentInChildren<Grid>();

        terrainGrid.Init(gridWidthAndHeight);
        //buildingGrid.Init(gridWidthAndHeight);
    }
}
