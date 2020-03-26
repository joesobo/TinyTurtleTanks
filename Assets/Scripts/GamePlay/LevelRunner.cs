using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour
{
    private int numberOfEnemies;
    [SerializeField]
    private int enemiesLeft;
    public bool isDead;

    private LoseMenu loseMenu;
    private WinMenu winMenu;
    private bool activeLose = true;
    private bool activeWin = true;
    private GameSettings settings;

    public GameObject fx;
    public GameObject grassSpawner;
    public GameObject backgroundMusic;
    public GameObject cloudSpawner;
    public GameObject crateSpawner;
    public GameObject obstacleList;
    public List<GameObject> enemyStuffList;

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();
        CallSettings();

        numberOfEnemies = FindObjectsOfType<SmartEnemy>().Length;
        enemiesLeft = numberOfEnemies;

        loseMenu = FindObjectOfType<LoseMenu>();
        winMenu = FindObjectOfType<WinMenu>();
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

            if (settings.isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
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
        fx.SetActive(settings.useVFX);
        grassSpawner.SetActive(settings.useGrass);
        backgroundMusic.SetActive(settings.useSound);
        cloudSpawner.SetActive(settings.useClouds);
        crateSpawner.SetActive(settings.useCrates);
        obstacleList.SetActive(settings.useEnvironmentObjects);
        foreach (GameObject enemyThing in enemyStuffList)
        {
            enemyThing.SetActive(settings.useEnemies);
        }
    }
}
