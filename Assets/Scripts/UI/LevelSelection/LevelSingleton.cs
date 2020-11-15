using UnityEngine;

public class LevelSingleton : MonoBehaviour {
    public static LevelSingleton Instance;
    public int activeLevel = 1;
    public int unlockedLevels = 1;

    private const int MAXLEVEL = 5;

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        if(!PlayerPrefs.HasKey("levelUnlocked")) {
            PlayerPrefs.SetInt("levelUnlocked", 1);
            PlayerPrefs.Save();
        }else{
            unlockedLevels = PlayerPrefs.GetInt("levelUnlocked");
            activeLevel = unlockedLevels;
        }
    }

    public void UnlockLevel() {
        if (unlockedLevels < MAXLEVEL) {
            unlockedLevels++;
            PlayerPrefs.SetInt("levelUnlocked", unlockedLevels);
            PlayerPrefs.Save();
        }
    }

    public void UnlockNextLevel() {
        if (activeLevel == unlockedLevels) {
            UnlockLevel();
        }
    }
}
