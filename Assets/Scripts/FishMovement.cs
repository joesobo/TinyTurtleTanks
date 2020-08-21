using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour {
    public LayerMask objectLayerMask;
    public GameObject raycastPoint;
    private Vector3 raycastOrigin;

    private float curSpeed = 0;
    public float speed = 7;
    public float rotateSpeed = 100;
    private float curRotate = 0;

    private bool lockRotation = true;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private Rigidbody rb;
    private GravityBody gravityBody;
    private GameSettings settings;
    private float moveSeconds;
    private float rotateSeconds;

    private float waterRadius = 26.5f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        gravityBody = GetComponent<GravityBody>();
        settings = FindObjectOfType<GameSettings>();

        raycastOrigin = raycastPoint.GetComponent<Transform>().localPosition;

        StartCoroutine("StartRotate");
    }

    private void Update() {
        if (!settings.isPaused) {
            CheckOutOfWater();

            CalculateMove();
            CalculateRotate();
        }
    }

    private void FixedUpdate() {
        if (!settings.isPaused) {
            //move
            Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime;
            rb.MovePosition(rb.position + localMove);

            //rotate
            transform.Rotate(0, 1 * curRotate * Time.deltaTime, 0);
        }
    }

    private void CalculateRotate() {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin + transform.position, transform.TransformDirection(Vector3.forward), out hit, 2, objectLayerMask)) {
            Debug.DrawRay(raycastOrigin + transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            curRotate = rotateSpeed;
        }
        else {
            Debug.DrawRay(raycastOrigin + transform.position, transform.TransformDirection(Vector3.forward) * 2, Color.white);
            if (lockRotation) {
                curRotate = 0;
            }
        }
    }

    private void CalculateMove() {
        Vector3 targetMoveAmount = Vector3.forward * curSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
    }

    private void CheckOutOfWater() {
        if (!IsInsideRadius(transform.position)) {
            gravityBody.useGrav = true;
        }
        else {
            gravityBody.useGrav = false;
        }
    }

    bool IsInsideRadius(Vector3 point) {
        return Mathf.Pow(point.x, 2) + Mathf.Pow(point.y, 2) + Mathf.Pow(point.z, 2) < Mathf.Pow(waterRadius, 2);
    }

    IEnumerator StartRotate() {
        curRotate = rotateSpeed;
        lockRotation = false;
        yield return new WaitForSeconds(rotateSeconds);
        lockRotation = true;
        curRotate = 0;

        rotateSeconds = Random.Range(1, 5);
        StartCoroutine("StartMove");
    }

    IEnumerator StartMove() {
        curSpeed = speed;
        yield return new WaitForSeconds(moveSeconds);
        curSpeed = 0;

        moveSeconds = Random.Range(1, 7);
        StartCoroutine("StartRotate");
    }
}
