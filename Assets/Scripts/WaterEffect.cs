using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour {
    public float changeSpeed = 2f;
    public float changeJump = 50f;
    public float changeGravity = 0.2f;

    private PlayerController playerController;
    private SmartEnemy enemyController;

    void Start() {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerController.speed -= changeSpeed;
            playerController.jumpForce -= changeJump;
        }
        else if (other.tag == "Enemy") {
            enemyController = other.gameObject.GetComponent<SmartEnemy>();
            enemyController.speed -= changeSpeed;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            playerController.speed += changeSpeed;
            playerController.jumpForce += changeJump;
        }
        else if (other.tag == "Enemy") {
            enemyController = other.gameObject.GetComponent<SmartEnemy>();
            enemyController.speed += changeSpeed;
        }
    }
}
