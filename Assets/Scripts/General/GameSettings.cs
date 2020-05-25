using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;
    public bool isPaused = false;
    public bool useVFX = false;
    public bool useSound = false;
    public bool useParticle = false;
    [Range(0,1)]
    public float particleSlider = 1.0f;
    public bool useGrass = false;
    public bool useSeaweed = false;
    public bool useFootPrints = false;
    public bool useClouds = false;
    public bool useMoons = false;
    public bool useWater = false;
    public bool useSun = false;
    [Range(0,1)]
    public float soundVolume = 1.0f;
    
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