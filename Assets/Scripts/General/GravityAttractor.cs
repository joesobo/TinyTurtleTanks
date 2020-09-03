using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour {
    public float gravity = -18;
    
    public void Attract(GravityBody body) {
        Vector3 targetDir = (body.transform.position - transform.position).normalized;
        Vector3 bodyUp = body.transform.up;

        body.transform.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.transform.rotation;
        if (body.useGrav) {
            body.GetRb().AddForce(targetDir * gravity);
        }
    }
}
