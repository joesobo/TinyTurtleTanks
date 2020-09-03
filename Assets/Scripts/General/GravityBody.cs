using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {
    GravityAttractor planet;

    Rigidbody rb;
    public bool useGrav = true;

    private GameSettings settings;

    private void Awake() {
        settings = FindObjectOfType<GameSettings>();
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();

        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate() {
        //if (!settings.isPaused) {
            planet.Attract(this);
        //}
    }

    public Rigidbody GetRb() {
        return rb;
    }
}
