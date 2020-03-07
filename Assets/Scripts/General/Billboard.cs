using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera camera = null;

    private void Start() {
        camera = FindObjectOfType<Camera>();
    }

    private void Update() {
        if(camera != null){
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.back,
                        camera.transform.rotation * Vector3.up);
        }
    }
}
