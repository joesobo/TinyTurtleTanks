using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public int numberOfGrassObjects = 30;
    public GameObject grassPrefab;
    private int spawnRadius = 50;
    public LayerMask mask;

    private Vector3 spawnPoint;

    private void Start() {
        for (int i = 0; i < numberOfGrassObjects; i++)
        {
            spawnPoint = FindSpawnPoint();
            if(spawnPoint != Vector3.zero){
                GameObject g = Instantiate(grassPrefab, spawnPoint, Quaternion.LookRotation(-spawnPoint), this.transform);
                g.transform.Rotate(new Vector3(-90,0,0));
                g.transform.localScale = new Vector3(1,Random.Range(1,5),1);
            }
        }
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
