using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GridMesh), typeof(Grid), typeof(MeshRenderer))]
public class GridDisplay : MonoBehaviour
{
    ///// <summary>
    ///// Structure who hold the sprites positions in the texture
    ///// </summary>
    //[Serializable]
    //public struct Uvs
    //{
    //    public Vector2 uv00;
    //    public Vector2 uv10;
    //    public Vector2 uv01;
    //    public Vector2 uv11;
    //}

    public Grid grid;

    /// <summary>
    /// List who will hold the sprites positions in the texture
    /// </summary>
    public List<SpriteUvs> spritesCoordList;

    public Material material;

    /// <summary>
    /// The texture we will use
    /// </summary>
    public Texture texture;

    /// <summary>
    /// The resolution of the texture, must be a square texture
    /// </summary>
    public int textureResolution;

    /// <summary>
    /// Number of Sprites per row in the texture, minimum =  1
    /// </summary>
    public int row;

    /// <summary>
    /// Number of Sprites per column in the texture, minimum =  1
    /// </summary>
    public int column;

   

    /// <summary>
    /// size of the sprite in Pixel
    /// </summary>
    public float spriteSizeX;
    public float spriteSizeY;

    public int startSprite;
    public bool random;


    public Dictionary<int, int> roadDictionary = new Dictionary<int, int>();


    public void Init(Grid grid)
    {

        

        this.grid = grid;
        material = GetComponent<MeshRenderer>().sharedMaterial;
        texture = material.mainTexture;
        textureResolution = texture.height;
        spriteSizeX = textureResolution / row;
        spriteSizeY = textureResolution / column;

        StoreAllSpriteIntexture(spriteSizeX, spriteSizeY);

        //if (random)
        //    FillAllGridRandom();

        //else
        //    FillAllGrid();

        //ConstructRoadDictionary();

    }

    public void FillAllGrid(int spriteIndex)
    {
        for (int y = 0; y < grid.GridSizeX; y++)
        {
            for (int x = 0; x < grid.GridSizeY; x++)
            {
                Tile tile = grid.genericGrid.GetGridObject(x, y);
                tile.SetTileSpriteIndex(spriteIndex);
                //SetTileSprite(tile);
                //SetTileSprite(grid.genericGrid.GetGridObject(x, y), tile.spriteIndex);
            }
        }
        grid.gridMesh.UpdateUvs();
    }

    public void FillAllGrid()
    {
        for (int y = 0; y < grid.GridSizeX; y++)
        {
            for (int x = 0; x < grid.GridSizeY; x++)
            {
                Tile tile = grid.genericGrid.GetGridObject(x, y);
                tile.SetTileSpriteIndex(startSprite);
                //SetTileSprite(tile);
            }
        }
        grid.gridMesh.UpdateUvs();
    }

    private void FillAllGridRandom()
    {
        for (int y = 0; y < grid.GridSizeX; y++)
        {
            for (int x = 0; x < grid.GridSizeY; x++)
            {
                int rand = UnityEngine.Random.Range(0, (column*row));
                Tile tile = grid.genericGrid.GetGridObject(x, y);
                tile.SetTileSpriteIndex(rand);
               // SetTileSprite(tile);
                //SetTileSprite(grid.genericGrid.GetGridObject(x, y), tile.spriteIndex);
            }
        }
        grid.gridMesh.UpdateUvs();
    }

    /// <summary>
    /// Split the texture and store all the sprites in a list of Uv Coords
    /// </summary>
    private void StoreAllSpriteIntexture(float spriteSizeX, float spriteSizeY)
    {
        for (int y = 0; y < column; y++)
            for (int i = 0; i < row; i++)
                spritesCoordList.Add(GetSpriteUvsCoord(new Vector2(i * spriteSizeX, y * spriteSizeY), spriteSizeX, spriteSizeY));
    }

    private void ConstructRoadDictionary()
    {
        roadDictionary.Add(0, 1);
        roadDictionary.Add(1, 2);
        roadDictionary.Add(2, 11);
        roadDictionary.Add(3, 10);
        roadDictionary.Add(4, 1);
        roadDictionary.Add(5, 0);
        roadDictionary.Add(6, 9);
        roadDictionary.Add(7, 8);
        roadDictionary.Add(8, 7);
        roadDictionary.Add(9, 6);
        roadDictionary.Add(10, 15);
        roadDictionary.Add(11, 14);
        roadDictionary.Add(12, 5);
        roadDictionary.Add(13, 4);
        roadDictionary.Add(14, 13);
        roadDictionary.Add(15, 12);
    }

    /// <summary>
    /// Get a sprite UV Coord in the texture 
    /// </summary>
    /// <param name="spriteOrigin"></param>
    /// <param name="spriteSize"></param>
    /// <returns></returns>
    public SpriteUvs GetSpriteUvsCoord(Vector2 spriteOrigin, float spriteSizeX, float spriteSizeY)
    {
        SpriteUvs spriteUV = new SpriteUvs();

        spriteUV.uv00 = spriteOrigin / textureResolution;
        spriteUV.uv10 = (spriteOrigin + new Vector2(spriteSizeX, 0)) / textureResolution;
        spriteUV.uv01 = (spriteOrigin + new Vector2(0, spriteSizeY)) / textureResolution;
        spriteUV.uv11 = (spriteOrigin + new Vector2(spriteSizeX, spriteSizeY)) / textureResolution;
        

        return spriteUV;
    }


    /// <summary>
    /// Set a tile Sprite
    /// </summary>
    /// <param name="tile">The tile to set the visual to </param>
    /// <param name="spriteIndex">The index of the sprite in the texture </param>
    public void SetTileSprite(Tile tile, int spriteIndex)
    {
        SpriteUvs spriteUv = spritesCoordList[spriteIndex];
        grid.gridMesh.SetTileUvs(tile, spriteUv);
        tile.SetTileSpriteIndex(spriteIndex);
    }

    public void SetTileSprite(Tile tile)
    {
        SpriteUvs spriteUv = spritesCoordList[tile.SpriteIndex];
        grid.gridMesh.SetTileUvs(tile, spriteUv);
    }
    public void UpdateTile(Tile tile)
    {
        SetTileSprite(tile);
    }

}
