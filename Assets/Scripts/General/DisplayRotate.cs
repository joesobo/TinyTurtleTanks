using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRotate : MonoBehaviour
{
    public float maxRotationSpeed = 1f;
    public float minRotationSpeed = 1f;
    [SerializeField]
    private float rotateX;
    [SerializeField]
    private float rotateY;
    [SerializeField]
    private float rotateZ;

    private void Start() {
        rotateX = GenRandNum(minRotationSpeed, maxRotationSpeed);
        rotateY = GenRandNum(minRotationSpeed, maxRotationSpeed);
        rotateZ = GenRandNum(minRotationSpeed, maxRotationSpeed);
    }

    private void Update() {
        this.transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime);
    }

    private float GenRandNum(float min, float max){
        float a = Random.Range(-max, max);
        if(a < min && a > -min){
            a = Random.Range(-max, max);
        }
        return a;
    }
}
