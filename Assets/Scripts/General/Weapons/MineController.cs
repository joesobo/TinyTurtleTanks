using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour {
    //public int speed;
    public int decaySpeed;
    public int damage;
    public int damageRadius = 5;
    public int knockbackRadius = 10;
    public int knockbackForce = 5000;
    public GameObject explosionParticlePrefab;
    public GameObject bloodParticlePrefab;

    private GameSettings settings;

    private Collider col;

    private bool checkExplosions = false;

    public void launch() {
        if (!settings) {
            settings = FindObjectOfType<GameSettings>();

            StartCoroutine("StartExplosive");
        }
    }

    void Update() {
        col = checkNearby();
        if (col && checkExplosions) {
            explode(col);
            checkExplosions = false;
        }
    }

    private Collider checkNearby() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider collider in hitColliders) {
            if (collider.tag == "Player" || collider.tag == "Enemy") {
                return collider;
            }
        }
        return null;
    }

    private void explode(Collider collider) {
        if (collider) {
            //play explosion
            if (settings.useParticle) {
                Instantiate(explosionParticlePrefab, transform.position, transform.rotation, this.transform);
            }
            //apply damage
            collider.GetComponent<Health>().decreaseHealth(damage);
            //check radius for object to knockback
            knockbackObjectsInRadius();
            //delete object
            StartCoroutine("DeleteObject");
        }
    }

    private void knockbackObjectsInRadius() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, knockbackRadius);
        foreach (Collider collider in hitColliders) {
            if (collider.tag == "Player" || collider.tag == "Enemy") {
                Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
                rb.AddExplosionForce(knockbackForce, transform.position, knockbackRadius);

                if (settings.useParticle) {
                    Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
                }
            }
        }
    }

    IEnumerator StartExplosive() {
        yield return new WaitForSeconds(1);
        checkExplosions = true;
    }

    IEnumerator DeleteObject() {
        yield return new WaitForSeconds(0.5f);
        Object.Destroy(this.gameObject);
    }
}
