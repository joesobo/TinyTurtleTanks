using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour {
    public enum GizmoType { Never, SelectedOnly, Always }

    public GameObject prefab;
    public int minSpawnRadius;
    public int maxSpawnRadius;
    public Vector3 center = Vector3.zero;
    public int spawnCount;
    public Color color1;
    public Color color2;

    private float minSize;
    private float maxSize;
    private int adultTime;
    private int deathTime;
    private int intervalSpawnTime;

    public GizmoType showSpawnRegion;

    private BoidSettings settings;

    public void StartSpawner(BoidSettings settings) {
        this.settings = settings;

        for (int i = 0; i < spawnCount; i++) {
            float randomSpawnRange = Random.Range(minSpawnRadius, maxSpawnRadius);
            CreateBoid((UnityEngine.Random.insideUnitSphere * (randomSpawnRange / 2)) + center);
        }
    }

    public Boid CreateBoid(Vector3 pos) {
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);

        go.transform.localScale = Vector3.one;
        go.transform.parent = this.transform;

        Boid boid = go.GetComponentInChildren<Boid>();

        boid.SetColor(RandomColor(color1, color2));

        return boid;
    }

    private Color RandomColor(Color c1, Color c2) {
        Color newColor = new Color(
            (float)Random.Range(c1.r, c2.r),
            (float)Random.Range(c1.g, c2.g),
            (float)Random.Range(c1.b, c2.b)
        );

        return newColor;
    }

    private void OnDrawGizmos() {
        if (showSpawnRegion == GizmoType.Always) {
            DrawGizmos();
        }
    }

    private void OnDrawGizmosSelected() {
        if (showSpawnRegion == GizmoType.SelectedOnly) {
            DrawGizmos();
        }
    }

    private void DrawGizmos() {
        Gizmos.color = new Color(1, 0.5f, 0, 0.3f);
        Gizmos.DrawSphere(transform.position + center, minSpawnRadius / 2);
        Gizmos.color = new Color(1, 0, 0.5f, 0.3f);
        Gizmos.DrawSphere(transform.position + center, maxSpawnRadius / 2);
    }
}
