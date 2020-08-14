﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltLaunch : MonoBehaviour {
    [HideInInspector]
    public int speed = 0;
    [HideInInspector]
    public int decaySpeed;
    [HideInInspector]
    public int damage;

    public int damageRadius = 5;
    public int knockbackRadius = 10;
    public int knockbackForce = 5000;
    public GameObject explosionParticlePrefab;
    public GameObject bloodParticlePrefab;

    private AltWeapon altWeapon;
    private GameSettings settings;
    private Rigidbody rb;
    private Explosion explosion;

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
        if (!explosion) {
            explosion = new Explosion(settings, explosionParticlePrefab, bloodParticlePrefab);
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

        explosion.playExplosion(transform.position, transform.rotation, this.transform);
        explosion.doDamage(damageRadius, knockbackRadius, knockbackForce, damage);
        //delete object
        StartCoroutine("DeleteObject");
    }

    IEnumerator DeleteObject() {
        yield return new WaitForSeconds(1f);
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
            explosion.playExplosion(transform.position, transform.rotation, this.transform);
            explosion.doDamage(damageRadius, knockbackRadius, knockbackForce, damage);
            //delete object
            StartCoroutine("DeleteObject");
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(0, 0.5f, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, damageRadius);

        Gizmos.color = new Color(1, 0.5f, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, knockbackRadius);
    }
}