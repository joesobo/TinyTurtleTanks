using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const int MAXHEALTH = 3;
    [SerializeField]
    private int curHealth;

    void Start()
    {
        curHealth = MAXHEALTH;        
    }

    public void increaseHealth(int amount){
        curHealth += amount;
    }

    public void decreaseHealth(int amount){
        curHealth -= amount;
    }
}
