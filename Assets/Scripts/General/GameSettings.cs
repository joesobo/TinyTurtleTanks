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
    [Header("Test Settings")]
    public bool useCrates = false;
    public bool useEnvironmentObjects = false;

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