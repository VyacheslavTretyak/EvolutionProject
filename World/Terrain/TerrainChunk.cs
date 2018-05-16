using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TextureSide
{
	UP,
	SIDE,
	BOTTOM
}
public class TerrainChunk : MonoBehaviour
{
    public int SizeChunk{ get; set; }
    public float TileWidth { get; set; }

    private List<Vector3> vertices;
    private List<Vector3> normals;
    private List<Vector2> texture;
    private List<int> triangles;   
    public Rect GetTexture(int type, int side, ref Rect[] rect)
    {
        return rect[type * 3 + side];
    }    
    private void SetTriangle(Vector3 norm, Rect tex)
    {       
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 1);
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 1);
        triangles.Add(vertices.Count - 2);
        for (int f = 0; f < 4; f++)
        {
            normals.Add(norm);
        }
        texture.Add(new Vector2(tex.xMin, tex.yMin));
        texture.Add(new Vector2(tex.xMax, tex.yMin));
        texture.Add(new Vector2(tex.xMin, tex.yMax));
        texture.Add(new Vector2(tex.xMax, tex.yMax));
    }
    private void AddQuad(Vector2[] v, float h1, float h2, int type, ref Rect[] textures, Vector3 norm)
    {
        Rect tex;
        float h = h1 - h2;
        if (h < TileWidth)
        {
            vertices.Add(new Vector3(v[0].x, h2, v[0].y));
            vertices.Add(new Vector3(v[1].x, h2, v[1].y));
            vertices.Add(new Vector3(v[0].x, h1, v[0].y));
            vertices.Add(new Vector3(v[1].x, h1, v[1].y));           
            tex = GetTexture(type, 1, ref textures);
            tex.yMin = tex.yMax - tex.height * h;
            SetTriangle(norm, tex);
        }
        else
        {
            float n = TileWidth;
            vertices.Add(new Vector3(v[0].x, h1 - n, v[0].y));
            vertices.Add(new Vector3(v[1].x, h1 - n, v[1].y));
            vertices.Add(new Vector3(v[0].x, h1, v[0].y));
            vertices.Add(new Vector3(v[1].x, h1, v[1].y));                   
            tex = GetTexture(type, 1, ref textures);
            tex.yMin = tex.yMax - tex.height * TileWidth;
            SetTriangle(norm, tex);
            while (h > TileWidth)
            {
                vertices.Add(new Vector3(v[0].x, h1 - n - TileWidth, v[0].y));
                vertices.Add(new Vector3(v[1].x, h1 - n - TileWidth, v[1].y));
                vertices.Add(new Vector3(v[0].x, h1 - n, v[0].y));
                vertices.Add(new Vector3(v[1].x, h1 - n, v[1].y));                
                tex = GetTexture(type, 2, ref textures);
                tex.yMin = tex.yMax - tex.height * TileWidth;
                SetTriangle(norm, tex);
                n+=TileWidth;
                h-=TileWidth;
            }            
        }
    }
    internal void CreateMesh(int xPos, int zPos, ref Generator map, ref Rect[] rect)
    {        
        vertices  = new List<Vector3>(); 
        normals = new List<Vector3>();
        texture = new List<Vector2>();
        triangles = new List<int>();       

        for (int x = 0; x < SizeChunk; x++)
        {
            for (int z = 0; z < SizeChunk; z++)
            {
                Tile tile = map[xPos * SizeChunk + x, zPos * SizeChunk + z];
                Rect rectUp = GetTexture((int)tile.Type, (int)TextureSide.UP, ref rect);             
                vertices.Add(new Vector3(x,             tile.HeightValue, z+TileWidth));
                vertices.Add(new Vector3(x + TileWidth, tile.HeightValue, z+TileWidth));
                vertices.Add(new Vector3(x,             tile.HeightValue, z));
                vertices.Add(new Vector3(x + TileWidth, tile.HeightValue, z));
                SetTriangle(Vector3.up, rectUp);                               
                
                float wall = -10f;
                if (tile.right == null)
                {
                    Vector2[] v = {new Vector2(x+TileWidth, z+TileWidth),
                                    new Vector2(x+TileWidth, z) };
                    AddQuad(v, tile.HeightValue, wall, (int)tile.Type, ref rect, Vector3.right);
                }
                else if (tile.HeightValue > tile.right.HeightValue)
                {
                    Vector2[] v = {new Vector2(x+TileWidth, z+TileWidth),
                                    new Vector2(x+TileWidth, z) };
                    AddQuad(v, tile.HeightValue, tile.right.HeightValue, (int)tile.Type, ref rect, Vector3.right);
                }
                if (tile.forward == null)
                {
                    Vector2[] v = {new Vector2(x, z+TileWidth),
                                   new Vector2(x+TileWidth, z+TileWidth)};
                    AddQuad(v, tile.HeightValue, wall, (int)tile.Type, ref rect, Vector3.forward);
                }
                else if (tile.HeightValue > tile.forward.HeightValue)
                {

                    Vector2[] v = {new Vector2(x, z+TileWidth),
                                   new Vector2(x+TileWidth, z+TileWidth)};
                    AddQuad(v, tile.HeightValue, tile.forward.HeightValue, (int)tile.Type, ref rect, Vector3.forward);
                }
                if (tile.left == null)
                {
                    Vector2[] v = { new Vector2(x, z),
                                    new Vector2(x, z+TileWidth)};
                    AddQuad(v, tile.HeightValue, wall, (int)tile.Type, ref rect, Vector3.left);
                }
                else if (tile.HeightValue > tile.left.HeightValue)
                {
                    Vector2[] v = { new Vector2(x, z),
                                    new Vector2(x, z+TileWidth)};
                    AddQuad(v, tile.HeightValue, tile.left.HeightValue, (int)tile.Type, ref rect, Vector3.left);
                }
                if (tile.back == null)
                {
                    Vector2[] v = {  new Vector2(x + TileWidth, z),
                                     new Vector2(x, z)};
                    AddQuad(v, tile.HeightValue, wall, (int)tile.Type, ref rect, Vector3.back);
                }
                else if (tile.HeightValue > tile.back.HeightValue)
                {
                    Vector2[] v = {  new Vector2(x + TileWidth, z),
                                     new Vector2(x, z)};
                    AddQuad(v, tile.HeightValue, tile.back.HeightValue, (int)tile.Type, ref rect, Vector3.back);
                }                
            }
        }
        Mesh mesh = new Mesh
        {
            name = "ChunkMesh",
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            normals = normals.ToArray(),
            uv = texture.ToArray()
        };
        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
