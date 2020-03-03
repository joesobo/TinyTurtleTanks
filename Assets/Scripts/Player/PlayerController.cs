using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float rotateSpeed = 100;
    public float jumpForce = 200;
    public LayerMask groundMask;

    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private Rigidbody rb;
    private bool grounded;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        //calculate movement
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(0, 0, inputY).normalized;
        Vector3 targetMoveAmount = moveDir * speed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        
        //calculate rotation
        float inputX = Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, inputX * rotateSpeed * Time.deltaTime, 0);

        //grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, .75f + .1f, groundMask)){
            grounded = true;
        }else{
            grounded = false;
        }
    }

    private void FixedUpdate() {
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime;
        rb.MovePosition(rb.position + localMove);

        //calculate jump
        if(Input.GetButtonDown("Jump")){
            if(grounded){
                rb.AddForce(transform.up * jumpForce);
            }
        }
    }
}