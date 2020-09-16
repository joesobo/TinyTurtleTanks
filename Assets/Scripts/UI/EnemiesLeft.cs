using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLeft : MonoBehaviour {
    private LevelRunner levelRunner;
    [HideInInspector]
    public int totalEnemies;
    public GameObject enemyIconPrefab;

    private List<GameObject> iconList = new List<GameObject>();
    private int activeIndex = 0;

    private void Start() {
        levelRunner = FindObjectOfType<LevelRunner>();
    }

    public void CreateIcons() {
        for (int i = 0; i < totalEnemies; i++) {
            iconList.Add(Instantiate(enemyIconPrefab, Vector3.zero, Quaternion.identity, transform));
        }
    }

    private void Update() {
        if (totalEnemies != levelRunner.GetNumEnemiesLeft()) {
            int difference = totalEnemies - levelRunner.GetNumEnemiesLeft();
            totalEnemies = levelRunner.GetNumEnemiesLeft();

            for (int i = 0; i < difference; i++) {
                transform.GetChild(activeIndex).GetChild(0).gameObject.SetActive(true);

                activeIndex++;
            }
        }
    }
}
