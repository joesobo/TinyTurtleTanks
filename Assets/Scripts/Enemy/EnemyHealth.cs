using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected override void onDeath()
    {
        FindObjectOfType<LevelRunner>().DecreaseNumEnemy();
        Destroy(gameObject);
    }
}
