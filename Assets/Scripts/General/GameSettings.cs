using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;
    public bool isPaused = false;
    public bool useVFX = false;
    public bool useSound = false;
    public bool useParticle = false;
    public bool useGrass = false;
    public bool useTrails = false;
    public bool useClouds = false;
    public bool useMoons = false;
    public bool useWater = false;
    public bool useSun = false;
    
    [Header("Test Settings")]
    public bool useCrates = false;
    public bool useEnvironmentObjects = false;
    public bool useEnemies = false;
    public int currentLevel = 1;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}