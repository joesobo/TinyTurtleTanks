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

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!settings.isPaused){
            transform.RotateAround(this.transform.parent.position, this.transform.right, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Ground")
        {
            Debug.Log("Hit Obstacle");
            Destroy(gameObject);
        }

        else if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            col.gameObject.GetComponent<Health>().decreaseHealth(1);
            Destroy(gameObject);
        }

        else if (col.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            col.gameObject.GetComponent<Health>().decreaseHealth(1);
            Destroy(gameObject);
        }

        else if(col.gameObject.tag == "Breakable"){
            Debug.Log("Hit Breakable");
            col.gameObject.GetComponent<Breakable>().Break();
            Destroy(gameObject);
        }
    }
}
