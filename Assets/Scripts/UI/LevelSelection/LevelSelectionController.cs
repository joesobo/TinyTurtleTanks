using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionController : MonoBehaviour {
    public List<GameObject> selectors = new List<GameObject>();    
    
    private LevelSingleton ls;
    private GameObject activeSelectorRef;
    private LevelLoader levelLoader;
    private GameSettings settings;

    private const int MAXLEVEL = 5;

    private void OnEnable() {
        ls = FindObjectOfType<LevelSingleton>();
        settings = FindObjectOfType<GameSettings>();
        levelLoader = GetComponent<LevelLoader>();
        activeSelectorRef = selectors[ls.activeLevel - 1];
        activeSelectorRef.SetActive(true);
    }

    public void UnlockNextLevel() {
        if (ls.unlockedLevels < MAXLEVEL) {
            ls.unlockedLevels++;
        }
    }

    public void Next() {
        if (ls.activeLevel < ls.unlockedLevels) {
            ls.activeLevel++;

            UpdateSelectors();
        }
    }

    public void Previous() {
        if (ls.activeLevel > 1) {
            ls.activeLevel--;

            UpdateSelectors();
        }
    }

    public void Play() {
        settings.isPaused = false;
        levelLoader.LoadLevel(ls.activeLevel);
    }

    private void UpdateSelectors() {
        activeSelectorRef.SetActive(false);
        activeSelectorRef = selectors[ls.activeLevel - 1];
        activeSelectorRef.SetActive(true);
    }
}
