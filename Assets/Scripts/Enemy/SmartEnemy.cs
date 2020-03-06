using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    private float moveSeconds;
    private float rotateSeconds;

    private float curSpeed = 0;
    public float speed = 7;
    private float curRotate = 0;
    public float rotateSpeed = 100;

    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private Rigidbody rb;

    public LayerMask objectLayerMask;
    private Vector3 offsetPosition;
    private bool lockRotation = true;

    public float playerLookRadius = 5;
    public float playerShootRadius = 2;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        
        randomTimes();
        StartCoroutine("StartRotate");
    }

    private void Update() {
        //calculate move if speed
        Vector3 moveDir = new Vector3(0, 0, 1);
        Vector3 targetMoveAmount = moveDir * curSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        offsetPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(offsetPosition, transform.TransformDirection(Vector3.forward), out hit, 2, objectLayerMask))
        {
            Debug.DrawRay(offsetPosition, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            curRotate = rotateSpeed;
        }
        else
        {
            Debug.DrawRay(offsetPosition, transform.TransformDirection(Vector3.forward) * 2, Color.white);
            if(lockRotation){
                curRotate = 0;
            }
        }
    }

    private void FixedUpdate() {
        //move
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime;
        rb.MovePosition(rb.position + localMove);

        //rotate
        transform.Rotate(0, 1 * curRotate * Time.deltaTime, 0);
    }

    IEnumerator StartRotate(){
        curRotate = rotateSpeed;
        lockRotation = false;
        yield return new WaitForSeconds(rotateSeconds);
        lockRotation = true;
        curRotate = 0;
        
        randomTimes();
        StartCoroutine("StartMove");
    }

    IEnumerator StartMove()
    {
        curSpeed = speed;
        yield return new WaitForSeconds(moveSeconds);
        curSpeed = 0;

        randomTimes();
        StartCoroutine("StartRotate");
    }

    private void randomTimes(){
        moveSeconds = Random.Range(1,7);
        rotateSeconds = Random.Range(1,5);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color32(5, 170, 250, 100);
        Gizmos.DrawSphere(transform.position, playerLookRadius);
        Gizmos.color = new Color32(250, 50, 10, 100);
        Gizmos.DrawSphere(transform.position, playerShootRadius);
    }
}
