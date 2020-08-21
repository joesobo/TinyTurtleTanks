using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : Pickup {
    public AltWeapon bomb;

    protected override void ApplyEffect(Collider col) {
        PlayerShoot playerShoot = col.gameObject.GetComponent<PlayerShoot>();

        playerShoot.newAltWeapon = bomb;
        playerShoot.useNewAlt = true;
    }
}
