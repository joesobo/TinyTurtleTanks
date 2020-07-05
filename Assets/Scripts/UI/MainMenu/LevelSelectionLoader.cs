using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionLoader : MonoBehaviour {
    public void LoadLevelSelection() {
        this.gameObject.SetActive(true);
    }

    public void UnloadLevelSelection() {
        this.gameObject.SetActive(false);
    }
}
