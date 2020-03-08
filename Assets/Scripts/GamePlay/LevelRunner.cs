using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour
{
    private int numberOfEnemies;
    [SerializeField]
    private int enemiesLeft;

    private void Start() {
        numberOfEnemies = FindObjectsOfType<SmartEnemy>().Length;
        enemiesLeft = numberOfEnemies;
    }

    public void DecreaseNumEnemy(){
        enemiesLeft--;
    }

    public int getNumEnemiesLeft(){
        return enemiesLeft;
    }
}
