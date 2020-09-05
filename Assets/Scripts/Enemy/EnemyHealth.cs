using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {
    protected override void OnDeath() {
        Explosion explosion = new Explosion(settings, deathParticles, bloodParticles);
        explosion.PlayExplosion(transform.position, transform.rotation);

        transform.GetChild(0).gameObject.SetActive(false);

        FindObjectOfType<LevelRunner>().DecreaseNumEnemy();

        StartCoroutine("KillObject");
    }

    IEnumerator KillObject() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
