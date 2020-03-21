using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootprints : MonoBehaviour
{
    public Footprints footprints;

    public float footprintSpacing = 2;

    private Vector3 lastPos = Vector3.zero;

    private void Start() {
        lastPos = transform.position;

        if(!footprints){
            footprints = GameObject.Find("Footprints").GetComponent<Footprints>();
        }
    }

    private void Update() {
        float distFromLastFootprint = (lastPos - transform.position).sqrMagnitude;

        if(distFromLastFootprint > footprintSpacing * footprintSpacing){
            footprints.AddFootprint(transform.position, transform.forward, transform.right, Random.Range(0,4));

            lastPos = transform.position;
        }
    }
}
