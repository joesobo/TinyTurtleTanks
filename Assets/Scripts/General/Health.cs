using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour {
    //[HideInInspector]
    public int MAXHEALTH = 3;
    [SerializeField]
    private int curHealth;
    public GameObject curHealthBar;
    private float barMax = 0.95f;
    private float barMin = 0;
    private LevelRunner levelRunner;
    public GameObject bloodParticles;
    public GameObject deathParticles;
    [HideInInspector]
    public GameSettings settings;
    private MeshRenderer[] meshRenderers;
    public Material flashMaterial;
    private bool flashInProgress = false;
    private bool cooldownInProgress = false;
    private List<Material> tempMaterials = new List<Material>();
    private CameraShake screenShake;

    private float flashTime = 0.15f;
    private float cooldownTime = 0.5f;
    private bool isDead = false;

    [HideInInspector]
    public AudioSource source;
    public AudioClip hurtAudioClip;
    public AudioClip deathAudioClip;

    void Start() {
        settings = FindObjectOfType<GameSettings>();
        levelRunner = FindObjectOfType<LevelRunner>();
        meshRenderers = transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
        curHealth = MAXHEALTH;
        screenShake = FindObjectOfType<CameraShake>();

        source = GetComponent<AudioSource>();
        source.volume = settings.soundVolume;
    }

    private void Update() {
        if (curHealth <= 0 && !isDead) {
            isDead = true;

            OnDeath();
        }
    }

    public void IncreaseHealth(int amount) {
        curHealth += amount;
        if (curHealth > MAXHEALTH) {
            curHealth = MAXHEALTH;
        }
        UpdateHealthBar();
    }

    public void DecreaseHealth(int amount) {
        if (!cooldownInProgress) {
            StartCoroutine("DamageCooldown");

            curHealth -= amount;
            if (curHealth < 0) {
                curHealth = 0;
            }
            UpdateHealthBar();
            if (curHealth > 0) {
                if (!flashInProgress) {
                    StartCoroutine("DamageFlash");
                }

                if (settings.useParticle) {
                    Instantiate(bloodParticles, transform.position, transform.rotation);
                }

                if (settings.useSound) {
                    source.PlayOneShot(hurtAudioClip);
                }
            }
            if (transform.name == "TurtlePlayer") {
                StartCoroutine(screenShake.Shake());
            }
        }
    }

    public int GetCurHealth() {
        return curHealth;
    }

    private void UpdateHealthBar() {
        float healthPercent;
        if (curHealth <= 0) {
            healthPercent = 0;
        }
        else if (curHealth < MAXHEALTH) {
            healthPercent = (float)curHealth / (float)MAXHEALTH;
        }
        else {
            healthPercent = 1;
        }

        curHealthBar.transform.localScale = (new Vector3(barMax * healthPercent + barMin, .8f, 1));
    }

    IEnumerator DamageCooldown() {
        cooldownInProgress = true;
        yield return new WaitForSeconds(cooldownTime);
        cooldownInProgress = false;
    }

    IEnumerator DamageFlash() {
        flashInProgress = true;
        //save old materials and set to white
        foreach (MeshRenderer renderer in meshRenderers) {
            tempMaterials.Add(renderer.material);
            renderer.material = flashMaterial;
        }
        yield return new WaitForSeconds(flashTime);
        //reset materials to old

        for (int index = 0; index < meshRenderers.Length; index++) {
            meshRenderers[index].material = tempMaterials[index];
        }
        tempMaterials.Clear();
        flashInProgress = false;
    }

    protected virtual void OnDeath() {
        if (settings.useSound) {
            source.PlayOneShot(deathAudioClip);
        }
    }
}
