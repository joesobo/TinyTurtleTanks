using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPickup : Pickup {
    public Weapon rocket;

    protected override void applyEffect(Collider col) {
        col.gameObject.GetComponent<PlayerShoot>().weapon = rocket;
    }
}
