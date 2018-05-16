using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsChunk : MonoBehaviour
{
    public int SizeChunk { get; set; }
    public float TileWidth { get; set; }
	int xChunkPos;
	int zChunkPos;
	Generator map;
	Rect[] rectsChunks;    
    public Rect GetTexture(int type, ref Rect[] rect)
    {
        return rect[type - 1];
    }
	public void ReBuildMesh()
	{
		CreateMesh(xChunkPos, zChunkPos, ref map, ref rectsChunks);
	}

    public void CreateMesh(int xPos, int zPos, ref Generator map, ref Rect[] rects)
    {
		xChunkPos = xPos;
		zChunkPos = zPos;
		this.map = map;
		rectsChunks = rects;
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> texture = new List<Vector2>();
        List<int> triangles = new List<int>();

        for (int x = 0; x < SizeChunk; x++)
        {
            for (int z = 0; z < SizeChunk; z++)
            {
                Tile tile = map[xPos * SizeChunk + x, zPos * SizeChunk + z];
                if (tile.PlantT == PlantType.NULL)
                {
                    continue;
                }
                float r = tile.AgePlant * 0.25f;
                float h = tile.AgePlant * 0.5f;
                float half = TileWidth * 0.5f;
                Rect rect = GetTexture((int)tile.PlantT, ref rects);

                vertices.Add(new Vector3(x + half - r, tile.HeightValue, z + half - r));
                vertices.Add(new Vector3(x + half + r, tile.HeightValue, z + half + r));
                vertices.Add(new Vector3(x + half - r, tile.HeightValue + h, z + half - r));
                vertices.Add(new Vector3(x + half + r, tile.HeightValue + h, z + half + r));
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 1);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 1);
                triangles.Add(vertices.Count - 2);
                for (int f = 0; f < 4; f++)
                {
                    normals.Add(new Vector3(-1f, 0, 1f));
                }
                texture.Add(new Vector2(rect.xMin, rect.yMin));
                texture.Add(new Vector2(rect.xMax, rect.yMin));
                texture.Add(new Vector2(rect.xMin, rect.yMax));
                texture.Add(new Vector2(rect.xMax, rect.yMax));

                vertices.Add(new Vector3(x + half - r, tile.HeightValue, z + half - r));
                vertices.Add(new Vector3(x + half + r, tile.HeightValue, z + half + r));
                vertices.Add(new Vector3(x + half - r, tile.HeightValue + h, z + half - r));
                vertices.Add(new Vector3(x + half + r, tile.HeightValue + h, z + half + r));
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                for (int f = 0; f < 4; f++)
                {
                    normals.Add(new Vector3(1f, 0, -1f));
                }
                texture.Add(new Vector2(rect.xMax, rect.yMin));
                texture.Add(new Vector2(rect.xMin, rect.yMin));
                texture.Add(new Vector2(rect.xMax, rect.yMax));
                texture.Add(new Vector2(rect.xMin, rect.yMax));

                vertices.Add(new Vector3(x + half - r, tile.HeightValue, z + half + r));
                vertices.Add(new Vector3(x + half + r, tile.HeightValue, z + half - r));
                vertices.Add(new Vector3(x + half - r, tile.HeightValue + h, z + half + r));
                vertices.Add(new Vector3(x + half + r, tile.HeightValue + h, z + half - r));
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 1);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 1);
                triangles.Add(vertices.Count - 2);
                for (int f = 0; f < 4; f++)
                {
                    normals.Add(new Vector3(-1f, 0, -1f));
                }
                texture.Add(new Vector2(rect.xMin, rect.yMin));
                texture.Add(new Vector2(rect.xMax, rect.yMin));
                texture.Add(new Vector2(rect.xMin, rect.yMax));
                texture.Add(new Vector2(rect.xMax, rect.yMax));

                vertices.Add(new Vector3(x + half - r, tile.HeightValue, z + half + r));
                vertices.Add(new Vector3(x + half + r, tile.HeightValue, z + half - r));
                vertices.Add(new Vector3(x + half - r, tile.HeightValue + h, z + half + r));
                vertices.Add(new Vector3(x + half + r, tile.HeightValue + h, z + half - r));
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                for (int f = 0; f < 4; f++)
                {
                    normals.Add(new Vector3(1f, 0, 1f));
                }
                texture.Add(new Vector2(rect.xMax, rect.yMin));
                texture.Add(new Vector2(rect.xMin, rect.yMin));
                texture.Add(new Vector2(rect.xMax, rect.yMax));
                texture.Add(new Vector2(rect.xMin, rect.yMax));
            }
        }
        Mesh mesh = new Mesh
        {
            name = "PlantMesh",
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            normals = normals.ToArray(),
            uv = texture.ToArray()
        };
        GetComponent<MeshFilter>().sharedMesh = mesh;       
    }
}
