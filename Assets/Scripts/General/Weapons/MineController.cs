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
        foreach (Collider col in hitColliders) {
            if (col.tag == "Player" || col.tag == "Enemy") {
                return col;
            }
        }
        return null;
    }

    private void explode(Collider col) {
        if (col) {
            //play explosion
            if (settings.useParticle) {
                Instantiate(explosionParticlePrefab, transform.position, transform.rotation, this.transform);
            }
            //apply damage
            col.GetComponent<Health>().decreaseHealth(damage);
            //delete object
            StartCoroutine("DeleteObject");
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
