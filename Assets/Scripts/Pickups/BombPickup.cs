using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : Pickup {
    public AltWeapon bomb;

    protected override void applyEffect(Collider col) {
        col.gameObject.GetComponent<PlayerShoot>().altWeapon = bomb;
    }
}
