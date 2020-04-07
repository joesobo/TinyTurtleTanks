using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalSpawner : MonoBehaviour
{
    public GameObject orbitalPrefab;
    public int minRadius = 50;
    public int maxRadius = 55;
    public int maxOrbitals = 10;

    private void Start()
    {
        //numberOfClouds = Random.Range(0, maxClouds);

        for (int i = 0; i < maxOrbitals; i++)
        {
            Instantiate(orbitalPrefab, RandomBetweenRadius3D(minRadius, maxRadius), this.transform.rotation, this.transform);
        }
    }

    Vector3 RandomBetweenRadius3D(float minRad, float maxRad)
    {
        float radius = Random.Range(minRad, maxRad);
        Vector3 point = Random.onUnitSphere * radius;
        return point;
    }
}
