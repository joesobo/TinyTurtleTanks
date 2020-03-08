using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    protected override void onDeath()
    {
        FindObjectOfType<LoseMenu>().ActivateLose();
        FindObjectOfType<LevelRunner>().isDead = true;
    }
}
