using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public BaseTurtle BaseTurtle;

    private EnemyHealth enemyHealth;
    private SmartEnemy smartEnemy;

    private GameSettings settings;

    void Awake() {
        settings = FindObjectOfType<GameSettings>();

        if (settings.useEnemies) {
            CreateEnemy(this.transform.position);
        }
    }

    public void CreateEnemy(Vector3 position) {
        GameObject turtle = Instantiate(BaseTurtle.prefab, position, Quaternion.identity);

        enemyHealth = turtle.GetComponent<EnemyHealth>();
        enemyHealth.MAXHEALTH = BaseTurtle.health;

        smartEnemy = turtle.GetComponent<SmartEnemy>();
        if (!smartEnemy) {
            smartEnemy = turtle.GetComponentInChildren<SmartEnemy>();
        }
        smartEnemy.speed = BaseTurtle.moveSpeed;
        smartEnemy.rotateSpeed = BaseTurtle.rotateSpeed;
        smartEnemy.jumpForce = BaseTurtle.jumpForce;
        smartEnemy.weapon = BaseTurtle.weapon;
        smartEnemy.altWeapon = BaseTurtle.altWeapon;

        if (BaseTurtle.weapon) {
            BaseTurtle.weapon.Reload();
        }
    }
}
