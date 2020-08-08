using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Turtle/Ammo")]
public class Ammo : ScriptableObject {
    public GameObject prefab;
    public float speed = 0;
    public int damage = 1;                                      // 1-5                   
    public int bounces = 0;                                     // 0-3
    public int decaySpeed = 0;                                  // 0-50
    public bool doesExplode = false;
    private BulletMove bulletMove;

    public void StartUp(GameObject bullet) {
        bulletMove = bullet.GetComponent<BulletMove>();
        bulletMove.speed = speed;
        bulletMove.decaySpeed = decaySpeed;
        bulletMove.damage = damage;
    }
}