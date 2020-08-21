using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    private GameSettings settings;
    private GameObject explosionParticles;
    private GameObject bloodParticlePrefab;
    private Vector3 position;

    public Explosion(GameSettings settings, GameObject explosionParticles, GameObject bloodParticles) {
        this.settings = settings;
        this.explosionParticles = explosionParticles;
        this.bloodParticlePrefab = bloodParticles;
    }

    public void PlayExplosion(Vector3 position, Quaternion rotation, Transform parent) {
        this.position = position;

        //play explosion
        if (settings.useParticle) {
            Instantiate(explosionParticles, position, rotation, parent);
        }
    }

    public void DoDamage(float damageRadius, float knockbackRadius, float knockbackForce, int damage) {
        //check radius for objects to damage
        DamageObjectsInRadius(damageRadius, damage);
        //check radius for object to knockback
        KnockbackObjectsInRadius(knockbackRadius, knockbackForce);
    }

    private void DamageObjectsInRadius(float damageRadius, int damage) {
        Collider[] hitColliders = Physics.OverlapSphere(position, damageRadius);
        foreach (Collider col in hitColliders) {
            if (col.tag == "Player") {
                PlayerEffects playerEffects = FindObjectOfType<PlayerEffects>();
                if (!playerEffects.shield) {
                    DamageCollider(col, damage);
                }
            }

            else if (col.tag == "Enemy") {
                DamageCollider(col, damage);
            }

            else if (col.gameObject.tag == "Obstacle") {
                Destroy(col.gameObject);
            }

            else if (col.gameObject.tag == "Breakable") {
                col.gameObject.GetComponent<Breakable>().Break();
            }
        }
    }

    private void KnockbackObjectsInRadius(float knockbackRadius, float knockbackForce) {
        Collider[] hitColliders = Physics.OverlapSphere(position, knockbackRadius);
        foreach (Collider col in hitColliders) {
            if (col.tag == "Player" || col.tag == "Enemy") {
                Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
                rb.AddExplosionForce(knockbackForce, position, knockbackRadius);
            }
        }
    }

    private void DamageCollider(Collider col, int damage) {
        col.gameObject.GetComponent<Health>().DecreaseHealth(damage);

        if (settings.useParticle) {
            Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
        }
    }
}