using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableSpawner : MonoBehaviour
{
    public GameObject breakablePrefab;
    public int radius = 50;
    public int waitForSeconds = 5;
    private Random rand = new Random();

    private void Start() {
        StartCoroutine("StartSpawn");
    }

    IEnumerator StartSpawn()
    {
        while (true)
        {
            SpawnAtPoint();
            yield return new WaitForSeconds(waitForSeconds);
        }
    }

    private void SpawnAtPoint(){
        Instantiate(breakablePrefab, randPos(radius), this.transform.rotation, this.transform);
    }

    private Vector3 randPos(int r){
        return Random.onUnitSphere * r;
    }
}
