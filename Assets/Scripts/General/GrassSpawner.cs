using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    private Mesh grassMesh;
    private Material grassMat;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private CombineInstance[] combine;

    public int numberOfGrassObjects = 30;
    public GameObject grassPrefab;
    private int spawnRadius = 50;
    public LayerMask mask;

    private Vector3 spawnPoint;

    private void Start() {
        grassMat = grassPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterial;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        combine = new CombineInstance[numberOfGrassObjects];

        for (int i = 0; i < numberOfGrassObjects; i++)
        {
            spawnPoint = FindSpawnPoint();
            if(spawnPoint != Vector3.zero){
                GameObject g = Instantiate(grassPrefab, spawnPoint, Quaternion.LookRotation(-spawnPoint), this.transform);
                g.transform.Rotate(new Vector3(-90,0,0));
                g.transform.localScale = new Vector3(1,Random.Range(1,5),1);
                combine[i].mesh = g.GetComponentInChildren<MeshFilter>().sharedMesh;
                combine[i].transform = g.transform.localToWorldMatrix;
                g.SetActive(false);
            }
        }

        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);
        meshRenderer.material = grassMat;
    }

    private Vector3 FindSpawnPoint()
    {
        Vector3 startPoint = Random.onUnitSphere * spawnRadius;

        RaycastHit hit;

        if(Physics.Raycast(startPoint, -startPoint, out hit, Mathf.Infinity, mask)){
            return hit.point;
        }

        return Vector3.zero;
    }
}
