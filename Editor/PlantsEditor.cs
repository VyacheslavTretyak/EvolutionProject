using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Plants))]
public class PlantsEditor : Editor
{
    bool statusNoise = false;
    bool statusPlants = false;
	Texture2D noiseTex;
	SerializedProperty plantOct;
	SerializedProperty plantFre;
	SerializedProperty grass;
	int sizeTex = 256;
	private void OnEnable()
	{
		plantOct = serializedObject.FindProperty("plantOctaves");
		plantFre = serializedObject.FindProperty("plantFrequency");
		grass = serializedObject.FindProperty("grass");
		noiseTex = new Texture2D(sizeTex, sizeTex);
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
            EditorGUILayout.PropertyField(plantOct);
			EditorGUILayout.PropertyField(plantFre);
			if (GUILayout.Button("Generate"))
			{
				noiseTex = TextureGenerator.GetNoiseTex(sizeTex, sizeTex, grass.floatValue, plantOct.intValue, plantFre.floatValue);
			}
			GUILayout.Label(noiseTex);
			EditorGUILayout.EndVertical();
        }

        statusPlants = EditorGUILayout.Foldout(statusPlants, "Plant Map", styleFold);
        if (statusPlants)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(grass);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("savanna"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("swamp"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("desert"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("cold"));            
            EditorGUILayout.EndVertical();           
        }        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("textures"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("prefChunk"));
        serializedObject.ApplyModifiedProperties();
    } 
}
