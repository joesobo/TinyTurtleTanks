using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableSpawner : MonoBehaviour {
    public GameObject breakablePrefab;
    public int radius = 50;
    public int waitForSeconds = 5;
    public int maxTotalCrates = 20;
    [HideInInspector]
    public int totalCrates = 0;
    private Random rand = new Random();
    private GameSettings settings;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        StartCoroutine("StartSpawn");
    }

    IEnumerator StartSpawn() {
        while (true) {
            if (!settings.isPaused) {
                SpawnAtPoint();
            }
            yield return new WaitForSeconds(waitForSeconds);
        }
    }

    private void SpawnAtPoint() {
        if (totalCrates < maxTotalCrates) {
            Instantiate(breakablePrefab, RandomPosition(radius), this.transform.rotation, this.transform);
            totalCrates++;
        }
    }

    private Vector3 RandomPosition(int r) {
        return Random.onUnitSphere * r;
    }
}
