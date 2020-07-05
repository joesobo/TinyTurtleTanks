using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {
    protected override void onDeath() {
        if (settings.useParticle) {
            Instantiate(deathParticles, transform.position, transform.rotation);
        }
        FindObjectOfType<LevelRunner>().isDead = true;
    }
}
