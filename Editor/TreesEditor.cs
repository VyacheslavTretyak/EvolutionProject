using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Trees))]
public class TreesEditor : Editor
{
	bool statusNoise = false;
	bool statusPlants = false;	
	Texture2D treeNoiseTex;	
	SerializedProperty treeOct;
	SerializedProperty treeFre;	
	SerializedProperty oak;
	SerializedProperty pine;
	SerializedProperty cactus;
	SerializedProperty acacia;
	SerializedProperty rainforest;
	int sizeTex = 256;

	private void OnEnable()
	{
		treeOct = serializedObject.FindProperty("treeOctaves");
		treeFre = serializedObject.FindProperty("treeFrequency");		
		oak = serializedObject.FindProperty("oak");
		pine = serializedObject.FindProperty("pine");
		cactus = serializedObject.FindProperty("cactus");
		acacia= serializedObject.FindProperty("acacia");
		rainforest = serializedObject.FindProperty("rainforest");
		treeNoiseTex = new Texture2D(sizeTex, sizeTex);
	}
	public override void OnInspectorGUI()
	{
		GUIStyle styleFold = EditorStyles.foldout;
		styleFold.fontStyle = FontStyle.Bold;
		styleFold.fontSize = 14;
		serializedObject.Update();		
		statusNoise = EditorGUILayout.Foldout(statusNoise, "Noise", styleFold);
		if (statusNoise)
		{
			EditorGUILayout.BeginVertical("Box");			
			EditorGUILayout.PropertyField(treeOct);
			EditorGUILayout.PropertyField(treeFre);					
			if (GUILayout.Button("Generate"))
			{
				treeNoiseTex = TextureGenerator.GetNoiseTex(sizeTex, sizeTex, oak.floatValue, treeOct.intValue, treeFre.floatValue);
			}
			GUILayout.Label(treeNoiseTex);
			EditorGUILayout.EndVertical();
		}
		statusPlants = EditorGUILayout.Foldout(statusPlants, "Trees Map", styleFold);
		if (statusPlants)
		{
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.PropertyField(oak);
			EditorGUILayout.PropertyField(pine);
			EditorGUILayout.PropertyField(cactus);
			EditorGUILayout.PropertyField(acacia);
			EditorGUILayout.PropertyField(rainforest);
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.PropertyField(serializedObject.FindProperty("textures"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("prefChunk"));
		serializedObject.ApplyModifiedProperties();		
	}
}
