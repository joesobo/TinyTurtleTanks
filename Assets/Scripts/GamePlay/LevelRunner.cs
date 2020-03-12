using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour
{
    private int numberOfEnemies;
    [SerializeField]
    private int enemiesLeft;
    public bool isDead;

    private LoseMenu loseMenu;
    private WinMenu winMenu;
    private bool activeLose = true;
    private bool activeWin = true;

    private void Start() {
        numberOfEnemies = FindObjectsOfType<SmartEnemy>().Length;
        enemiesLeft = numberOfEnemies;

        loseMenu = FindObjectOfType<LoseMenu>();
        winMenu = FindObjectOfType<WinMenu>();
    }

    private void Update() {
        if(isDead && activeLose && activeWin){
            activeLose = false;
            loseMenu.ActivateLose();
        }

        else if(getNumEnemiesLeft() <= 0 && activeWin && activeLose){
            activeWin = false;
            winMenu.ActivateWin();
        }
    }

    public void DecreaseNumEnemy(){
        enemiesLeft--;
    }

    public int getNumEnemiesLeft(){
        return enemiesLeft;
    }
}
