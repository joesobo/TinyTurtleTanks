using UnityEngine;

public class LevelSingleton : MonoBehaviour {
    public static LevelSingleton Instance;
    public int activeLevel = 1;
    public int unlockedLevels = 1;

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
}
