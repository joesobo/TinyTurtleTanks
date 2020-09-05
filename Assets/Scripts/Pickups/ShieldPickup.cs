using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : Pickup {
    protected override void ApplyEffect(Collider col) {
        PlayerEffects playerEffects = col.gameObject.GetComponent<PlayerEffects>();
        if (!playerEffects.shield && !playerEffects.speed && !playerEffects.jump) {
            playerEffects.ActivateShield();
            Destroy(transform.parent.gameObject);
        }
    }
}
