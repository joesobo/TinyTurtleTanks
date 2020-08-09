using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleKiller : MonoBehaviour {
    ParticleSystem ps;
    bool stopped = false;

    private void Start() {
        ps = this.GetComponent<ParticleSystem>();
    }

    void Update() {
        if (ps.particleCount == ps.main.maxParticles) {
            stopped = true;
        }

        if (stopped) {
            ps.Stop();
        }
    }
}
