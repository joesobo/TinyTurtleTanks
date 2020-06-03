using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    public float speed = 20f;
    private GameSettings settings;
    private LevelRunner levelRunner;

    private bool delayOn = true;
    private AudioSource source;

    private MeshRenderer meshRenderer;
    public float decaySpeed = 20;

    public GameObject bloodParticlePrefab;

    private void Start()
    {
        levelRunner = FindObjectOfType<LevelRunner>();
        settings = FindObjectOfType<GameSettings>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();

        //add coroutine delay
        StartCoroutine("DelayCo");
    }

    private void Update()
    {
        if (!settings.isPaused)
        {
            transform.RotateAround(this.transform.parent.position, this.transform.right, speed * Time.deltaTime);

            meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, meshRenderer.material.color.a - (Time.deltaTime / decaySpeed));

            if (meshRenderer.material.color.a <= .15f)
            {
                BulletDeath();
            }
        }
    }

    void BulletDeath()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (!delayOn)
        {
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("Hit Player");
                col.gameObject.GetComponent<Health>().decreaseHealth(1);
                Destroy(gameObject);
            }
        }

        if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Planet")
        {
            Debug.Log("Hit Obstacle");
            if (settings.useSound)
            {
                source.volume = settings.soundVolume;
                source.Play();
            }
            Destroy(gameObject);
        }

        else if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            col.gameObject.GetComponent<Health>().decreaseHealth(1);
            if (settings.useParticle)
            {
                Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
            }

            if (settings.useSound)
            {
                source.volume = settings.soundVolume;
                source.Play();
            }
            Destroy(gameObject);
        }

        else if (col.gameObject.tag == "Breakable")
        {
            Debug.Log("Hit Breakable");
            col.gameObject.GetComponent<Breakable>().Break();
            if (settings.useSound)
            {
                source.volume = settings.soundVolume;
                source.Play();
            }
            Destroy(gameObject);
        }

        else if (col.gameObject.tag == "Fish")
        {
            Debug.Log("Hit Fish");
            if (settings.useParticle)
            {
                Instantiate(bloodParticlePrefab, col.transform.position, col.transform.rotation);
            }
            
            if (settings.useSound)
            {
                source.volume = settings.soundVolume;
                source.Play();
            }

            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator DelayCo()
    {
        yield return new WaitForSeconds(0.1f);
        delayOn = false;
    }
}
