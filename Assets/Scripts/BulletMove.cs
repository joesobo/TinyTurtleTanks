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
            //Destroy(col.gameObject);
            //TODO: add damage enemy
        }

        if (col.gameObject.tag == "Player")
        {
            //Destroy(col.gameObject);
            //TODO: add damage player
        }

        if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Ground")
        {
            Destroy(col.gameObject);
        }

        Destroy(gameObject);
    }
}
