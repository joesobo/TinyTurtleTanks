using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AltWeapon", menuName = "Turtle/AltWeapon")]
public class AltWeapon : ScriptableObject {
    public float timeBetweenUses = 0.5f;
    public int maxInUse = 3;
    public float knockback = 1;
    public Ammo ammo = null;

    // public AltWeapon(float timeBetweenUses, int maxInUse, float knockback, Ammo ammo) {
    //     this.timeBetweenUses = timeBetweenUses;
    //     this.maxInUse = maxInUse;
    //     this.knockback = knockback;
    //     this.ammo = ammo;
    // }
}