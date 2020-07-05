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

    private float saveSpeed;
    private float saveJump;

    public int waitForSecondsShield = 5;
    public int waitForSecondsSpeed = 5;
    public int waitForSecondsJump = 5;

    private PlayerController playerController;
    private Health playerHealth;

    private GameSettings settings;

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
    }

    private void Update() {
        if (shield) {
            shield = false;
            StartCoroutine("StartShield");
        }

        if (speed) {
            speed = false;
            saveSpeed = playerController.speed;
            StartCoroutine("StartSpeed");
        }

        if (jump) {
            jump = false;
            saveJump = playerController.jumpForce;
            StartCoroutine("StartJump");
        }

        if (health) {
            health = false;
            playerHealth.increaseHealth(1);
            Instantiate(healthParticles, col.transform.position, col.transform.rotation, col.transform);
        }
    }

    IEnumerator StartShield() {
        var particle = Instantiate(shieldParticles, col.transform.position, col.transform.rotation, col.transform);
        findChildByName("Shield").gameObject.SetActive(true);
        if (!settings.isPaused) {
            yield return new WaitForSeconds(waitForSecondsShield);
        }
        findChildByName("Shield").gameObject.SetActive(false);
        Destroy(particle);
    }

    IEnumerator StartSpeed() {
        var particle = Instantiate(speedParticles, col.transform.position, col.transform.rotation, col.transform);
        playerController.speed = 15;
        yield return new WaitForSeconds(waitForSecondsSpeed);
        playerController.speed = saveSpeed;
        Destroy(particle);
    }

    IEnumerator StartJump() {
        var particle = Instantiate(jumpParticles, col.transform.position, col.transform.rotation, col.transform);
        playerController.jumpForce = 1000;
        yield return new WaitForSeconds(waitForSecondsJump);
        playerController.jumpForce = saveJump;
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
}
