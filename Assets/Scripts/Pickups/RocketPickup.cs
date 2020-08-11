using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPickup : Pickup {
    public Weapon rocket;

    protected override void applyEffect(Collider col) {
        PlayerShoot playerShoot = col.gameObject.GetComponent<PlayerShoot>();

        playerShoot.newWeapon = rocket;
        playerShoot.useNewWeapon = true;
    }
}
