using System.Collections;
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

    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private bool delayOn = true;
    private bool hasExploded = false;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        rb = GetComponent<Rigidbody>();

        //add coroutine delay
        StartCoroutine("DelayCo");
    }

    private void Update() {
        transform.RotateAround(this.transform.parent.position, this.transform.right, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player" && !delayOn) {
            col.gameObject.GetComponent<Health>().DecreaseHealth(damage);
            ExplodeBullet();
        }
        if (col.gameObject.tag == "Shield" && !delayOn) {
            ExplodeBullet();
        }
        
        if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Planet") {
            ExplodeBullet();
        }
        else if (col.gameObject.tag == "Enemy") {
            col.gameObject.GetComponent<Health>().DecreaseHealth(damage);
            if (settings.useParticle) {
                Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
            }
            ExplodeBullet();
        }
        else if (col.gameObject.tag == "Breakable") {
            col.gameObject.GetComponent<Breakable>().Break();
            ExplodeBullet();
        }
        else if (col.gameObject.tag == "Fish") {
            if (settings.useParticle) {
                Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
            }
            ExplodeBullet();
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Boid") {
            if (settings.useParticle) {
                Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
            }

            BoidManager boidManager = FindObjectOfType<BoidManager>();
            boidManager.RemoveBoid(col.gameObject.GetComponentInChildren<Boid>());

            ExplodeBullet();
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Unbreakable") {
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
