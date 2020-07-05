using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawner : MonoBehaviour {
    public GameObject prefab;
    public MeshFilter meshFilter;
    public Transform parent;

    public int num;
    public float scale = 13;

    private Mesh mesh;

    private void Start() {
        mesh = meshFilter.sharedMesh;

        for (int i = 0; i < num; i++) {
            GenerateObject();
        }
    }

    private void GenerateObject() {
        int rndTriStart = Random.Range(0, mesh.vertices.Length / 3) * 3; //pick a triangle
        Vector3 position = mesh.vertices[rndTriStart + 0];
        Vector3 normal = mesh.normals[rndTriStart + 0];

        Instantiate(prefab, position * scale, Quaternion.FromToRotation(Vector3.up, normal), parent);
    }
}
