using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionController : MonoBehaviour {
    private const int MAXLEVEL = 5;
    private List<int> unlockedLevels = new List<int>();
    public List<GameObject> selectors = new List<GameObject>();
    private int activeLevel = 1;
    private GameObject activeSelectorRef;
    private LevelLoader levelLoader;

    private void Start() {
        levelLoader = GetComponent<LevelLoader>();

        unlockedLevels.Add(1);
        activeSelectorRef = selectors[0];
    }

    public void UnlockNextLevel() {
        if(unlockedLevels.Count < MAXLEVEL) {
            unlockedLevels.Add(unlockedLevels.Count + 1);
        }
    }

    public void Next() {
        if (activeLevel < unlockedLevels[unlockedLevels.Count-1]) {
            activeLevel++;
        }

        UpdateSelectors();
    }

    public void Previous() {
        if (activeLevel > 1) {
            activeLevel--;
        }

        UpdateSelectors();
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
