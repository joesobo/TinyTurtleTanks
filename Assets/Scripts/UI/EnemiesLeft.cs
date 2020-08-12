using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemiesLeft : MonoBehaviour {
    private LevelRunner levelRunner;
    private TextElement text;

    private void Start() {
        levelRunner = FindObjectOfType<LevelRunner>();
        text = transform.GetChild(1).GetComponent<TextElement>();
    }

    private void Update() {
        text.text = "Remaining: " + levelRunner.getNumEnemiesLeft();
    }
}
