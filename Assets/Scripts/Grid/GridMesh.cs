using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridMesh : MonoBehaviour
{
    Grid grid;

    public bool debug;
    public Mesh mesh;

    public Vector3[] vertices = new Vector3[0];
    public int[] triangles = new int[0];
    public Vector2[] uvs = new Vector2[0];

    public Testing.MapOrientation currenMapOrientation = Testing.MapOrientation.NORTH;



    public void Init(Grid grid)
    {
        currenMapOrientation = Testing.MapOrientation.NORTH;
        this.grid = grid;

        CreateDiscreteIsometricMesh();
        UpdateMesh();

        
    }

    private void CreateDiscreteIsometricMesh()
    {
       
        mesh = new Mesh();
        vertices = new Vector3[(grid.GridSizeX * grid.GridSizeY) * 4];
        uvs = new Vector2[vertices.Length];
        triangles = new int[grid.genericGrid.Height * grid.genericGrid.Width * 6];

        int verticesIndex = 0;
        int trisIndex = 0;

        for (int y = 0; y < grid.GridSizeY; y++)
        {
            for (int x = 0; x < grid.GridSizeX; x++)
            {
                #region Vertices 

                //Origin
                //vertices[verticesIndex] = grid.genericGrid.GetWorldPositionCartToIso(x, y);
                vertices[verticesIndex] = new Vector3(x, y);

                // Right
                //vertices[verticesIndex + 1] = vertices[verticesIndex] + new Vector3(1, 0.5f);
                vertices[verticesIndex + 1] = vertices[verticesIndex] + new Vector3(1, 0);

                //Left
                //vertices[verticesIndex + 2] = vertices[verticesIndex] + new Vector3(-1, 0.5f);
                vertices[verticesIndex + 2] = vertices[verticesIndex] + new Vector3(0,1);

                //Top
                //vertices[verticesIndex + 3] = vertices[verticesIndex] + new Vector3(0, 1);
                vertices[verticesIndex + 3] = vertices[verticesIndex] + new Vector3(1, 1);


                #endregion

                #region Triangles

                triangles[trisIndex + 0] = verticesIndex + 0;
                triangles[trisIndex + 1] = verticesIndex + 2;
                triangles[trisIndex + 2] = verticesIndex + 1;
                triangles[trisIndex + 3] = verticesIndex + 1;
                triangles[trisIndex + 4] = verticesIndex + 2;
                triangles[trisIndex + 5] = verticesIndex + 3;

                #endregion

                #region Debug

                if (debug)
                {
                    //Bottom
                    Utils.CreateWorldText("VerticieIndex " + verticesIndex, verticesIndex.ToString(), null, vertices[verticesIndex] + new Vector3(0, 0.1f) + grid.genericGrid.originPosition, 2, Color.red, TextAnchor.MiddleCenter);
                    // Right
                    Utils.CreateWorldText("VerticieIndex " + verticesIndex, (verticesIndex + 1).ToString(), null, vertices[verticesIndex + 1] + new Vector3(-0.2f, 0) + grid.genericGrid.originPosition, 2, Color.blue, TextAnchor.MiddleCenter);
                    //Left
                    Utils.CreateWorldText("VerticieIndex " + verticesIndex, (verticesIndex + 2).ToString(), null, vertices[verticesIndex + 2] + new Vector3(0.2f, 0) + grid.genericGrid.originPosition, 2, Color.green, TextAnchor.MiddleCenter);
                    //Top
                    Utils.CreateWorldText("VerticieIndex " + verticesIndex, (verticesIndex + 3).ToString(), null, vertices[verticesIndex + 3] + new Vector3(0, -0.1f) + grid.genericGrid.originPosition, 2, Color.yellow, TextAnchor.MiddleCenter);
                }

                #endregion

                verticesIndex += 4;
                trisIndex += 6;
            }
        }

    }

    //public void SetTileSprite(Tile tile, int spriteIndex)
    //{
    //    int[] vertices = GetVerticesIndexForThisTile(tile);

    //    TileMapSprite.SpriteUV spriteUv = tilemapSprite.sprites[3];
    //    uvs[vertices[0]] = spriteUv.uv00;
    //    uvs[vertices[1]] = spriteUv.uv01;
    //    uvs[vertices[2]] = spriteUv.uv10;
    //    uvs[vertices[3]] = spriteUv.uv11;

    //    UpdateMesh();
    //}
    public void UpdateMesh()
    {
        mesh.name = "Procedural Grid";

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        UpdateUvs();
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void UpdateUvs()
    {
        mesh.uv = uvs;
    }

    public int[] GetVerticesIndexForThisTile(Tile tile)
    {
        Vector3[] vertices = mesh.vertices;
        int[] TileVertices = new int[4];

        int X, Y;
        Utils.GetTileCoordinatesWithRotation(grid.GridSizeX, tile.X, tile.Y, out X, out Y, currenMapOrientation);
       
        int index = X * 4 + Y * (grid.genericGrid.Width * 4);

        TileVertices[0] = index;
        TileVertices[1] = index + 1;
        TileVertices[2] = index + 2;
        TileVertices[3] = index + 3;

        //int index = tile.X * 4 + tile.Y * (grid.genericGrid.Width * 4);

        //TileVertices[0] = index;
        //TileVertices[1] = index + 1;
        //TileVertices[2] = index + 2;
        //TileVertices[3] = index + 3;





        //if (!debug)
        //{
        //    Utils.CreateWorldText("TileVerticeIndex " + index, index.ToString(), null, vertices[index], 3, Color.cyan, TextAnchor.MiddleCenter);
        //    Utils.CreateWorldText("TileVerticeIndex " + index, (index + 1).ToString(), null, vertices[index + 1], 3, Color.cyan, TextAnchor.MiddleCenter);
        //    Utils.CreateWorldText("TileVerticeIndex " + index, (index + 2).ToString(), null, vertices[index + 2], 3, Color.cyan, TextAnchor.MiddleCenter);
        //    Utils.CreateWorldText("TileVerticeIndex " + index, (index + 3).ToString(), null, vertices[index + 3], 3, Color.cyan, TextAnchor.MiddleCenter);
        //}

        return TileVertices;
    }

    

    /// <summary>
    /// Set a tile Uv
    /// </summary>
    /// <param name="tile">The tile to set the visual</param>
    /// <param name="spriteIndex">The sprite index in the texture </param>
    public void SetTileUvs(Tile tile, SpriteUvs spriteUvs)
    {
        int[] vertices = GetVerticesIndexForThisTile(tile);

        uvs[vertices[0]] = spriteUvs.uv00;
        uvs[vertices[1]] = spriteUvs.uv10;
        uvs[vertices[2]] = spriteUvs.uv01;
        uvs[vertices[3]] = spriteUvs.uv11;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 25), "UpdateMesh"))
        {
            UpdateMesh();
        }
    }




    private void CreateContiguousIsometricMesh()
    {
        //    mesh = new Mesh();
        //    vertices = GetMeshVertices();
        //    triangles = new int[grid.Height * grid.Width * 6];

        //    int verticesIndex = 0;
        //    int trisIndex = 0;

        //    for (int j = 0; j < grid.Height; j++)
        //    {
        //        for (int i = 0; i < grid.Width; i++)
        //        {
        //            triangles[trisIndex + 0] = verticesIndex + 0;
        //            triangles[trisIndex + 1] = verticesIndex + grid.Width + 1;
        //            triangles[trisIndex + 2] = verticesIndex + grid.Width + 2;
        //            triangles[trisIndex + 3] = verticesIndex + 0;
        //            triangles[trisIndex + 4] = verticesIndex + grid.Width + 2;
        //            triangles[trisIndex + 5] = verticesIndex + 1;

        //            trisIndex += 6;
        //            verticesIndex += 1;
        //        }
        //        verticesIndex += 1;
        //    }

        //    uvs = new Vector2[vertices.Length];
        //    uvs = GetMeshUvs();

    }

    //private Vector3[] GetMeshVertices()
    //{
    //    Vector3[] positions = new Vector3[(genericGrid.Width + 1) * (genericGrid.Height + 1)];
    //    int index = 0;

    //    for (int y = 0; y < genericGrid.Height + 1; y++)
    //    {
    //        for (int x = 0; x < genericGrid.Width + 1; x++)
    //        {
    //            positions[index] = genericGrid.GetWorldPositionCartToIso(x, y);
    //            // Utils.CreateWorldText(index.ToString(), null, positions[index], 2, Color.red, TextAnchor.MiddleCenter); ;
    //            index++;
    //        }
    //    }
    //    return positions;
    //}

    //private Vector2[] GetMeshUvs()
    //{
    //    Vector2[] uvs = new Vector2[vertices.Length];

    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        uvs[i] = genericGrid.GetWorldPositionIsoToCart(vertices[i]);
    //    }

    //    return uvs;
    //}

    //private Vector3[] GetIndividualCellVertices(int x, int y)
    //{
    //    Vector3[] positions = new Vector3[4];

    //    // Get the Tile Position
    //    Vector3 cellPos = genericGrid.GetWorldPositionCartToIso(x, y);

    //    // Offset the TilePos 
    //    Vector3 righttPos = cellPos + new Vector3(1, 0.5f);
    //    Vector3 leftPos = cellPos + new Vector3(-1, 0.5f);
    //    Vector3 topPos = cellPos + new Vector3(0, 1);

    //    positions[0] = cellPos;
    //    positions[1] = righttPos;
    //    positions[2] = leftPos;
    //    positions[3] = topPos;

    //    return positions;
    //}
    //public int[] GetVerticesIndexForThisTile(Vector3 worldPos)
    //{
    //    Vector3[] vertices = mesh.vertices;

    //    int[] tileVertices = new int[4];

    //    int xPos, yPos;
    //    grid.genericGrid.GetXY(worldPos, out xPos, out yPos);

    //    int index = xPos * 4 + yPos * (grid.genericGrid.Width * 4);

    //    tileVertices[0] = index;
    //    tileVertices[1] = index + 1;
    //    tileVertices[2] = index + 2;
    //    tileVertices[3] = index + 3;

    //    //if (debug)
    //    //{
    //    //    Utils.CreateWorldText("TileVerticeIndex " + index, index.ToString(), null, vertices[index], 3, Color.cyan, TextAnchor.MiddleCenter);
    //    //    Utils.CreateWorldText("TileVerticeIndex " + index, (index + 1).ToString(), null, vertices[index + 1], 3, Color.cyan, TextAnchor.MiddleCenter);
    //    //    Utils.CreateWorldText("TileVerticeIndex " + index, (index + 2).ToString(), null, vertices[index + 2], 3, Color.cyan, TextAnchor.MiddleCenter);
    //    //    Utils.CreateWorldText("TileVerticeIndex " + index, (index + 3).ToString(), null, vertices[index + 3], 3, Color.cyan, TextAnchor.MiddleCenter);
    //    //}

    //    return tileVertices;
    //}
    //public int[] GetVerticesIndexForThisTile(Vector3 worldPos)
    //{
    //    Vector3[] vertices = mesh.vertices;

    //    int[] tileVertices = new int[4];

    //    int xPos, yPos;
    //    grid.genericGrid.GetXY(worldPos, out xPos, out yPos);

    //    int index = xPos * 4 + yPos * (grid.genericGrid.Width * 4);

    //    tileVertices[0] = index;
    //    tileVertices[1] = index + 1;
    //    tileVertices[2] = index + 2;
    //    tileVertices[3] = index + 3;

    //    //if (debug)
    //    //{
    //    //    Utils.CreateWorldText("TileVerticeIndex " + index, index.ToString(), null, vertices[index], 3, Color.cyan, TextAnchor.MiddleCenter);
    //    //    Utils.CreateWorldText("TileVerticeIndex " + index, (index + 1).ToString(), null, vertices[index + 1], 3, Color.cyan, TextAnchor.MiddleCenter);
    //    //    Utils.CreateWorldText("TileVerticeIndex " + index, (index + 2).ToString(), null, vertices[index + 2], 3, Color.cyan, TextAnchor.MiddleCenter);
    //    //    Utils.CreateWorldText("TileVerticeIndex " + index, (index + 3).ToString(), null, vertices[index + 3], 3, Color.cyan, TextAnchor.MiddleCenter);
    //    //}

    //    return tileVertices;
    //}

}
