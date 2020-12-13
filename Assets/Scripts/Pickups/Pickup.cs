using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour {
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            ApplyEffect(col);
        }
    }

    protected abstract void ApplyEffect(Collider col);
}
