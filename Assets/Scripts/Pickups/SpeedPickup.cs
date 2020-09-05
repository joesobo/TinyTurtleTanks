using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup {
    protected override void ApplyEffect(Collider col) {
        PlayerEffects playerEffects = col.gameObject.GetComponent<PlayerEffects>();
        if (!playerEffects.shield && !playerEffects.speed && !playerEffects.jump) {
            playerEffects.ActivateSpeed();
            Destroy(transform.parent.gameObject);
        }
    }
}
