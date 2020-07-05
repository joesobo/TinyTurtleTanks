using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrap : MonoBehaviour {
    public GameObject spikes = null;
    public int moveSpeed = 5;
    private bool moveSpike = false;
    private float maxSpike = -0.25f;
    private bool blowUp = false;
    public ParticleSystem explosion;

    private GameSettings settings;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
    }

    private void Update() {
        if (!settings.isPaused) {
            //check if spikes
            if (spikes != null) {
                if (moveSpike) {
                    if (spikes.transform.localPosition.y < maxSpike) {
                        spikes.transform.localPosition = new Vector3(
                            spikes.transform.localPosition.x,
                            spikes.transform.localPosition.y + (moveSpeed * Time.deltaTime),
                            spikes.transform.localPosition.z);
                    }
                }
            }
            //otherwise its a mine
            else {
                if (blowUp) {
                    if (settings.useParticle) {

                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            Debug.Log("Player activated trap");
            moveSpike = true;
            blowUp = true;
        }
    }

    public void Delete() {
        Destroy(gameObject);
    }
}
