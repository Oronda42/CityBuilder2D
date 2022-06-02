using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GenericGrid<Tile> genericGrid;
    public GridMesh gridMesh;
    public GridDisplay gridVisual;


    private  int gridSizeX;
    private int gridSizeY;
    public int cellSize;

  
    public int GridSizeX { get => gridSizeX;}
    public int GridSizeY { get => gridSizeY;}
    public int CellSize { get => cellSize;}



   public void Init(int gridSize)
   {
        this.gridSizeX = gridSize;
        this.gridSizeY = gridSize;

        //gridMesh = GetComponent<GridMesh>();
        //gridVisual = GetComponent<GridDisplay>();
        CreateGenericGrid(gridSizeX, gridSizeY);

        //gridMesh.Init(this);
        //gridVisual.Init(this);
   }

    private void CreateGenericGrid(int gridSizeX, int gridSizeY)
    {
        genericGrid = new GenericGrid<Tile>(gridSizeX, gridSizeY, cellSize, transform.position, TileConstructor);

        //SubscribeEvents();
    }

    public Tile TileConstructor(int x, int y)
    {
        Tile tile = new Tile(x, y);
        //tile.SetTileSpriteIndex(2);
        return tile;
    }

    private void SubscribeEvents()
    {
        for (int y = 0; y < genericGrid.Height; y++)
        {
            for (int x = 0; x < genericGrid.Width; x++)
            {
                Tile tile = genericGrid.GetGridObject(x, y);
                //tile.OnValueChanged += gridVisual.UpdateTile;
               // tile.OnValueChanged += genericGrid.OnGridObjectModified;
            }
        }
    }
}
