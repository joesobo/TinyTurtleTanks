using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemiesLeft : MonoBehaviour {
    private LevelRunner levelRunner;
    private TextElement text;
    private int totalEnemies;

    private void Start() {
        levelRunner = FindObjectOfType<LevelRunner>();
        totalEnemies = levelRunner.numberOfEnemies;
        
        
    }

    private void Update() {
        text.text = "Remaining: " + levelRunner.getNumEnemiesLeft();
    }
}
