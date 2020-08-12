using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainLoader : MonoBehaviour {
    public List<GameObject> mainObjects;

    void Awake() {
        LoadObjects(true);
    }

    public void LoadMain() {
        LoadObjects(true);
    }

    public void UnloadMain() {
        LoadObjects(false);
    }

    private void LoadObjects(bool active) {
        foreach (GameObject obj in mainObjects) {
            obj.SetActive(active);
        }
    }
}
