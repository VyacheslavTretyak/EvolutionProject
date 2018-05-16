using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaws : MonoBehaviour, IBody
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
		if (Own.Params.JawsParam.IsActive)
		{
			JawsParam p = Own.Params.JawsParam;
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
		float width = 0.2f * Own.Params.JawsParam.JawsLevel;
		float length = 0.1f * Own.Params.JawsParam.JawsLevel;
		float height = 0.2f*Generator.LOW_FIBONACCI * Own.Params.JawsParam.JawsLevel;
		AddToMesh(new Vector3(0, 0, 0.5f * length), Shapes.Box(width, length, height, texRect));
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.normals = normals.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.name = GetType().Name;
		return mesh;
	}
}
