using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Turtle/Weapon")]
public class Weapon : ScriptableObject {
    public int clipSize = 1;                                // 1-5
    public float reloadTime = 0.5f;
    public float timeBetweenShots = 0.1f;
    public int directions = 1;                              // 1-8
    public Ammo ammo = null;

    public int currentClip { get; set; }

    public void useAmmo(int amount) {
        currentClip -= amount;
        if(currentClip < 0) {
            currentClip = 0;
        }
    }

    public void reload() {
        currentClip = clipSize;
    }

    public void shoot(Vector3 position, Quaternion rotation, Transform parent) {
        ammo.StartUpBullet(Instantiate(ammo.prefab, position, rotation, parent));
        useAmmo(1);
    }
}