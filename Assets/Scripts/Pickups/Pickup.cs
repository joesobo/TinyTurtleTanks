using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player got pickup");
            applyEffect(col);
            col.gameObject.GetComponent<PlayerEffects>().col = col;

            Destroy(transform.parent.gameObject);
        }
    }

    protected abstract void applyEffect(Collider col);
}
