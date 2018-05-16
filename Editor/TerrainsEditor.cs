using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Terrains))]
public class TerrainsEditor : Editor
{
    bool statusHeight = false;
    bool statusHeightMap = false;
    bool statusHeat = false;
    bool statusHeatMap = false;
    bool statusMoinsture= false;
    bool statusMoinstureMap = false;	

	
	public override void OnInspectorGUI()
    {   

        GUIStyle styleFold = EditorStyles.foldout;
        styleFold.fontStyle = FontStyle.Bold;
        styleFold.fontSize = 14;
		Terrains ter = (Terrains)target;
        serializedObject.Update();
        statusHeight = EditorGUILayout.Foldout(statusHeight, "Height Noise", styleFold);
        if (statusHeight)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heightOctaves"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heightFrequency"));           
			EditorGUILayout.EndVertical();
        }

        statusHeightMap = EditorGUILayout.Foldout(statusHeightMap, "Height Map", styleFold);
        if (statusHeightMap)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mountValue"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("deepValue"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heightDeep"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heightWater"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heightBeach"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heightGrass"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heightForest"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heightRock"));
            EditorGUILayout.EndVertical();           
        }
        statusHeat = EditorGUILayout.Foldout(statusHeat, "Heat Noise", styleFold);
        if (statusHeat)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heatOctaves"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("heatFrequency"));            
            EditorGUILayout.EndVertical();
        }
        statusHeatMap = EditorGUILayout.Foldout(statusHeatMap, "Heat Map", styleFold);
        if (statusHeatMap)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("coldest"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("cold"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("warm"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("warmest"));
            EditorGUILayout.EndVertical();
        }
        statusMoinsture = EditorGUILayout.Foldout(statusMoinsture, "Moinsture Noise", styleFold);
        if (statusMoinsture)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("moinstureOctaves"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("moinstureFrequency"));            
            EditorGUILayout.EndVertical();
        }
        statusMoinstureMap = EditorGUILayout.Foldout(statusMoinstureMap, "Moinsture Map", styleFold);
        if (statusMoinstureMap)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dryest"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dry"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("wet"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("wetter"));
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("textures"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("prefChunk"));
        serializedObject.ApplyModifiedProperties();
    } 
}
