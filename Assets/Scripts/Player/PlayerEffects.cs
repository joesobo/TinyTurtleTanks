using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour {
    [HideInInspector]
    public bool shield = false;
    [HideInInspector]
    public bool speed = false;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool health = false;

    private bool shieldLock = false;
    private bool speedLock = false;
    private bool jumpLock = false;
    private bool healthLock = false;

    private float saveSpeed;
    private float saveJump;

    public int waitForSecondsShield = 5;
    public int waitForSecondsSpeed = 5;
    public int waitForSecondsJump = 5;

    private PlayerController playerController;
    private Health playerHealth;
    private GameSettings settings;

    private GameObject particle;
    private PickupDisplay pickupDisplay;

    public GameObject shieldParticles;
    public GameObject speedParticles;
    public GameObject jumpParticles;
    public GameObject healthParticles;

    [HideInInspector]
    public Collider col;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        playerController = GetComponent<PlayerController>();
        playerHealth = GetComponent<Health>();
        pickupDisplay = FindObjectOfType<PickupDisplay>();
    }

    private void Update() {
        if (shield && shieldLock) {
            shieldLock = false;
            StartCoroutine("StartShield");
        }

        if (speed && speedLock) {
            speedLock = false;
            saveSpeed = playerController.speed;
            StartCoroutine("StartSpeed");
        }

        if (jump && jumpLock) {
            jumpLock = false;
            saveJump = playerController.jumpForce;
            StartCoroutine("StartJump");
        }

        if (health && healthLock) {
            healthLock = false;
            playerHealth.increaseHealth(1);
            Instantiate(healthParticles, col.transform.position, col.transform.rotation, col.transform);
            health = false;
        }
    }

    IEnumerator StartShield() {
        particle = Instantiate(shieldParticles, col.transform.position, col.transform.rotation, col.transform);
        pickupDisplay.begin(waitForSecondsShield, new Color32(52, 179, 217, 85));
        findChildByName("Shield").gameObject.SetActive(true);
        yield return new WaitForSeconds(waitForSecondsShield);
        findChildByName("Shield").gameObject.SetActive(false);
        shield = false;
        Destroy(particle);
    }

    IEnumerator StartSpeed() {
        particle = Instantiate(speedParticles, col.transform.position, col.transform.rotation, col.transform);
        pickupDisplay.begin(waitForSecondsShield, new Color32(217, 210, 0, 85));
        playerController.speed = 15;
        yield return new WaitForSeconds(waitForSecondsSpeed);
        playerController.speed = saveSpeed;
        speed = false;
        Destroy(particle);
    }

    IEnumerator StartJump() {
        particle = Instantiate(jumpParticles, col.transform.position, col.transform.rotation, col.transform);
        pickupDisplay.begin(waitForSecondsShield, new Color32(97, 194, 82, 76));
        playerController.jumpForce = 1000;
        yield return new WaitForSeconds(waitForSecondsJump);
        playerController.jumpForce = saveJump;
        jump = false;
        Destroy(particle);
    }

    private Transform findChildByName(string text) {
        Transform saveChild = null;

        foreach (Transform child in transform) {
            if (child.name == text) {
                saveChild = child;
            }
        }

        return saveChild;
    }

    public void ActivateShield() {
        shield = true;
        shieldLock = true;
    }

    public void ActivateSpeed() {
        speed = true;
        speedLock = true;
    }

    public void ActivateJump() {
        jump = true;
        jumpLock = true;
    }

    public void ActivateHealth() {
        health = true;
        healthLock = true;
    }
}
