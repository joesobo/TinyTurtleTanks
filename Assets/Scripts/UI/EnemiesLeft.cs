using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemiesLeft : MonoBehaviour {
    private LevelRunner levelRunner;
    [HideInInspector]
    public int totalEnemies;
    public GameObject enemyIconPrefab;

    public void CreateIcons() {
        Debug.Log(totalEnemies);

        for (int i = 0; i < totalEnemies; i++) {
            Instantiate(enemyIconPrefab, Vector3.zero, Quaternion.identity, transform);
        }
    }

    private void Update() {
        //text.text = "Remaining: " + levelRunner.getNumEnemiesLeft();
    }
}
