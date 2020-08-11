using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltLaunch : MonoBehaviour {
    [HideInInspector]
    public int speed = 0;
    [HideInInspector]
    public int decaySpeed;
    [HideInInspector]
    public int damage;

    [HideInInspector]
    public int damageRadius = 5;
    public int knockbackRadius = 10;
    public int knockbackForce = 5000;
    public GameObject explosionParticlePrefab;
    public GameObject bloodParticlePrefab;

    private AltWeapon altWeapon;
    private GameSettings settings;
    private Rigidbody rb;

    private Collider col;
    private bool checkToExplode = false;

    

    private void Start() {
        rb = GetComponent<Rigidbody>();
        settings = FindObjectOfType<GameSettings>();
    }

    private void Update() {
        col = checkNearby();
        if (col && checkToExplode) {
            explode(col);
            checkToExplode = false;
        }
    }

    public void launch(AltWeapon altWeap, AltWeapon.BulletType type) {
        altWeapon = altWeap;

        if (!rb) {
            rb = GetComponent<Rigidbody>();
        }
        if (!settings) {
            settings = FindObjectOfType<GameSettings>();
        }

        if (type == AltWeapon.BulletType.Bomb) {
            rb.AddForce(transform.up * speed);
            rb.AddForce(transform.forward * speed);

            StartCoroutine("BombCountdownExplosion");
        }
        else {
            StartCoroutine("DelayedMineExplosion");
        }
    }

    IEnumerator DelayedMineExplosion() {
        yield return new WaitForSeconds(1);
        checkToExplode = true;
    }

    IEnumerator BombCountdownExplosion() {
        yield return new WaitForSeconds(decaySpeed);
        //turn off renderer
        GetComponent<MeshRenderer>().enabled = false;
        //play explosion
        if (settings.useParticle) {
            Instantiate(explosionParticlePrefab, transform.position, transform.rotation, this.transform);
        }
        //check radius for objects to damage
        damageObjectsInRadius();
        //check radius for object to knockback
        knockbackObjectsInRadius();
        //delete object
        StartCoroutine("DeleteObject");
    }

    IEnumerator DeleteObject() {
        yield return new WaitForSeconds(0.5f);
        Object.Destroy(this.gameObject);
        altWeapon.inPlay--;
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
            damageCollider();
            //check radius for object to knockback
            knockbackObjectsInRadius();
            //delete object
            StartCoroutine("DeleteObject");
        }
    }

    private void damageObjectsInRadius() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider col in hitColliders) {
            if (col.tag == "Player" || col.tag == "Enemy") {
                damageCollider();
            }
        }
    }

    private void knockbackObjectsInRadius() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, knockbackRadius);
        foreach (Collider col in hitColliders) {
            if (col.tag == "Player" || col.tag == "Enemy") {
                Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
                rb.AddExplosionForce(knockbackForce, transform.position, knockbackRadius);
            }
        }
    }

    private void damageCollider() {
        col.gameObject.GetComponent<Health>().decreaseHealth(damage);

        if (settings.useParticle) {
            Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(0, 0.5f, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, damageRadius);

        Gizmos.color = new Color(1, 0.5f, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, knockbackRadius);
    }
}
