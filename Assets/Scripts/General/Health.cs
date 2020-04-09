using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int MAXHEALTH = 3;
    [SerializeField]
    private int curHealth;
    public GameObject curHealthBar;
    private float barMax = .95f;
    private float barMin = 0;
    private LevelRunner levelRunner;
    public GameObject deathParticles;
    public GameSettings settings;

    void Start()
    {
        settings = FindObjectOfType<GameSettings>();
        levelRunner = FindObjectOfType<LevelRunner>();
        curHealth = MAXHEALTH; 
    }

    public void increaseHealth(int amount){
        curHealth += amount;
        updateHealthBar();
    }

    public void decreaseHealth(int amount){
        curHealth -= amount;
        updateHealthBar();
    }

    public int getCurHealth(){
        return curHealth;
    }

    private void updateHealthBar(){
        float healthPercent;
        if(curHealth <= 0){
            healthPercent = 0;
        }
        else if(curHealth < MAXHEALTH){
            healthPercent = (float)curHealth / (float)MAXHEALTH;
        }else{
            healthPercent = 1;
        }
        
        curHealthBar.transform.localScale = (new Vector3(barMax * healthPercent + barMin, .8f, 1));
    }

    private void Update() {
        if(curHealth <= 0){
            onDeath();
        }

        if (curHealth > MAXHEALTH){
            curHealth = MAXHEALTH;
        }
    }

    protected abstract void onDeath();
}
