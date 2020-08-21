using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPickup : Pickup {
    protected override void ApplyEffect(Collider col) {
        col.gameObject.GetComponent<PlayerEffects>().ActivateJump();
    }
}
