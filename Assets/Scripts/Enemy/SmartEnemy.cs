using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour {
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

    private GameObject player;
    public float playerLookRadius = 5;
    public float playerShootRadius = 2;
    private bool lockPlayer = false;
    private bool shootPlayer = false;
    public float shootSeconds = 5;
    public GameObject bullet;
    public Material bulletMat;
    public List<Transform> shootPoints;
    private Transform parent;
    private bool canShoot = false;

    private AudioSource source;
    private GameSettings settings;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>().gameObject;
        settings = FindObjectOfType<GameSettings>();
        source = GetComponent<AudioSource>();
        parent = FindObjectOfType<BulletController>().transform;
        bullet.GetComponent<MeshRenderer>().material = bulletMat;

        randomTimes();
        StartCoroutine("StartRotate");
    }

    private void Update() {
        if (!settings.isPaused) {
            //check for player in look radius
            if (isPlayerInRadius(playerLookRadius)) {
                lockPlayer = true;
            }
            else {
                lockPlayer = false;
            }

            //check for player in shoot radius
            if (isPlayerInRadius(playerShootRadius)) {
                if (!shootPlayer) {
                    canShoot = true;
                }
                shootPlayer = true;
            }
            else {
                shootPlayer = false;
            }

            //looking and moving towards player
            if (lockPlayer) {
                Vector3 groundNormal = transform.position;
                Vector3 forwardVector = Vector3.Cross(groundNormal, player.transform.position);
                Vector3 rotatedVector = Quaternion.AngleAxis(-90, transform.up) * forwardVector;
                transform.rotation = Quaternion.LookRotation(rotatedVector, groundNormal);

                //stop moving and shoot at player
                if (shootPlayer) {
                    curSpeed = 0;
                    if (canShoot) {
                        canShoot = false;
                        StartCoroutine("StartShoot");
                    }
                }
            }
            //auto moving state
            else {
                //calculate move if speed
                Vector3 targetMoveAmount = Vector3.forward * curSpeed;
                moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

                offsetPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                RaycastHit hit;
                if (Physics.Raycast(offsetPosition, transform.TransformDirection(Vector3.forward), out hit, 2, objectLayerMask)) {
                    Debug.DrawRay(offsetPosition, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    curRotate = rotateSpeed;
                }
                else {
                    Debug.DrawRay(offsetPosition, transform.TransformDirection(Vector3.forward) * 2, Color.white);
                    if (lockRotation) {
                        curRotate = 0;
                    }
                }

                lockPlayer = false;
                shootPlayer = false;
            }
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

    IEnumerator StartRotate() {
        curRotate = rotateSpeed;
        lockRotation = false;
        yield return new WaitForSeconds(rotateSeconds);
        lockRotation = true;
        curRotate = 0;

        randomTimes();
        StartCoroutine("StartMove");
    }

    IEnumerator StartMove() {
        curSpeed = speed;
        yield return new WaitForSeconds(moveSeconds);
        curSpeed = 0;

        randomTimes();
        StartCoroutine("StartRotate");
    }

    IEnumerator StartShoot() {
        if (!settings.isPaused) {
            ShootAtPoints();
        }
        yield return new WaitForSeconds(shootSeconds);
        canShoot = true;
    }

    private bool isPlayerInRadius(float radius) {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in hitColliders) {
            if (collider.tag == "Player") {
                return true;
            }
        }
        return false;
    }

    private void ShootAtPoints() {
        foreach (Transform shootPoint in shootPoints) {
            Instantiate(bullet, shootPoint.position, shootPoint.rotation, parent);
        }
        if (settings.useSound) {
            source.volume = settings.soundVolume;
            source.Play();
        }
    }

    private void randomTimes() {
        moveSeconds = Random.Range(1, 7);
        rotateSeconds = Random.Range(1, 5);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color32(5, 170, 250, 100);
        Gizmos.DrawSphere(transform.position, playerLookRadius);
        Gizmos.color = new Color32(250, 50, 10, 100);
        Gizmos.DrawSphere(transform.position, playerShootRadius);
    }
}
