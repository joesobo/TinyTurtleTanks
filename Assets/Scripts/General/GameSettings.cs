using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameSettings : MonoBehaviour {
    public static GameSettings Instance;
    public bool isPaused = false;
    public bool useCheats = false;
    public bool useVFX = false;
    public bool useSound = false;
    [Range(0, 1)]
    public float soundVolume = 1.0f;
    public bool useMusic = false;
    [Range(0, 1)]
    public float musicVolume = 1.0f;
    [HideInInspector]
    public bool useParticle = false;
    [Range(0, 5)]
    public float particleSlider = 1.0f;
    public bool useGrass = false;
    public bool useSeaweed = false;
    public bool useFootPrints = false;
    public bool useClouds = false;
    public bool useMoons = false;
    public bool useBirds = false;
    public bool useAtmosphere = false;
    public bool useWater = false;
    public bool daylightCycle = false;

    [Header("Test Settings")]
    public bool useCrates = false;
    public bool useEnvironmentObjects = false;
    public bool useEnemies = false;
    public int currentLevel = 1;

    [HideInInspector]
    public float defaultTimeScale = 1;

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        if (particleSlider == 0) {
            useParticle = false;
        }

        SetupPlayerPrefs();
    }

    void Update() {
        if (isPaused) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = defaultTimeScale;
        }
    }

    private void SetupPlayerPrefs() {
        if (!PlayerPrefs.HasKey("soundSetting")) {
            ResetPlayerPrefs();
        }

        UpdateBoolSettings();
    }

    public void UpdatePlayerPrefs() {
        PlayerPrefs.SetInt("cheatSetting", (useCheats ? 1 : 0));
        PlayerPrefs.SetInt("vfxSetting", (useVFX ? 1 : 0));
        PlayerPrefs.SetInt("soundSetting", (useSound ? 1 : 0));
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
        PlayerPrefs.SetInt("musicSetting", (useMusic ? 1 : 0));
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetInt("particleSetting", (useParticle ? 1 : 0));
        PlayerPrefs.SetFloat("particleValue", particleSlider);
        PlayerPrefs.SetInt("grassSetting", (useGrass ? 1 : 0));
        PlayerPrefs.SetInt("trailSetting", (useFootPrints ? 1 : 0));
        PlayerPrefs.SetInt("cloudSetting", (useClouds ? 1 : 0));
        PlayerPrefs.SetInt("moonSetting", (useMoons ? 1 : 0));
        PlayerPrefs.SetInt("birdSetting", (useBirds ? 1 : 0));
        PlayerPrefs.SetInt("daylightCycle", (daylightCycle ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void ResetPlayerPrefs() {
        PlayerPrefs.SetInt("cheatSetting", 0);
        PlayerPrefs.SetInt("vfxSetting", 1);
        PlayerPrefs.SetInt("soundSetting", 1);
        PlayerPrefs.SetFloat("soundVolume", 1);
        PlayerPrefs.SetInt("musicSetting", 1);
        PlayerPrefs.SetFloat("musicVolume", 1);
        PlayerPrefs.SetInt("particleSetting", 1);
        PlayerPrefs.SetFloat("particleValue", 1);
        PlayerPrefs.SetInt("grassSetting", 1);
        PlayerPrefs.SetInt("trailSetting", 1);
        PlayerPrefs.SetInt("cloudSetting", 1);
        PlayerPrefs.SetInt("moonSetting", 1);
        PlayerPrefs.SetInt("birdSetting", 1);
        PlayerPrefs.SetInt("daylightCycle", 1);
        PlayerPrefs.Save();

        UpdateBoolSettings();
    }

    private void UpdateBoolSettings() {
        useCheats = (PlayerPrefs.GetInt("cheatSetting") != 0);
        useVFX = (PlayerPrefs.GetInt("vfxSetting") != 0);
        useSound = (PlayerPrefs.GetInt("soundSetting") != 0);
        soundVolume = PlayerPrefs.GetFloat("soundVolume");
        useMusic = (PlayerPrefs.GetInt("musicSetting") != 0);
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        useParticle = (PlayerPrefs.GetInt("particleSetting") != 0);
        particleSlider = PlayerPrefs.GetFloat("particleValue");
        useGrass = (PlayerPrefs.GetInt("grassSetting") != 0);
        useFootPrints = (PlayerPrefs.GetInt("trailSetting") != 0);
        useClouds = (PlayerPrefs.GetInt("cloudSetting") != 0);
        useMoons = (PlayerPrefs.GetInt("moonSetting") != 0);
        useBirds = (PlayerPrefs.GetInt("birdSetting") != 0);
        daylightCycle = (PlayerPrefs.GetInt("daylightCycle") != 0);
    }

    public void SetParticleValues(ParticleSystem ps) {
        if (ps != null) {
            var main = ps.main;
            var emissionModule = ps.emission;

            int oldMaxParticles = main.maxParticles;
            float oldRateOverTime = emissionModule.rateOverTime.constant;
            Burst oldBurst = emissionModule.GetBurst(0);

            emissionModule = ps.emission;
            emissionModule.rateOverTime = new MinMaxCurve(oldRateOverTime * particleSlider);

            emissionModule.SetBurst(0, new Burst(0, oldBurst.count.constant * particleSlider));

            main.maxParticles = (int)(oldMaxParticles * particleSlider);
        }
    }
}