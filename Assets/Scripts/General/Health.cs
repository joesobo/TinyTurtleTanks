using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MAXHEALTH = 3;
    [SerializeField]
    private int curHealth;
    public GameObject curHealthBar;
    private float barMax = .95f;
    private float barMin = 0;

    void Start()
    {
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

    private void updateHealthBar(){
        float healthPercent = (float)curHealth / (float)MAXHEALTH;
        curHealthBar.transform.localScale = (new Vector3(barMax * healthPercent + barMin, .8f, 1));
    }

    private void Update() {
        if(curHealth <= 0){
            Destroy(gameObject);
        }

        if (curHealth > MAXHEALTH){
            curHealth = MAXHEALTH;
        }
    }
}
