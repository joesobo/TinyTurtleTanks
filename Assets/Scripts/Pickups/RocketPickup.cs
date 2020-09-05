using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPickup : Pickup {
    public Weapon rocket;

    protected override void ApplyEffect(Collider col) {
        PlayerShoot playerShoot = col.gameObject.GetComponent<PlayerShoot>();
        if (playerShoot.newWeapon != rocket) {
            playerShoot.newWeapon = rocket;
            playerShoot.useNewWeapon = true;
            Destroy(transform.parent.gameObject);
        }

    }
}
