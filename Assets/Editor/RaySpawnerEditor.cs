using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(RaySpawner))]
public class RaySpawnerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        RaySpawner raySpawner = target as RaySpawner;

        EditorGUILayout.LabelField("Object Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);
        EditorGUILayout.BeginVertical();
        EditorList.Show(serializedObject.FindProperty("prefabs"));
        raySpawner.parent = (Transform)EditorGUILayout.ObjectField("Parent", raySpawner.parent, typeof(Transform), true);
        raySpawner.num = EditorGUILayout.IntField("Number of Objects", raySpawner.num);
        raySpawner.offsetScale = EditorGUILayout.FloatField("Offset Scale", raySpawner.offsetScale);
        EditorGUIUtility.labelWidth = 75;
        EditorGUILayout.BeginHorizontal();
        raySpawner.minScale = EditorGUILayout.FloatField("Min Scale", raySpawner.minScale);
        GUILayout.Space(25);
        raySpawner.maxScale = EditorGUILayout.FloatField("Max Scale", raySpawner.maxScale);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        raySpawner.minHeight = EditorGUILayout.FloatField("Min Height", raySpawner.minHeight);
        GUILayout.Space(25);
        raySpawner.maxHeight = EditorGUILayout.FloatField("Max Height", raySpawner.maxHeight);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Layer Mask");
        LayerMask tempMask = EditorGUILayout.MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(raySpawner.layerMask), InternalEditorUtility.layers);
        raySpawner.layerMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(15);
        EditorGUILayout.LabelField("Ray Spawning Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);
        EditorGUILayout.BeginVertical();
        raySpawner.spawnTopDown = GUILayout.Toggle(raySpawner.spawnTopDown, "Use Top Down Spawning");
        EditorGUIUtility.labelWidth = 100;
        GUILayout.Space(5);
        if (raySpawner.spawnTopDown)
        {
            raySpawner.radius = EditorGUILayout.IntField(new GUIContent("Ray Start Radius", "Larger radiuses ensure better top down spawning"), raySpawner.radius);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            raySpawner.minRayHeight = EditorGUILayout.FloatField("Min Ray Height", raySpawner.minRayHeight);
            GUILayout.Space(25);
            raySpawner.maxRayHeight = EditorGUILayout.FloatField("Max Ray Height", raySpawner.maxRayHeight);
            EditorGUILayout.EndHorizontal();
        }

        raySpawner.checkDst = EditorGUILayout.IntField(new GUIContent("Check Distance", "Length ray should check"), raySpawner.checkDst);

        EditorGUIUtility.labelWidth = 75;
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(15);
        EditorGUILayout.LabelField("Overlap Detection Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);
        EditorGUILayout.BeginVertical();
        raySpawner.maxDensity = EditorGUILayout.FloatField("Max Density", raySpawner.maxDensity);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Object Mask");
        tempMask = EditorGUILayout.MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(raySpawner.objectMask), InternalEditorUtility.layers);
        raySpawner.objectMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(15);
        EditorGUILayout.LabelField("Extra Options", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);
        EditorGUILayout.BeginVertical();

        raySpawner.keepColliders = GUILayout.Toggle(raySpawner.keepColliders, "Use Colliders");
        raySpawner.useGizmos = GUILayout.Toggle(raySpawner.useGizmos, "Use Gizmos");
        raySpawner.useRandomRotation = GUILayout.Toggle(raySpawner.useRandomRotation, "Use Random Rotation");
        raySpawner.useRandomColor = GUILayout.Toggle(raySpawner.useRandomColor, "Use Random Color");

        GUILayout.Space(5);
        if (raySpawner.useRandomColor)
        {
            EditorGUILayout.BeginHorizontal();
            raySpawner.startColor = DrawColor(raySpawner.startColor, true);
            raySpawner.endColor = DrawColor(raySpawner.endColor, false);
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Regenerate"))
        {
            raySpawner.GenAll();
        }

        if (GUILayout.Button("Save Objects"))
        {

        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    private Color DrawColor(Color color, bool start)
    {
        if (start)
        {
            return EditorGUILayout.ColorField("Start Color", color);
        }
        else
        {
            return EditorGUILayout.ColorField("End Color", color);
        }
    }
}