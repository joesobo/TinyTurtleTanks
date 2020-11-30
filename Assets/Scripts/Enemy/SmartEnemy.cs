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
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    public AltWeapon altWeapon;
    public List<Transform> shootPoints;
    private Transform parent;
    private Transform altParent;
    private bool canShoot = false;
    private bool canJump = false;
    public float jumpForce = 200;
    public LayerMask groundMask;
    private bool grounded;

    private GameSettings settings;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>().gameObject;
        parent = FindObjectOfType<BulletController>().transform;
        altParent = GameObject.Find("BombController").transform;
        settings = FindObjectOfType<GameSettings>();

        GenerateRandomTimes();
        StartCoroutine("StartRotate");
    }

    private void Update() {
        GroundCheck();

        JumpCheck();

        //looking and moving towards player
        if (IsPlayerInRadius(playerLookRadius)) {
            RotateTowardsPlayer();

            CheckCanShootPlayer();

            SlowAndShoot();

            Jump();
        }
        //auto moving state
        else {
            CalculateMove();

            CollisionRotation();

            shootPlayer = false;
        }
    }

    private void FixedUpdate() {
        //move
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime;
        if (rb)
            rb.MovePosition(rb.position + localMove);

        //rotate
        transform.Rotate(0, 1 * curRotate * Time.deltaTime, 0);

    }

    private void CalculateMove() {
        Vector3 targetMoveAmount = Vector3.forward * curSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
    }

    private void CollisionRotation() {
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
    }

    private void Jump() {
        if (canJump) {
            canJump = false;
            rb.AddForce(transform.up * jumpForce);
        }
    }

    private void RotateTowardsPlayer() {
        Vector3 groundNormal = transform.position;
        Vector3 forwardVector = Vector3.Cross(groundNormal, player.transform.position);
        Vector3 rotatedVector = Quaternion.AngleAxis(-90, transform.up) * forwardVector;
        transform.rotation = Quaternion.LookRotation(rotatedVector, groundNormal);
    }

    private void SlowAndShoot() {
        if (shootPlayer) {
            curSpeed = speed / 2;
            if (canShoot) {
                canShoot = false;
                FireRandomWeapon();
            }
        }
    }

    private void JumpCheck() {
        float playerDist = Vector3.Distance(Vector3.zero, player.transform.position);
        float enemyDist = Vector3.Distance(Vector3.zero, transform.position);

        if (Mathf.Abs(playerDist - enemyDist) > 1 && grounded) {
            canJump = true;
        }
        else {
            canJump = false;
        }
    }

    private void GroundCheck() {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit groundHit;

        Debug.DrawRay(transform.position, -transform.up * .85f, Color.red);
        if (Physics.Raycast(ray, out groundHit, .85f, groundMask)) {
            grounded = true;
        }
        else {
            grounded = false;
        }
    }

    private void CheckCanShootPlayer() {
        if (IsPlayerInRadius(playerShootRadius)) {
            if (!shootPlayer) {
                canShoot = true;
            }
            shootPlayer = true;
        }
        else {
            shootPlayer = false;
        }
    }

    private void FireRandomWeapon() {
        List<string> activeWeapons = new List<string>();
        if (weapon) {
            activeWeapons.Add("Weapon");
        }
        if (altWeapon) {
            activeWeapons.Add("AltWeapon");
        }

        if (activeWeapons.Count > 0) {
            if (activeWeapons[Random.Range(0, activeWeapons.Count)] == "Weapon") {
                StartCoroutine("StartShootWeapon");
            }
            else {
                if (altWeapon.inPlay < altWeapon.maxInPlay) {
                    StartCoroutine("StartShootAlt");
                }
                else if (weapon) {
                    StartCoroutine("StartShootWeapon");
                }
            }
        }
    }

    IEnumerator StartRotate() {
        curRotate = rotateSpeed;
        lockRotation = false;
        yield return new WaitForSeconds(rotateSeconds);
        lockRotation = true;
        curRotate = 0;

        GenerateRandomTimes();
        StartCoroutine("StartMove");
    }

    IEnumerator StartMove() {
        curSpeed = speed;
        yield return new WaitForSeconds(moveSeconds);
        curSpeed = 0;

        GenerateRandomTimes();
        StartCoroutine("StartRotate");
    }

    IEnumerator StartShootWeapon() {
        ShootWeaponAtPoints();


        if (weapon.ammo.currentClip > 0) {
            yield return new WaitForSeconds(weapon.timeBetweenShots);
        }
        else {
            yield return new WaitForSeconds(weapon.reloadTime);
            weapon.Reload();
        }

        canShoot = true;
    }

    IEnumerator StartShootAlt() {
        ShootAltWeaponAtPoints();

        yield return new WaitForSeconds(altWeapon.timeBetweenUses);

        canShoot = true;
    }

    private bool IsPlayerInRadius(float radius) {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in hitColliders) {
            if (collider.tag == "Player") {
                return true;
            }
        }
        return false;
    }

    private void ShootWeaponAtPoints() {
        foreach (Transform shootPoint in shootPoints) {
            weapon.Shoot(shootPoint.position, this.transform.rotation, parent);
        }
    }

    private void ShootAltWeaponAtPoints() {
        foreach (Transform shootPoint in shootPoints) {
            altWeapon.Shoot(shootPoint.position, this.transform.rotation, altParent);
        }
        // if (settings.useSound) {
        //     source.volume = settings.soundVolume;
        //     source.Play();
        // }
    }

    private void GenerateRandomTimes() {
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
