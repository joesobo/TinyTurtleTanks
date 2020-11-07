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
    }

    public void UnlockLevel() {
        if (unlockedLevels < MAXLEVEL) {
            unlockedLevels++;
        }
    }

    public void UnlockNextLevel() {
        if (activeLevel < unlockedLevels) {
            UnlockLevel();
        }
    }
}
