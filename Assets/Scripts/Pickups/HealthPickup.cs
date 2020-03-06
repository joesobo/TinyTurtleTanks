using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    protected override void applyEffect(Collider col)
    {
        col.gameObject.GetComponent<Health>().increaseHealth(1);
    }
}
