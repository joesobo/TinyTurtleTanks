using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPickup : Pickup
{
    protected override void applyEffect(Collider col)
    {
        col.gameObject.GetComponent<PlayerEffects>().jump = true;
    }
}
