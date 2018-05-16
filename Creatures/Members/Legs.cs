using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour, IBody
{
	public Creature Own { get; set; }	

	List<Vector3> vertices;
	List<int> triangles;
	List<Vector3> normals;
	List<Vector2> uvs;
	void Start()
	{
		
		Redraw();
	}
	public void Redraw()
	{
		
		if (Own.Params.LegsParam.IsActive)
		{			
			GetComponent<MeshFilter>().mesh = GetMesh();
			LegsParam p = Own.Params.LegsParam;
			GetComponent<MeshRenderer>().material.mainTexture = TextureGenerator.GetSkinTex(p.Color1, p.Color2, 32, 32, (int)p.SkinTextureOctave, p.SkinTextureFreq);
			Vector3 v = GetSize();
			Own.BoxCol.center = new Vector3(0, 0.5f-v.y*0.5f, -0.5f);
			Own.BoxCol.size = new Vector3(1f, 1f + v.y, 1f);
		}
		else
		{			
			GetComponent<MeshFilter>().mesh = null;
			Own.BoxCol.center = new Vector3(0, 0.5f, -0.5f);
			Own.BoxCol.size = new Vector3(1f, 1f, 1f);
		}
	}	
	void AddToMesh(Vector3 pos, Mesh aMesh)
	{
		Vector3[] arr = new Vector3[aMesh.vertices.Length];
		for (int i = 0; i < aMesh.vertices.Length; i++)
		{
			arr[i] = aMesh.vertices[i] + pos;
		}
		int[] ind = new int[aMesh.triangles.Length];
		for (int i = 0; i < aMesh.triangles.Length; i++)
		{
			ind[i] = aMesh.triangles[i] + vertices.Count;
		}
		vertices.AddRange(arr);
		triangles.AddRange(ind);
		normals.AddRange(aMesh.normals);
		uvs.AddRange(aMesh.uv);
	}
	public Vector3 GetSize()
	{
		if (!Own.Params.LegsParam.IsActive)
		{
			return Vector3.zero;
		}
		Vector3 v = new Vector3();
		v.x = 0.1f * Own.Params.LegsParam.LegsLevel * 0.5f;
		v.z= 0.1f * Own.Params.LegsParam.LegsLevel * 0.5f;
		v.y= 0.1f * Own.Params.LegsParam.LegsLevel * 2f;
		return v;
	}
	Mesh GetMesh()
	{
		Mesh mesh = new Mesh();
		vertices = new List<Vector3>();
		triangles = new List<int>();
		normals = new List<Vector3>();
		uvs = new List<Vector2>();
		Rect texRect = new Rect(0, 0, 1f, 1f);
		Vector3 v = GetSize();
		float width = v.x;
		float length = v.z;
		float height = v.y;		
		AddToMesh(new Vector3(-Generator.LOW_FIBONACCI * 0.5f, -height, 0.5f * length - Generator.LOW_FIBONACCI), Shapes.Box(width, length, height, texRect));
		AddToMesh(new Vector3(Generator.LOW_FIBONACCI * 0.5f, -height, 0.5f * length - Generator.LOW_FIBONACCI), Shapes.Box(width, length, height, texRect));
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.normals = normals.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.name = GetType().Name;
		return mesh;
	}
}
