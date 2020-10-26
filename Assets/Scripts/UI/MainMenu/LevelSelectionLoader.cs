using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionLoader : MonoBehaviour {
    private void Start() {
        //UnloadLevelSelection();
    }

    public void LoadLevelSelection() {
        this.gameObject.SetActive(true);
    }

    public void UnloadLevelSelection() {
        this.gameObject.SetActive(false);
    }
}
