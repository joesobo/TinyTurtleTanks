using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RaySpawner))]
public class RaySpawnerEditor : Editor {

    override public void OnInspectorGUI(){
        RaySpawner raySpawner = target as RaySpawner;

        raySpawner.useRandomColor = GUILayout.Toggle(raySpawner.useRandomColor, "Use Random Color");

        if(raySpawner.useRandomColor){
            raySpawner.startColor = EditorGUILayout.ColorField("Start Color", raySpawner.startColor);
            raySpawner.endColor = EditorGUILayout.ColorField("End Color", raySpawner.startColor);
        }
    }
}