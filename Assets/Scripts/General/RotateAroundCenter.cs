using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundCenter : MonoBehaviour {
    public float orbitSpeed = 5;
    public bool useWind;
    public Vector3 rotateDir;

    private void Start() {
        float x = Random.Range(0, 1);
        float y = Random.Range(0, Mathf.Max(1 - x));
        float z = Random.Range(0, Mathf.Max(0, 1 - y - x));
        rotateDir = new Vector3(x, y, z);
    }

    private void FixedUpdate() {
        if (useWind) {
            transform.RotateAround(this.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
        else {
            transform.RotateAround(this.transform.position, rotateDir, orbitSpeed * Time.deltaTime);
        }
    }
}
