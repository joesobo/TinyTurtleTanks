using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup {
    protected override void applyEffect(Collider col) {
        col.gameObject.GetComponent<PlayerEffects>().ActivateSpeed();
    }
}
