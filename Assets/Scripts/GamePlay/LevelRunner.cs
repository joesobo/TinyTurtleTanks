using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour {
    [HideInInspector]
    public int numberOfEnemies = 0;
    [SerializeField]
    private int enemiesRemaining;
    public bool isDead;

    private LoseMenu loseMenu;
    private WinMenu winMenu;
    private QuitMenu quitMenu;
    [HideInInspector]
    public bool activeLose = true;
    [HideInInspector]
    public bool activeWin = true;
    private GameSettings settings;

    public GameObject fx;
    public GameObject grassSpawner;
    public GameObject seaweedSpawner;
    public GameObject backgroundMusic;
    public GameObject cloudSpawner;
    public GameObject moonSpawner;
    public GameObject boidManager;
    public GameObject atmosphere;
    public GameObject crateSpawner;
    public List<GameObject> obstacleList;
    public GameObject footprints;
    public List<GameObject> enemyStuffList;
    public List<GameObject> particleList;
    public GameObject water;
    public RotateAroundCenter daylightCycle;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        settings.isPaused = false;

        quitMenu = FindObjectOfType<QuitMenu>();

        if (settings.useEnemies) {
            loseMenu = FindObjectOfType<LoseMenu>();
            winMenu = FindObjectOfType<WinMenu>();

            numberOfEnemies = FindObjectsOfType<SmartEnemy>().Length;
            enemiesRemaining = numberOfEnemies;

            EnemiesLeft enemiesLeft = FindObjectOfType<EnemiesLeft>();

            enemiesLeft.totalEnemies = numberOfEnemies;
            enemiesLeft.CreateIcons();
        }

        foreach (ParticleSystem ps in FindObjectsOfType(typeof(ParticleSystem))) {
            ps.Stop();
        }

        UpdateSettings();
    }

    private void Update() {
        if (settings.useEnemies) {
            if (isDead && activeLose && activeWin) {
                activeLose = false;
                loseMenu.ActivateLose();
            }

            else if (GetNumEnemiesLeft() <= 0 && activeWin && activeLose) {
                activeWin = false;
                winMenu.ActivateWin();
            }

            if ((!activeLose || !activeWin) && !settings.isPaused) {
                settings.isPaused = true;
            }
        }

        if (settings.isPaused) {
            Cursor.lockState = CursorLockMode.None;
            ParticleLock(true);
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            ParticleLock(false);
        }
    }

    public void DecreaseNumEnemy() {
        enemiesRemaining--;
    }

    public int GetNumEnemiesLeft() {
        return enemiesRemaining;
    }

    public void UpdateSettings() {
        //SFX and VFX
        fx.SetActive(settings.useVFX);
        if (grassSpawner) grassSpawner.SetActive(settings.useGrass);
        if (seaweedSpawner) seaweedSpawner.SetActive(settings.useSeaweed);
        backgroundMusic.SetActive(settings.useMusic);

        //Obstacles and Environment
        if (cloudSpawner) cloudSpawner.SetActive(settings.useClouds);
        if (moonSpawner) moonSpawner.SetActive(settings.useMoons);
        if (atmosphere) atmosphere.SetActive(settings.useAtmosphere);
        if (crateSpawner) crateSpawner.SetActive(settings.useCrates);

        //Obstacles
        foreach (GameObject obstacle in obstacleList) {
            obstacle.SetActive(settings.useEnvironmentObjects);
        }

        footprints.SetActive(settings.useFootPrints);
        if (water) water.SetActive(settings.useWater);
        daylightCycle.enabled = settings.daylightCycle;

        //Enemies
        foreach (GameObject enemyThing in enemyStuffList) {
            enemyThing.SetActive(settings.useEnemies);
        }

        //Particles
        foreach (GameObject particle in particleList) {
            if (settings.useParticle) {
                particle.SetActive(settings.useParticle);
                ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                settings.SetParticleValues(ps);
            }
        }

        //UI Menu
        quitMenu.gameObject.SetActive(settings.useEnemies);
    }

    private void ParticleLock(bool active) {
        foreach (ParticleSystem ps in FindObjectsOfType(typeof(ParticleSystem))) {
            if (active) {
                ps.Pause();
            }
            else {
                ps.Play();
            }
        }
    }
}
