using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalSpawner : MonoBehaviour {
    public GameObject orbitalPrefab;
    public int minRadius = 50;
    public int maxRadius = 55;
    public int numOfOrbitals = 10;
    public float minScale = 0.5f;
    public float maxScale = 2;

    private void Start() {
        for (int i = 0; i < numOfOrbitals; i++) {
            GameObject orbital = Instantiate(orbitalPrefab, RandomBetweenRadius3D(minRadius, maxRadius), this.transform.rotation, this.transform);
            orbital.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
        }
    }

    Vector3 RandomBetweenRadius3D(float minRad, float maxRad) {
        float radius = Random.Range(minRad, maxRad);
        Vector3 point = Random.onUnitSphere * radius;
        return point;
    }
}
