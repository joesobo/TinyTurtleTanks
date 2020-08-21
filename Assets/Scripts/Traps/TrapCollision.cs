using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollision : MonoBehaviour {
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            Debug.Log("Trap hit player");
            col.gameObject.GetComponent<Health>().DecreaseHealth(1);
            gameObject.transform.parent.GetComponent<ActivateTrap>().Delete();
        }
    }
}
