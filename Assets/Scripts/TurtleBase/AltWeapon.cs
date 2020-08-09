using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AltWeapon", menuName = "Turtle/AltWeapon")]
public class AltWeapon : ScriptableObject {
    public float timeBetweenUses = 0.5f;
    //public int maxInUse = 3;
    public float knockback = 1;
    public Ammo ammo = null;

    public void shoot(Vector3 position, Quaternion rotation, Transform parent) {
        ammo.StartUpAlt(Instantiate(ammo.prefab, position, rotation, parent));
    }

    //public int inUse { get; set; }

    // public AltWeapon(float timeBetweenUses, int maxInUse, float knockback, Ammo ammo) {
    //     this.timeBetweenUses = timeBetweenUses;
    //     this.maxInUse = maxInUse;
    //     this.knockback = knockback;
    //     this.ammo = ammo;
    // }
}