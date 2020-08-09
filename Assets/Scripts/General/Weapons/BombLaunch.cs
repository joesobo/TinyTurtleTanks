using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLaunch : MonoBehaviour {
    public int speed;
    public int decaySpeed;
    public int damage;
    public GameObject explosionParticlePrefab;

    private GameSettings settings;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        settings = FindObjectOfType<GameSettings>();
    }

    public void launch() {
        if (!rb) {
            rb = GetComponent<Rigidbody>();
        }
        if (!settings) {
            settings = FindObjectOfType<GameSettings>();
        }
        rb.AddForce(transform.up * speed);
        rb.AddForce(transform.forward * speed);

        StartCoroutine("DelayExplosion");
    }

    IEnumerator DelayExplosion() {
        yield return new WaitForSeconds(decaySpeed);
        //turn off renderer
        GetComponent<MeshRenderer>().enabled = false;
        //play explosion
        if (settings.useParticle) {
            Instantiate(explosionParticlePrefab, transform.position, transform.rotation, this.transform);
        }
        //check radius for objects to damage
        //delete object
        StartCoroutine("DeleteObject");
    }

    IEnumerator DeleteObject() {
        yield return new WaitForSeconds(0.5f);
        Object.Destroy(this);
    }
}
