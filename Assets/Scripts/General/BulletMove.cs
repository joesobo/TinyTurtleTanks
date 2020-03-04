using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    public float speed = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.RotateAround(this.transform.parent.position, this.transform.right, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            col.gameObject.GetComponent<Health>().decreaseHealth(1);
        }

        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            col.gameObject.GetComponent<Health>().decreaseHealth(1);
        }

        if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Ground")
        {
            Debug.Log("Hit Obstacle");
        }

        if(col.gameObject.tag == "Breakable"){
            Debug.Log("Hit Breakable");
            //TODO: Spawn new trap or pickup
        }

        Debug.Log(col);
        Destroy(gameObject);
    }
}
