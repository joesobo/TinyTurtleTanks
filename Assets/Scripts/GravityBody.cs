using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour
{
    GravityAttractor planet;

    Rigidbody rb;
    public bool useGrav = true;

    private void Awake() {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();

        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate() {
        planet.Attract(this);
    }

    public Rigidbody getRb(){
        return rb;
    }
}
