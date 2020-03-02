using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundCenter : MonoBehaviour
{
    public float orbitSpeed = 5;

    private void FixedUpdate() {
         transform.RotateAround(Vector3.zero, Vector3.forward, orbitSpeed * Time.deltaTime);
    }
}
