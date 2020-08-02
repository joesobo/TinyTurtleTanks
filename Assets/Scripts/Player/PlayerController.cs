﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 10;
    public float rotationChangeSpeed = 50;
    public float rotateSpeed = 0;
    private float maxRotateSpeed = 100;
    public float jumpForce = 200;
    public LayerMask groundMask;

    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private Rigidbody rb;
    private bool grounded;
    private bool landParticleSpawned = false;

    private GameSettings settings;
    public ParticleSystem trailParticles;
    public ParticleSystem landParticles;
    public ParticleSystem waterParticles;
    public Animator animator;

    private Vector3 moveDir;
    private Vector3 localMove;
    private Vector3 targetMoveAmount;
    private float inputX;
    private float inputY;
    private Ray ray;
    private RaycastHit hit;
    private float dist;
    private ParticleSystem ps;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        settings = FindObjectOfType<GameSettings>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update() {
        if (settings.isPaused && rb.constraints == RigidbodyConstraints.FreezeRotation) {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (rb.constraints == RigidbodyConstraints.FreezeAll) {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        animator.SetBool("Walking", false);
        if (!settings.isPaused) {
            //calculate movement
            inputY = Input.GetAxisRaw("Vertical");

            if (inputY != 0) {
                animator.SetBool("Walking", true);
            }

            moveDir = new Vector3(0, 0, inputY).normalized;
            targetMoveAmount = moveDir * speed;
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

            //calculate rotation
            inputX = Input.GetAxisRaw("Horizontal");
            if (inputX > 0) {
                rotateSpeed = maxRotateSpeed;
            }
            else if (inputX < 0) {
                rotateSpeed = -maxRotateSpeed;
            }
            else {
                rotateSpeed = 0;
            }
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

            if (settings.useParticle) {
                if (settings.isPaused) {
                    trailParticles.Pause();
                }
                else {
                    if (inputX != 0 || inputY != 0) {
                        trailParticles.Play();
                    }
                }
            }
            
            //grounded check
            ray = new Ray(transform.position, -transform.up);
            Debug.DrawRay(transform.position, -transform.up * .85f, Color.red);
            if (Physics.Raycast(ray, out hit, .85f, groundMask)) {
                grounded = true;

                if (settings.useParticle && !landParticleSpawned) {
                    ps = Instantiate(landParticles, transform.position, transform.rotation);
                    settings.SetParticleValues(ps);
                    landParticleSpawned = true;
                }
            }
            else {
                grounded = false;
                landParticleSpawned = false;
            }

            //calculate jump
            if (Input.GetButtonDown("Jump")) {
                if (grounded) {
                    rb.AddForce(transform.up * jumpForce);
                }
            }
        }
    }

    private void FixedUpdate() {
        if (!settings.isPaused) {
            localMove = transform.TransformDirection(moveAmount) * Time.deltaTime;
            rb.MovePosition(rb.position + localMove);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Water") {
            if (settings.useParticle ) {
                ps = Instantiate(waterParticles, transform.position, transform.rotation);
                settings.SetParticleValues(ps);
            }
        }
    }
}