using System.Collections;
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
    [HideInInspector]
    public bool fire = false;

    private bool shieldLock = false;
    private bool speedLock = false;
    private bool jumpLock = false;
    private bool healthLock = false;
    private bool fireLock = false;

    private float saveSpeed;
    private float saveJump;

    public int waitForSecondsShield = 5;
    public int waitForSecondsSpeed = 5;
    public int waitForSecondsJump = 5;
    public int waitForSecondsFireBurn = 6;
    public float waitForSecondsFireDamage = 1.5f;

    private PlayerController playerController;
    private Health playerHealth;
    private GameSettings settings;

    private GameObject particle;
    private PickupDisplay pickupDisplay;

    private Transform fireObject;
    private Transform shieldObject;

    public GameObject shieldParticles;
    public GameObject speedParticles;
    public GameObject jumpParticles;
    public GameObject healthParticles;

    private PlayerSoundManager soundManager;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        playerController = GetComponent<PlayerController>();
        playerHealth = GetComponent<Health>();
        pickupDisplay = FindObjectOfType<PickupDisplay>();

        shieldObject = findChildByName("Shield");
        fireObject = findChildByName("Fire Particle");

        soundManager = GetComponent<PlayerSoundManager>();
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
            playerHealth.IncreaseHealth(1);
            Instantiate(healthParticles, transform.position, transform.rotation, transform);
            health = false;
        }

        if (fire && fireLock) {
            fireLock = false;
            fireObject.gameObject.SetActive(true);
            StartCoroutine("StartFireDamage");
        }
    }

    IEnumerator StartShield() {
        particle = Instantiate(shieldParticles, transform.position, transform.rotation, transform);
        pickupDisplay.Begin(waitForSecondsShield, new Color32(52, 179, 217, 85));
        shieldObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitForSecondsShield);
        shieldObject.gameObject.SetActive(false);
        shield = false;
        Destroy(particle);
    }

    IEnumerator StartSpeed() {
        particle = Instantiate(speedParticles, transform.position, transform.rotation, transform);
        pickupDisplay.Begin(waitForSecondsShield, new Color32(217, 210, 0, 85));
        playerController.speed = 15;
        yield return new WaitForSeconds(waitForSecondsSpeed);
        playerController.speed = saveSpeed;
        speed = false;
        Destroy(particle);
    }

    IEnumerator StartJump() {
        particle = Instantiate(jumpParticles, transform.position, transform.rotation, transform);
        pickupDisplay.Begin(waitForSecondsShield, new Color32(97, 194, 82, 76));
        playerController.jumpForce = 1000;
        yield return new WaitForSeconds(waitForSecondsJump);
        playerController.jumpForce = saveJump;
        jump = false;
        Destroy(particle);
    }

    public IEnumerator StartFire() {
        yield return new WaitForSeconds(waitForSecondsFireBurn);
        fireObject.gameObject.SetActive(false);
        fire = false;
    }

    IEnumerator StartFireDamage() {
        playerHealth.DecreaseHealth(1);
        yield return new WaitForSeconds(waitForSecondsFireDamage);
        if (fire) StartCoroutine("StartFireDamage");
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
        soundManager.Play(PlayerSoundManager.Clip.powerup);
    }

    public void ActivateSpeed() {
        speed = true;
        speedLock = true;
        soundManager.Play(PlayerSoundManager.Clip.powerup);
    }

    public void ActivateJump() {
        jump = true;
        jumpLock = true;
        soundManager.Play(PlayerSoundManager.Clip.powerup);
    }

    public void ActivateHealth() {
        health = true;
        healthLock = true;
        soundManager.Play(PlayerSoundManager.Clip.powerup);
    }

    public void ActivateFire() {
        fire = true;
        fireLock = true;
    }
}
