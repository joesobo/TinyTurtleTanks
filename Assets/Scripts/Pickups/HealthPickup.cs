using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup {
    protected override void ApplyEffect(Collider col) {
        col.gameObject.GetComponent<PlayerEffects>().ActivateHealth();
        Destroy(transform.parent.gameObject);
    }
}
