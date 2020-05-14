using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(RaySpawner))]
public class RaySpawnerEditor : Editor
{
    bool showObjectSettings = false;
    bool showSpawnSettings = false;
    bool showOverlapSettings = false;
    bool showOtherSettings = false;

    override public void OnInspectorGUI()
    {
        RaySpawner raySpawner = target as RaySpawner;

        #region ObjectSettings
        showObjectSettings = EditorGUILayout.Foldout(showObjectSettings, "Object Settings");
        if (showObjectSettings)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical();
            EditorList.Show(serializedObject.FindProperty("prefabs"));
            raySpawner.parent = (Transform)EditorGUILayout.ObjectField("Parent", raySpawner.parent, typeof(Transform), true);
            raySpawner.num = EditorGUILayout.IntField("Number of Objects", raySpawner.num);
            raySpawner.offsetScale = EditorGUILayout.FloatField("Offset Scale", raySpawner.offsetScale);
            EditorGUIUtility.labelWidth = 75;
            EditorGUILayout.BeginHorizontal();
            raySpawner.minScale = EditorGUILayout.FloatField("Min Scale:", raySpawner.minScale);
            GUILayout.Space(25);
            raySpawner.maxScale = EditorGUILayout.FloatField("Max Scale:", raySpawner.maxScale);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.MinMaxSlider(ref raySpawner.minScale, ref raySpawner.maxScale, 0, 5);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            raySpawner.minHeight = EditorGUILayout.FloatField("Min Height", raySpawner.minHeight);
            GUILayout.Space(25);
            raySpawner.maxHeight = EditorGUILayout.FloatField("Max Height", raySpawner.maxHeight);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.MinMaxSlider(ref raySpawner.minHeight, ref raySpawner.maxHeight, 0, 50);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Layer Mask");
            LayerMask tempMask = EditorGUILayout.MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(raySpawner.layerMask), InternalEditorUtility.layers);
            raySpawner.layerMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
            EditorGUILayout.EndHorizontal();

            raySpawner.useGizmoHeight = GUILayout.Toggle(raySpawner.useGizmoHeight, "Use Height Gizmos");
            if (raySpawner.useGizmoHeight)
            {
                raySpawner.useGizmoRadius = false;
                raySpawner.useGizmoRayHeight = false;
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(15);
        }
        #endregion

        #region SpawnSettings
        showSpawnSettings = EditorGUILayout.Foldout(showSpawnSettings, "Spawn Settings");
        if (showSpawnSettings)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical();
            raySpawner.spawnTopDown = GUILayout.Toggle(raySpawner.spawnTopDown, "Use Top Down Spawning");
            EditorGUIUtility.labelWidth = 100;
            GUILayout.Space(5);

            raySpawner.checkDst = EditorGUILayout.IntField(new GUIContent("Check Distance", "Length ray should check"), raySpawner.checkDst);
            raySpawner.slopeCutoff = EditorGUILayout.FloatField("Slope Cutoff", raySpawner.slopeCutoff);

            if (raySpawner.spawnTopDown)
            {
                raySpawner.radius = EditorGUILayout.IntField(new GUIContent("Ray Start Radius", "Larger radiuses ensure better top down spawning"), raySpawner.radius);

                raySpawner.useGizmoRadius = GUILayout.Toggle(raySpawner.useGizmoRadius, "Use Radius Gizmos");
                if (raySpawner.useGizmoRadius)
                {
                    raySpawner.useGizmoRayHeight = false;
                    raySpawner.useGizmoHeight = false;
                }
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                raySpawner.minRayHeight = EditorGUILayout.FloatField("Min Ray Height", raySpawner.minRayHeight);
                GUILayout.Space(25);
                raySpawner.maxRayHeight = EditorGUILayout.FloatField("Max Ray Height", raySpawner.maxRayHeight);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.MinMaxSlider(ref raySpawner.minRayHeight, ref raySpawner.maxRayHeight, 0, 50);
                EditorGUILayout.EndHorizontal();

                raySpawner.useGizmoRayHeight = GUILayout.Toggle(raySpawner.useGizmoRayHeight, "Use Ray Height Gizmos");
                if (raySpawner.useGizmoRayHeight)
                {
                    raySpawner.useGizmoRadius = false;
                    raySpawner.useGizmoHeight = false;
                }
            }
            EditorGUIUtility.labelWidth = 75;
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(15);
        }
        #endregion

        #region OverlapSettings
        showOverlapSettings = EditorGUILayout.Foldout(showOverlapSettings, "Overlap Detection Settings");
        if (showOverlapSettings)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical();
            raySpawner.maxDensity = EditorGUILayout.FloatField("Max Density", raySpawner.maxDensity);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Object Mask");
            LayerMask tempMask = EditorGUILayout.MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(raySpawner.objectMask), InternalEditorUtility.layers);
            raySpawner.objectMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(15);
        }
        #endregion

        #region OtherSettings
        showOtherSettings = EditorGUILayout.Foldout(showOtherSettings, "Other Settings");
        if (showOtherSettings)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical();

            raySpawner.keepColliders = GUILayout.Toggle(raySpawner.keepColliders, "Use Colliders");
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
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);
        EditorGUILayout.BeginVertical();
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Regenerate"))
            {
                raySpawner.ClearObjects();
                raySpawner.GenAll();
            }

            if (GUILayout.Button("Save Objects"))
            {
                raySpawner.Save();
            }
        }

        if (GUILayout.Button("Load Objects"))
        {
            raySpawner.ClearObjects();
            raySpawner.Load();
        }

        if (GUILayout.Button("Clear Objects"))
        {
            raySpawner.ClearObjects();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        #endregion
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