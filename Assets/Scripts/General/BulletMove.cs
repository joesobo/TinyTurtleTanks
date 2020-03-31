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

    private void Start()
    {
        levelRunner = FindObjectOfType<LevelRunner>();
        settings = FindObjectOfType<GameSettings>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        //add coroutine delay
        StartCoroutine("DelayCo");
    }

    private void Update()
    {
        if (!settings.isPaused)
        {
            transform.RotateAround(this.transform.parent.position, this.transform.right, speed * Time.deltaTime);
        }
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
            if(settings.useSound){
                source.Play();
            }
            Destroy(gameObject);
        }

        else if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            col.gameObject.GetComponent<Health>().decreaseHealth(1);
            if(settings.useSound){
                source.Play();
            }
            Destroy(gameObject);
        }

        else if (col.gameObject.tag == "Breakable")
        {
            Debug.Log("Hit Breakable");
            col.gameObject.GetComponent<Breakable>().Break();
            if(settings.useSound){
                source.Play();
            }
            Destroy(gameObject);
        }
    }

    IEnumerator DelayCo()
    {
        yield return new WaitForSeconds(0.1f);
        delayOn = false;
    }
}
