using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrap : MonoBehaviour {
    public GameObject spikes = null;
    public int moveSpeed = 5;
    private bool moveSpike = false;
    private float maxSpike = -0.25f;

    private void Update() {
        AnimateSpikes();
    }

    private void AnimateSpikes() {
        if (moveSpike && spikes.transform.localPosition.y < maxSpike) {
            spikes.transform.localPosition = new Vector3(
                spikes.transform.localPosition.x,
                spikes.transform.localPosition.y + (moveSpeed * Time.deltaTime),
                spikes.transform.localPosition.z);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            moveSpike = true;
        }
    }

    public void Delete() {
        Destroy(gameObject);
    }
}
