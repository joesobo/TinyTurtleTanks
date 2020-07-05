using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLeft : MonoBehaviour {
    private LevelRunner levelRunner;
    private Text text;

    private void Start() {
        levelRunner = FindObjectOfType<LevelRunner>();
        text = transform.GetChild(1).GetComponent<Text>();
    }

    private void Update() {
        text.text = "Remaining: " + levelRunner.getNumEnemiesLeft();
    }
}
