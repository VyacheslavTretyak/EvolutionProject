using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Creature))]
public class CreatureEditor : Editor
{	
	Texture2D treeNoiseTex;		
	int sizeTex = 256;	
	float freq = 0.01f;
	int octaves = 3;
	Color col1 = Color.black;
	Color col2 = Color.white;

	private void OnEnable()
	{
		treeNoiseTex = new Texture2D(sizeTex, sizeTex);
	}
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		EditorGUILayout.BeginVertical("Box");
		GUILayout.Label(treeNoiseTex);		
		freq = EditorGUILayout.FloatField("Frequency", freq);
		octaves = EditorGUILayout.IntField("Octaves", octaves);
		col1 = EditorGUILayout.ColorField("Color 1", col1);
		col2 = EditorGUILayout.ColorField("Color 2", col2);
		if (GUILayout.Button("Generate"))
		{
			treeNoiseTex = TextureGenerator.GetSkinTex(col1, col2, sizeTex, sizeTex, octaves, freq);
		}
		GUILayout.EndVertical();
	}	
}
