using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int numberOfBullets;
    public int maxBullets = 5;

    void Update()
    {
        numberOfBullets = transform.childCount;

        if(numberOfBullets > maxBullets){
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
