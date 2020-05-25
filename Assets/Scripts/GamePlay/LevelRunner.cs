using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LevelRunner : MonoBehaviour
{
    private int numberOfEnemies;
    [SerializeField]
    private int enemiesLeft;
    public bool isDead;

    private LoseMenu loseMenu;
    private WinMenu winMenu;
    private QuitMenu quitMenu;
    private bool activeLose = true;
    private bool activeWin = true;
    private GameSettings settings;

    public GameObject fx;
    public GameObject grassSpawner;
    public GameObject seaweedSpawner;
    public GameObject backgroundMusic;
    public GameObject cloudSpawner;
    public GameObject moonSpawner;
    public GameObject crateSpawner;
    public GameObject obstacleList;
    public List<GameObject> enemyStuffList;
    public List<GameObject> particleList;
    public GameObject water;
    public GameObject sun;

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();

        loseMenu = FindObjectOfType<LoseMenu>();
        winMenu = FindObjectOfType<WinMenu>();
        quitMenu = FindObjectOfType<QuitMenu>();

        numberOfEnemies = FindObjectsOfType<SmartEnemy>().Length;
        enemiesLeft = numberOfEnemies;

        CallSettings();       
    }

    private void Update()
    {
        if (settings.useEnemies)
        {
            if (isDead && activeLose && activeWin)
            {
                activeLose = false;
                loseMenu.ActivateLose();
            }

            else if (getNumEnemiesLeft() <= 0 && activeWin && activeLose)
            {
                activeWin = false;
                winMenu.ActivateWin();
            }

            if (!activeLose || !activeWin)
            {
                settings.isPaused = true;
            }
        }

        if (settings.isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void DecreaseNumEnemy()
    {
        enemiesLeft--;
    }

    public int getNumEnemiesLeft()
    {
        return enemiesLeft;
    }

    private void CallSettings()
    {
        //SFX and VFX
        fx.SetActive(settings.useVFX);
        grassSpawner.SetActive(settings.useGrass);
        seaweedSpawner.SetActive(settings.useSeaweed);
        backgroundMusic.SetActive(settings.useSound);

        //Obstacles and Environment
        cloudSpawner.SetActive(settings.useClouds);
        moonSpawner.SetActive(settings.useMoons);
        crateSpawner.SetActive(settings.useCrates);
        obstacleList.SetActive(settings.useEnvironmentObjects);
        water.SetActive(settings.useWater);
        sun.SetActive(settings.useSun);

        //Enemies
        foreach (GameObject enemyThing in enemyStuffList)
        {
            enemyThing.SetActive(settings.useEnemies);
        }

        //Particles
        foreach (GameObject particle in particleList)
        {
            particle.SetActive(settings.useParticle);

            float limitParticle = settings.particleSlider;

            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
            var main = ps.main;
            var emissionModule = ps.emission;
            var rate = emissionModule.rateOverTime;         

            int oldMaxParticles = main.maxParticles;
            float oldRateOverTime = rate.constant;

            emissionModule = ps.emission;
            ParticleSystem.MinMaxCurve tempCurve = emissionModule.rateOverTime;
            tempCurve.constant = oldRateOverTime * limitParticle;
            emissionModule.rateOverTime = tempCurve;

            main.maxParticles = (int)(oldMaxParticles * limitParticle);
        }

        //UI Menu
        quitMenu.gameObject.SetActive(settings.useEnemies);
    }
}
