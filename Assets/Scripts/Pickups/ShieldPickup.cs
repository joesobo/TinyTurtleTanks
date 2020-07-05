using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : Pickup {
    protected override void applyEffect(Collider col) {
        col.gameObject.GetComponent<PlayerEffects>().shield = true;
    }
}
