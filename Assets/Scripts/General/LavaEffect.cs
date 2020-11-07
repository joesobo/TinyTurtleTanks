using UnityEngine;

public class LavaEffect : MonoBehaviour {
    private PlayerEffects playerEffects;
    private Transform player;

    void Start() {
        playerEffects = FindObjectOfType<PlayerEffects>();
        player = playerEffects.transform;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !playerEffects.fire) {
            playerEffects.ActivateFire();
        }
    }
}
