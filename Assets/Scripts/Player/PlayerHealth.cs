using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    protected override void onDeath()
    {
        FindObjectOfType<LevelRunner>().isDead = true;
    }
}
