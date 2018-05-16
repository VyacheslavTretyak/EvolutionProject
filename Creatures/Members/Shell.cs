using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour, IBody
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
		if (Own.Params.ShellParam.IsActive)
		{
			ShellParam p = Own.Params.ShellParam;
			GetComponent<MeshRenderer>().material.mainTexture = TextureGenerator.GetSkinTex(p.Color1, p.Color2, 32, 32, (int)p.SkinTextureOctave, p.SkinTextureFreq);
			GetComponent<MeshFilter>().mesh = GetMesh();
		}
		else
		{
			//GetComponent<MeshRenderer>().material.mainTexture = null;
			GetComponent<MeshFilter>().mesh = null;
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
	Mesh GetMesh()
	{
		Mesh mesh = new Mesh();
		vertices = new List<Vector3>();
		triangles = new List<int>();
		normals = new List<Vector3>();
		uvs = new List<Vector2>();
		Rect texRect = new Rect(0, 0, 1f, 1f);
		float w = Own.Params.ShellParam.ShellLevel * 0.1f; 
		float l = Own.Params.ShellParam.ShellLevel * 0.1f; 
		float h = Own.Params.ShellParam.ShellLevel * 0.05f;
		float width = 1f + w;
		float length = 1f + l;
		float height = 1f + h;
		AddToMesh(new Vector3(0, h, -0.5f - l), Shapes.Box(width, length, height, texRect));
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.normals = normals.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.name = GetType().Name;
		return mesh;
	}
}
