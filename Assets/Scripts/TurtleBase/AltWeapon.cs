using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AltWeapon", menuName = "Turtle/AltWeapon")]
public class AltWeapon : ScriptableObject {
    public float timeBetweenUses = 0.5f;
    public float knockback = 1;
    public int maxInPlay = 3;
    public Ammo ammo = null;
    public BulletType type;

    public int inPlay { get; set; } = 0;

    public enum BulletType {
        Bomb,
        Mine
    }

    public void shoot(Vector3 position, Quaternion rotation, Transform parent) {
        ammo.StartUpAlt(Instantiate(ammo.prefab, position, rotation, parent), this, type);
    }
}