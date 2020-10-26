using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionController : MonoBehaviour {
    private const int MAXLEVEL = 5;
    private int unlockedLevels = 1;
    public List<GameObject> selectors = new List<GameObject>();
    private int activeLevel = 1;
    private GameObject activeSelectorRef;
    private LevelLoader levelLoader;

    private void OnEnable() {
        levelLoader = GetComponent<LevelLoader>();
        activeSelectorRef = selectors[activeLevel - 1];
    }

    public void UnlockNextLevel() {
        if(unlockedLevels < MAXLEVEL) {
            unlockedLevels++;
        }
    }

    public void Next() {
        if (activeLevel < unlockedLevels) {
            activeLevel++;

            UpdateSelectors();
        }
    }

    public void Previous() {
        if (activeLevel > 1) {
            activeLevel--;

            UpdateSelectors();
        }
    }

    public void Play() {
        levelLoader.LoadLevel(activeLevel);
    }

    private void UpdateSelectors() {
        activeSelectorRef.SetActive(false);
        activeSelectorRef = selectors[activeLevel-1];
        activeSelectorRef.SetActive(true);
    }
}
