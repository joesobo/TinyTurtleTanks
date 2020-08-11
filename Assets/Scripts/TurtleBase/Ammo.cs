using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Turtle/Ammo")]
public class Ammo : ScriptableObject {
    public GameObject prefab;
    public int speed = 0;
    public int damage = 1;                                      // 1-5                   
    public int bounces = 0;                                     // 0-3
    public int decaySpeed = 0;                                  // 0-50
    private BulletMove bulletMove;
    private BombLaunch bombLaunch;
    private MineController mineController;

    public void StartUpBullet(GameObject bullet) {
        bulletMove = bullet.GetComponent<BulletMove>();
        bulletMove.speed = speed;
        bulletMove.decaySpeed = decaySpeed;
        bulletMove.damage = damage;
    }

    public void StartUpAlt(GameObject bullet, AltWeapon altWeapon) {
        bombLaunch = bullet.GetComponent<BombLaunch>();
        mineController = bullet.GetComponent<MineController>();

        if (bombLaunch) {
            bombLaunch.speed = speed;
            bombLaunch.decaySpeed = decaySpeed;
            bombLaunch.damage = damage;

            bombLaunch.launch(altWeapon);
        }

        if (mineController) {
            mineController.decaySpeed = decaySpeed;
            mineController.damage = damage;

            mineController.launch(altWeapon);
        }
    }
}