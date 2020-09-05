using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    [HideInInspector]
    public float speed = 20f;
    [HideInInspector]
    public int damage = 1;
    [HideInInspector]
    public float decaySpeed = 20;
    [HideInInspector]
    public bool doesExplode = false;

    public int damageRadius = 5;
    public int knockbackRadius = 10;
    public int knockbackForce = 5000;
    public GameObject explosionParticlePrefab;
    public GameObject bloodParticlePrefab;

    private GameSettings settings;
    private Rigidbody rb;
    private AudioSource source;

    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private bool delayOn = true;
    private bool hasExploded = false;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        //add coroutine delay
        StartCoroutine("DelayCo");
    }

    private void Update() {
        if (!settings.isPaused) {
            transform.RotateAround(this.transform.parent.position, this.transform.right, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (!delayOn) {
            if (col.gameObject.tag == "Player") {
                Debug.Log("Hit Player");
                col.gameObject.GetComponent<Health>().DecreaseHealth(damage);
                ExplodeBullet();
            }
        }

        if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Planet") {
            Debug.Log("Hit Obstacle");
            if (settings.useSound) {
                source.volume = settings.soundVolume;
                source.Play();
            }
            ExplodeBullet();
        }

        else if (col.gameObject.tag == "Enemy") {
            Debug.Log("Hit Enemy");
            col.gameObject.GetComponent<Health>().DecreaseHealth(damage);
            if (settings.useParticle) {
                Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
            }

            if (settings.useSound) {
                source.volume = settings.soundVolume;
                source.Play();
            }
            ExplodeBullet();
        }

        else if (col.gameObject.tag == "Breakable") {
            Debug.Log("Hit Breakable");
            col.gameObject.GetComponent<Breakable>().Break();
            if (settings.useSound) {
                source.volume = settings.soundVolume;
                source.Play();
            }
            ExplodeBullet();
        }

        else if (col.gameObject.tag == "Fish") {
            Debug.Log("Hit Fish");
            if (settings.useParticle) {
                Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
            }

            if (settings.useSound) {
                source.volume = settings.soundVolume;
                source.Play();
            }

            Destroy(col.gameObject);
            ExplodeBullet();
        }

        else if (col.gameObject.tag == "Boid") {
            Debug.Log("Hit Boid");
            if (settings.useParticle) {
                Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
            }

            if (settings.useSound) {
                source.volume = settings.soundVolume;
                source.Play();
            }

            BoidManager boidManager = FindObjectOfType<BoidManager>();
            boidManager.RemoveBoid(col.gameObject.GetComponentInChildren<Boid>());

            Destroy(col.gameObject);
            ExplodeBullet();
        }
    }

    private void ExplodeBullet() {
        if (doesExplode && !hasExploded) {
            hasExploded = true;
            Explosion explosion = new Explosion(settings, explosionParticlePrefab, bloodParticlePrefab);
            explosion.PlayExplosion(transform.position, transform.rotation);
            explosion.DoDamage(damageRadius, knockbackRadius, knockbackForce, damage);
        }
        Destroy(gameObject);
    }

    IEnumerator DelayCo() {
        yield return new WaitForSeconds(0.1f);
        delayOn = false;
    }
}
