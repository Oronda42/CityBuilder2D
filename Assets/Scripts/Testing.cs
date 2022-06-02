using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour
{

    public Grid grid;
    public GameObject building;

    public int noiseScale;

    public Texture2D water;
    SpriteUvs spriteUvWater = new SpriteUvs { uv00 = new Vector2(0, 0), uv01 = new Vector2(0, 1), uv10 = new Vector2(1, 0), uv11 = new Vector2(1, 1) };

    [Flags]
    public enum Direction
    {
        None = 0,
        North = 1,
        West = 1 << 1,
        East = 1 << 2,
        South = 1 << 3,
    }

    public enum MapOrientation
    {
        NORTH = 0,
        EAST = 1,
        SOUTH = 2,
        WEST = 3
    }

    private void Start()
    {
        ApllyNoise();

      
    }

    private void ApllyNoise()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(grid.GridSizeX, grid.GridSizeY, noiseScale);

        for (int y = 0; y < grid.GridSizeY; y++)
        {
            for (int x = 0; x < grid.GridSizeX; x++)
            {
                Tile tile = grid.genericGrid.gridArray[x, y];

                int _spriteIndex = 0;

                if (noiseMap[x, y] < 0.25f)
                {
                    _spriteIndex = 1;
                }
                if (noiseMap[x, y] < 0.50f && noiseMap[x, y] >= 0.25f)
                {
                    _spriteIndex =2;
                }
                if (noiseMap[x, y] < 0.75f && noiseMap[x, y] >= 0.50f)
                {
                    _spriteIndex = 2;
                }
                if (noiseMap[x, y] <= 1f && noiseMap[x, y] >= 0.75f)
                {
                    _spriteIndex = 2;
                }

                tile.SpriteIndex = _spriteIndex;
                if (_spriteIndex >=0)
                {
                    SpriteUvs spriteUv = grid.gridVisual.spritesCoordList[_spriteIndex];
                    grid.gridMesh.SetTileUvs(tile, spriteUv);

                }
                else
                {
                    grid.gridMesh.SetTileUvs(tile, spriteUvWater);

                }
            }
        }

        //for (int y = 0; y < grid.GridSizeY; y++)
        //{
        //    for (int x = 0; x < grid.GridSizeX; x++)
        //    {
        //        Tile tile = grid.genericGrid.gridArray[x, y];

        //        SpriteUvs spriteUv = grid.gridVisual.spritesCoordList[CalculateBitFlagTest(tile)];
        //        grid.gridMesh.SetTileUvs(tile, spriteUv);
        //    }
        //}

        grid.gridMesh.UpdateUvs();
    }




    private void Update()
    {

        #region PathfindingTest

        //if (Input.GetMouseButtonDown(0))
        //{
        //Tile tile = grid.GetGridObject(Utils.GetMouseWorldPosition());

        //TileMapSprite.SpriteUV spriteUv = visual.tilemapSprite.sprites[0];
        //int[] vertices = visual.GetVerticesIndexForThisTile(Utils.GetMouseWorldPosition());

        //visual.uvs[vertices[0]] = spriteUv.uv00;
        //visual.uvs[vertices[1]] = spriteUv.uv10;
        //visual.uvs[vertices[2]] = spriteUv.uv01;
        //visual.uvs[vertices[3]] = spriteUv.uv11;

        //visual.UpdateMesh();
        //tile.isWalkable = true;
        //tile.isRoad = true; ;

        //List<Tile> path = pathFinding.FindPath(grid.GetGridObject(0, 0), tile);
        //if (path != null)
        //{
        //    for (int i = 0; i < path.Count - 1; i++)
        //    {
        //        Debug.DrawLine(grid.GetWorldPositionCartToIso(new Vector3(path[i].X, path[i].Y) + new Vector3(grid.cellSize * 0.5f, grid.cellSize * 0.5f)), grid.GetWorldPositionCartToIso(new Vector3(path[i + 1].X, path[i + 1].Y) + new Vector3(grid.cellSize * 0.5f, grid.cellSize * 0.5f)), Color.red, 5f);

        //    }
        //    for (int i = 0; i < path.Count; i++)
        //    {
        //        //Utils.CreateWorldText("path", i.ToString(), null, grid.GetWorldPositionCartToIso(new Vector3(path[i].X, path[i].Y) + new Vector3(grid.cellSize * 0.5f, grid.cellSize * 0.5f)), 5, Color.red, TextAnchor.MiddleCenter);
        //    }

        //}


        //}

        //if (Input.GetMouseButtonDown(0))
        //{

        //    float[,] noise = Noise.GenerateNoiseMap(grid.GridSizeX, grid.GridSizeY);

        //} 
        #endregion 

        if (Input.GetMouseButton(0))
        {
            Tile tile = grid.genericGrid.GetGridObject(Utils.GetMouseWorldPosition());

            if (tile != null && !tile.isRoad && !tile.building)
            {

                int bitIndex = CalculateBitFlag(tile);

                // Debug.Log(bitIndex);

                int spriteIndex;
                grid.gridVisual.roadDictionary.TryGetValue(bitIndex, out spriteIndex);
                tile.isRoad = true;
                tile.isWalkable = true;
                tile.roadBitIndex = bitIndex;


                grid.gridVisual.SetTileSprite(tile, spriteIndex);

                //UPDATE NEARBY TILES

                UpdateNearbyTiles(tile);

                grid.gridMesh.UpdateMesh();

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Tile tile = grid.genericGrid.GetGridObject(Utils.GetMouseWorldPosition());
            if (tile != null)
            {

                //tile.SetTileSpriteIndex(3);
                grid.gridVisual.SetTileSprite(tile, 3);
                tile.isRoad = false;
                tile.isWalkable = false;
                grid.gridMesh.UpdateMesh();

                //int spriteIndex = 3;
                ////visual.tilemapSprite.roadDictionary.TryGetValue(15, out spriteIndex);
                //tile.isRoad = false;
                //tile.roadBitIndex = 0;
                //grid.gridVisual.SetTileSprite(tile, spriteIndex);
                //UpdateNearbyTiles(tile);

                //grid.gridMesh.UpdateMesh();


            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Tile tile = grid.genericGrid.GetGridObject(Utils.GetMouseWorldPosition());
            if (tile != null && !tile.isRoad && !tile.building)
            {
                Vector3 buildingPos = grid.genericGrid.GetWorldPositionCartToIso(tile.X, tile.Y);
                tile.building = Instantiate(building, buildingPos, Quaternion.identity).transform;
                tile.building.GetComponent<Building>().Init(grid, tile);

            }
            else
                Debug.Log("Cannot build");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Tile[,] rotatedGrid = RotateMatrix(roadGrid.genericGrid.gridArray, roadGrid.genericGrid.Width);
            grid.gridMesh.currenMapOrientation += 1;
            if ((int)grid.gridMesh.currenMapOrientation > 4)
            {
               grid.gridMesh.currenMapOrientation = MapOrientation.NORTH;
            }

            for (int y = 0; y < grid.GridSizeY; y++)
            {
                for (int x = 0; x < grid.GridSizeX; x++)
                {
                    Tile tile = grid.genericGrid.gridArray[x, y];
                   // Tile afterRotateTile = rotatedGrid[x, y];

                    SpriteUvs spriteUv = grid.gridVisual.spritesCoordList[tile.SpriteIndex];
                    grid.gridMesh.SetTileUvs(tile, spriteUv);
                }
            }
            grid.gridMesh.UpdateUvs();
            //roadGrid.genericGrid.gridArray = rotatedGrid;
           

        }
    }

    public void UpdateNearbyTiles(Tile tile)
    {
        int spriteIndex;
        //WEST
        if (tile.X - 1 >= 0)
        {
            Tile westTile = grid.genericGrid.GetGridObject(tile.X - 1, tile.Y);
            if (westTile.isRoad)
            {
                int bitflag = CalculateBitFlag(westTile);
                if (bitflag != westTile.roadBitIndex)
                {
                    westTile.roadBitIndex = bitflag;
                    grid.gridVisual.roadDictionary.TryGetValue(bitflag, out spriteIndex);
                    grid.gridVisual.SetTileSprite(westTile, spriteIndex);
                }
            }
        }

        //EAST
        if (tile.X + 1 < grid.GridSizeX)
        {
            Tile eastTile = grid.genericGrid.GetGridObject(tile.X + 1, tile.Y);
            if (eastTile.isRoad)
            {
                int bitflag = CalculateBitFlag(eastTile);
                if (bitflag != eastTile.roadBitIndex)
                {
                    eastTile.roadBitIndex = bitflag;
                    grid.gridVisual.roadDictionary.TryGetValue(bitflag, out spriteIndex);
                    grid.gridVisual.SetTileSprite(eastTile, spriteIndex);
                }
            }
        }

        //SOUTH
        if (tile.Y - 1 >= 0)
        {
            Tile southTile = grid.genericGrid.GetGridObject(tile.X, tile.Y - 1);
            if (southTile.isRoad)
            {
                int bitflag = CalculateBitFlag(southTile);
                if (bitflag != southTile.roadBitIndex)
                {
                    southTile.roadBitIndex = bitflag;
                    grid.gridVisual.roadDictionary.TryGetValue(bitflag, out spriteIndex);
                    grid.gridVisual.SetTileSprite(southTile, spriteIndex);
                }
            }
        }

        //NORTH
        if (tile.Y + 1 < grid.GridSizeY)
        {
            Tile northTile = grid.genericGrid.GetGridObject(tile.X, tile.Y + 1);
            if (northTile.isRoad)
            {
                int bitflag = CalculateBitFlag(northTile);
                if (bitflag != northTile.roadBitIndex)
                {
                    northTile.roadBitIndex = bitflag;
                    grid.gridVisual.roadDictionary.TryGetValue(bitflag, out spriteIndex);
                    grid.gridVisual.SetTileSprite(northTile, spriteIndex);
                }
            }
        }
    }
    public int CalculateBitFlag(Tile tile)
    {
        Direction direction = Direction.None;

        // WEST
        if (tile.X - 1 >= 0)
            if (grid.genericGrid.GetGridObject(tile.X - 1, tile.Y).isRoad)
                direction |= Direction.West;

        // EAST
        if (tile.X + 1 < grid.GridSizeX)
            if (grid.genericGrid.GetGridObject(tile.X + 1, tile.Y).isRoad)
                direction |= Direction.East;

        //SOUTH
        if (tile.Y - 1 >= 0)
            if (grid.genericGrid.GetGridObject(tile.X, tile.Y - 1).isRoad)
                direction |= Direction.South;

        //NORTH
        if (tile.Y + 1 < grid.GridSizeY)
            if (grid.genericGrid.GetGridObject(tile.X, tile.Y + 1).isRoad)
                direction |= Direction.North;

        return (int)(direction);
    }

    public int CalculateBitFlagTest(Tile tile)
    {
        Direction direction = Direction.None;

        // WEST
        if (tile.X - 1 >= 0)
            if (grid.genericGrid.GetGridObject(tile.X - 1, tile.Y).SpriteIndex == 4)
                direction |= Direction.West;

        // EAST
        if (tile.X + 1 < grid.GridSizeX)
            if (grid.genericGrid.GetGridObject(tile.X + 1, tile.Y).SpriteIndex == 4)
                direction |= Direction.East;

        //SOUTH
        if (tile.Y - 1 >= 0)
            if (grid.genericGrid.GetGridObject(tile.X, tile.Y - 1).SpriteIndex == 4)
                direction |= Direction.South;

        //NORTH
        if (tile.Y + 1 < grid.GridSizeY)
            if (grid.genericGrid.GetGridObject(tile.X, tile.Y + 1).SpriteIndex == 4)
                direction |= Direction.North;

        return (int)(direction);
    }



    /// <summary>
    /// Rotate the 2d array data by - 90°
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    static Tile[,] RotateMatrix(Tile[,] matrix, int n)
    {
        Tile[,] ret = new Tile[n, n];

        for (int x = 0; x < n; ++x)
        {
            for (int y = 0; y < n; ++y)
            {
                ret[x, y] = matrix[n - y - 1, x];
            }
        }

        return ret;
    }
}

//if (Input.GetMouseButtonDown(0))
//{
//    Tile tile = grid.GetGridObject(Utils.GetMouseWorldPosition());
//    if (tile != null)
//    {
//        tile.SetTileSprite(Tile.TileSprite.ROAD);
//        int[] vertices =  visual.GetVerticesIndexForThisTile(Utils.GetMouseWorldPosition());

//        visual.uvs[vertices[0]] = new Vector2(0, 0);
//        visual.uvs[vertices[1]] = new Vector2(0.5f, 0);
//        visual.uvs[vertices[2]] = new Vector2(0, 0.5f);
//        visual.uvs[vertices[3]] = new Vector2(0.5f, 0.5f);

//        visual.UpdateMesh();

//    }
//    else
//    {
//        Debug.Log("Tile is NUll");
//    }
//}
//if (Input.GetMouseButtonDown(1))
//{
//    Tile tile = grid.GetGridObject(Utils.GetMouseWorldPosition());
//    if (tile != null)
//    {
//        tile.SetTileSprite(Tile.TileSprite.GRASS);
//        int[] vertices = visual.GetVerticesIndexForThisTile(Utils.GetMouseWorldPosition());

//        visual.uvs[vertices[0]] = new Vector2(0, 0.5f);
//        visual.uvs[vertices[1]] = new Vector2(0.5f, 0.5f);
//        visual.uvs[vertices[2]] = new Vector2(0, 1);
//        visual.uvs[vertices[3]] = new Vector2(0.5f, 1);

//        visual.UpdateMesh();

//    }
//    else
//    {
//        Debug.Log("Tile is NUll");
//    }
//}


