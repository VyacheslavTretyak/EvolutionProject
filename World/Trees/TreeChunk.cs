using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChunk : MonoBehaviour
{
	public int SizeChunk { get; set; }
	public float TileWidth { get; set; }
	List<Vector3> vertices;
	List<Vector3> normals;
	List<Vector2> texture;
	List<int> triangles;
	Rect[] rects;
	Rect GetTexture(TreeType type, TreeMat mat)
	{
		return rects[(int)(type - 1) * 2 + (int)mat];
	}
	void AddMesh(Vector3 pos, Mesh mesh)
	{		
		Vector3[] arr = new Vector3[mesh.vertices.Length];
		for (int i = 0; i < mesh.vertices.Length; i++)
		{
			arr[i] = mesh.vertices[i] + pos;
		}
		int[] ind = new int[mesh.triangles.Length];
		for (int i = 0; i < mesh.triangles.Length; i++)
		{
			ind[i] = mesh.triangles[i] + vertices.Count;
		}
		vertices.AddRange(arr);
		triangles.AddRange(ind);
		normals.AddRange(mesh.normals);
		texture.AddRange(mesh.uv);
	}	
	void SetOak(float px, float pz, float tileHeight)
	{
		float height = Random.Range(1f, 2f);
		float rad = height * 0.15f;		
		AddMesh(new Vector3(px, tileHeight, pz), Shapes.Box(rad, rad, height, GetTexture(TreeType.Oak, TreeMat.Wood)));	
		int level = (int)(height / Random.Range(0.3f, 0.5f));
		for (int i = 0; i < level - 1; i++)
		{
			float e = height / level;
			int dir = Random.Range(0, 2);
			if(dir == 0)
			{
				float h = e * i + Random.Range(0, e);
				float d = Random.Range(rad * 0.5f, rad * 0.5f + i * 0.01f);
				float r = rad * 0.5f + d * 0.5f;
				AddMesh(new Vector3(px + r, tileHeight + h, pz), Shapes.Box(d, d, d, GetTexture(TreeType.Oak, TreeMat.Leafs)));
				h = e * i + Random.Range(0, e);
				d = Random.Range(rad * 0.5f, rad*0.5f + i*0.05f);
				r = rad * 0.5f + d * 0.5f;
				AddMesh(new Vector3(px - r, tileHeight + h, pz), Shapes.Box(d, d, d, GetTexture(TreeType.Oak, TreeMat.Leafs)));
			}
			else
			{
				float h = e * i + Random.Range(0, e);
				float d = Random.Range(rad * 0.5f, rad * 0.5f + i * 0.01f);
				float r = rad * 0.5f + d * 0.5f;
				AddMesh(new Vector3(px, tileHeight + h, pz + r), Shapes.Box(d, d, d, GetTexture(TreeType.Oak, TreeMat.Leafs)));
				h = e * i + Random.Range(0, e);
				d = Random.Range(rad * 0.5f, rad * 0.5f + i * 0.01f);
				r = rad * 0.5f + d * 0.5f;
				AddMesh(new Vector3(px, tileHeight + h, pz - r), Shapes.Box(d, d, d, GetTexture(TreeType.Oak, TreeMat.Leafs)));
			}			
		}
		float s = Random.Range(rad * 3f, rad * 4f);
		AddMesh(new Vector3(px, tileHeight + height, pz), Shapes.Box(s, s, s*1.2f, GetTexture(TreeType.Oak, TreeMat.Leafs)));		
	}
	void SetPine(float px, float pz, float tileHeight)
	{
		float height = Random.Range(0.5f, 1f);
		float rad = Random.Range(0.15f, 0.3f);
		AddMesh(new Vector3(px, tileHeight, pz), Shapes.Box(rad, rad, height, GetTexture(TreeType.Pine, TreeMat.Wood)));
		int level = Random.Range(3, 5);		
		float hPos = tileHeight + height;
		for (int i = 0; i < level; i++)
		{
			float r = (rad * (2 + level - i));
			float h = r;
			AddMesh(new Vector3(px, hPos, pz), Shapes.Cone(h, r, 0.1f, 6, GetTexture(TreeType.Pine, TreeMat.Leafs)));
			hPos += h * 0.75f;
		}
	}
	void SetCactus(float px, float pz, float tileHeight)
	{
		float height = Random.Range(1f, 1.5f);
		float rad = Random.Range(0.15f, 0.3f);
		AddMesh(new Vector3(px, tileHeight, pz), Shapes.Box(rad, rad, height, GetTexture(TreeType.Cactus, TreeMat.Wood)));
		int dir = Random.Range(0, 2);
		if (dir == 0)
		{
			float h = Random.Range(height * 0.2f, height * 0.4f);
			float len = Random.Range(0.1f, 0.3f);
			float r = rad * 0.5f + len * 0.5f;
			float rad1 = rad * 0.75f;
			AddMesh(new Vector3(px + r, tileHeight+ h, pz), Shapes.Box(len, rad1, rad1, GetTexture(TreeType.Cactus, TreeMat.Wood)));
			h = Random.Range(height * 0.6f, height * 0.9f);
			len = Random.Range(0.1f, 0.3f);
			r = rad * 0.5f + len * 0.5f;
			AddMesh(new Vector3(px - r, tileHeight + h, pz), Shapes.Box(len, rad1, rad1, GetTexture(TreeType.Cactus, TreeMat.Wood)));
		}
		else
		{
			float h = Random.Range(height * 0.2f, height * 0.4f);
			float len = Random.Range(0.1f, 0.3f);
			float r = rad * 0.5f + len * 0.5f;
			float rad1 = rad * 0.75f;
			AddMesh(new Vector3(px, tileHeight + h, pz+r), Shapes.Box(rad1, len, rad1, GetTexture(TreeType.Cactus, TreeMat.Wood)));
			h = Random.Range(height * 0.6f, height * 0.9f);
			len = Random.Range(0.1f, 0.3f);
			r = rad * 0.5f + len * 0.5f;
			AddMesh(new Vector3(px, tileHeight + h, pz-r), Shapes.Box(rad1, len, rad1, GetTexture(TreeType.Cactus, TreeMat.Wood)));
		}
	}
	void SetAcacia(float px, float pz, float tileHeight)
	{
		float height = Random.Range(1.5f, 2.5f);
		float rad = Random.Range(0.25f, 0.35f);
		AddMesh(new Vector3(px, tileHeight, pz), Shapes.Box(rad, rad, height, GetTexture(TreeType.Acacia, TreeMat.Wood)));
		float dw = Random.Range(height * 0.75f, height);
		float dl = Random.Range(height * 0.75f, height);
		AddMesh(new Vector3(px, tileHeight+height, pz), Shapes.Box(dw, dl, dw * 0.2f, GetTexture(TreeType.Acacia, TreeMat.Leafs)));
		//right branch
		float len = Random.Range(height*0.5f, height);
		float h = Random.Range(height * 0.6f, height * 0.8f);
		float r = (rad + len) * 0.5f;
		float rad1 = rad * 0.75f;		
		AddMesh(new Vector3(px + r, tileHeight+h, pz), Shapes.Box(len, rad1, rad1, GetTexture(TreeType.Acacia, TreeMat.Wood)));
		rad1 *= 0.75f;
		float h1 = Random.Range(0.5f, 0.75f) * (height - h);
		float r2 = rad * 0.5f + len + rad1 * 0.5f;
		AddMesh(new Vector3(px + r2, tileHeight + h, pz), Shapes.Box(rad1, rad1, h1, GetTexture(TreeType.Acacia, TreeMat.Wood)));
		dw = Random.Range(len * 0.5f, len * 0.75f);
		dl = Random.Range(len * 0.5f, len * 0.75f);
		AddMesh(new Vector3(px + r2 , tileHeight + h+h1, pz), Shapes.Box(dw, dl, dw * 0.2f, GetTexture(TreeType.Acacia, TreeMat.Leafs)));
		//left branch
		len = Random.Range(height * 0.5f, height);
		h = Random.Range(height * 0.6f, height * 0.8f);
		r = (rad + len) * 0.5f;
		rad1 = rad * 0.75f;		
		AddMesh(new Vector3(px - r, tileHeight + h, pz), Shapes.Box(len, rad1, rad1, GetTexture(TreeType.Acacia, TreeMat.Wood)));
		rad1 *= 0.75f;
		h1 = Random.Range(0.5f, 0.75f) * (height - h);
		r2 = rad * 0.5f + len + rad1 * 0.5f;
		AddMesh(new Vector3(px - r2, tileHeight + h, pz), Shapes.Box(rad1, rad1, h1, GetTexture(TreeType.Acacia, TreeMat.Wood)));
		dw = Random.Range(len * 0.5f, len * 0.75f);
		dl = Random.Range(len * 0.5f, len * 0.75f);
		AddMesh(new Vector3(px - r2, tileHeight + h + h1, pz), Shapes.Box(dw, dl, dw * 0.2f, GetTexture(TreeType.Acacia, TreeMat.Leafs)));
		//forward branch
		len = Random.Range(height * 0.25f, height * 0.5f);
		h = Random.Range(height * 0.6f, height * 0.8f);
		r = (rad + len) * 0.5f;
		rad1 = rad * 0.75f;
		AddMesh(new Vector3(px, tileHeight + h, pz + r), Shapes.Box(rad1, len, rad1, GetTexture(TreeType.Acacia, TreeMat.Wood)));
		rad1 *= 0.75f;			
		h1 = Random.Range(0.5f, 0.75f) * (height - h);
		r2 = rad * 0.5f + len + rad1 * 0.5f;
		AddMesh(new Vector3(px, tileHeight + h, pz + r2), Shapes.Box(rad1, rad1, h1, GetTexture(TreeType.Acacia, TreeMat.Wood)));
		dw = Random.Range(len * 0.5f, len * 0.75f);
		dl = Random.Range(len * 0.5f, len * 0.75f);
		AddMesh(new Vector3(px, tileHeight + h + h1, pz + r2), Shapes.Box(dw, dl, dw * 0.2f, GetTexture(TreeType.Acacia, TreeMat.Leafs)));
		
		//back branch
		len = Random.Range(height * 0.25f, height * 0.5f);
		h = Random.Range(height * 0.6f, height * 0.8f);
		r = rad * 0.5f + len * 0.5f;
		rad1 = rad * 0.75f;
		AddMesh(new Vector3(px, tileHeight + h, pz - r), Shapes.Box(rad1, len, rad1, GetTexture(TreeType.Acacia, TreeMat.Wood)));
		rad1 *= 0.75f;
		h1 = Random.Range(0.5f, 0.75f) * (height - h);
		r2 = rad * 0.5f + len + rad1 * 0.5f;
		AddMesh(new Vector3(px, tileHeight + h, pz - r2), Shapes.Box(rad1, rad1, h1, GetTexture(TreeType.Acacia, TreeMat.Wood)));
		dw = Random.Range(len * 0.5f, len * 0.75f);
		dl = Random.Range(len * 0.5f, len * 0.75f);
		AddMesh(new Vector3(px, tileHeight + h + h1, pz - r2), Shapes.Box(dw, dl, dw * 0.2f, GetTexture(TreeType.Acacia, TreeMat.Leafs)));
	}
	void SetRainforest(float px, float pz, float tileHeight)
	{
		float height = Random.Range(3f, 5f);
		float rad = height * 0.1f;		
		AddMesh(new Vector3(px, tileHeight, pz), Shapes.Box(rad, rad, height, GetTexture(TreeType.Rainforest, TreeMat.Wood)));
		int level = Random.Range(5, 8);
		for (int i = 0; i < level - 1; i++)
		{
			float e = height / level;
			int dir = Random.Range(0, 2);
			if (dir == 0)
			{
				float h = Random.Range(0, e) + e * i;
				float d = Random.Range(0.5f, 0.5f + i * 0.2f);
				float r = rad * 0.5f + d * 0.5f;
				AddMesh(new Vector3(px + r, tileHeight + h, pz), Shapes.Box(d, d, d * 0.33f, GetTexture(TreeType.Rainforest, TreeMat.Leafs)));
				h = Random.Range(0, e) + e * i;
				d = Random.Range(0.5f, 0.5f + i * 0.2f);
				r = rad * 0.5f + d * 0.5f;
				AddMesh(new Vector3(px - r, tileHeight + h, pz), Shapes.Box(d, d, d * 0.33f, GetTexture(TreeType.Rainforest, TreeMat.Leafs)));
			}
			else
			{
				float h = Random.Range(0, e) + e * i;
				float d = Random.Range(0.5f, 0.5f + i * 0.2f);
				float r = rad * 0.5f + d * 0.5f;
				AddMesh(new Vector3(px, tileHeight + h, pz + r), Shapes.Box(d, d, d * 0.33f, GetTexture(TreeType.Rainforest, TreeMat.Leafs)));
				h = Random.Range(0, e) + e * i;
				d = Random.Range(0.5f, 0.5f + i * 0.2f);
				r = rad * 0.5f + d * 0.5f;
				AddMesh(new Vector3(px, tileHeight + h, pz - r), Shapes.Box(d, d, d * 0.33f, GetTexture(TreeType.Rainforest, TreeMat.Leafs)));
			}
		}
		float up = Random.Range(height * 0.3f, height * 0.5f);		
		AddMesh(new Vector3(px, tileHeight + height, pz), Shapes.Box(up, up, up * 0.2f, GetTexture(TreeType.Rainforest, TreeMat.Leafs)));
	}

	public void CreateMesh(int xPos, int zPos, ref Generator map, Rect[] textureRects)
    {
		rects = textureRects;
		vertices = new List<Vector3>();
		normals = new List<Vector3>();
		texture = new List<Vector2>();
		triangles = new List<int>();		
		for (int x = 0; x < SizeChunk; x++)
		{
			for (int z = 0; z < SizeChunk; z++)
			{
				Tile tile = map[xPos * SizeChunk + x, zPos * SizeChunk + z];
				if (tile.TreeT == TreeType.NULL) 
				{
					continue;
				}				
				float px = x + TileWidth * 0.5f;
				float pz = z + TileWidth * 0.5f;
				switch (tile.TreeT)
				{
					case TreeType.Oak:
						SetOak(px, pz, tile.HeightValue);
						break;
					case TreeType.Pine:
						SetPine(px, pz, tile.HeightValue);
						break;
					case TreeType.Cactus:
						SetCactus(px, pz, tile.HeightValue);
						break;
					case TreeType.Acacia:
						SetAcacia(px, pz, tile.HeightValue);
						break;
					case TreeType.Rainforest:
						SetRainforest(px, pz, tile.HeightValue);
						break;
				}				
			}
		}
		Mesh mesh = new Mesh {
			name = "TreeMesh",
			vertices = vertices.ToArray(),
			triangles = triangles.ToArray(),
			normals = normals.ToArray(),
			uv = texture.ToArray()
		};
		GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
