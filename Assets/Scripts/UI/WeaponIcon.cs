using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour {
    private PlayerShoot playerShoot;
    private Image image;

    private Sprite weaponIcon;
    private Sprite altWeaponIcon;

    public bool isAltWeapon = false;

    private void Start() {
        playerShoot = FindObjectOfType<PlayerShoot>();
        image = GetComponent<Image>();
    }

    private void Update() {
        if (playerShoot.weapon && !isAltWeapon) {
            if (weaponIcon != playerShoot.weapon.icon) {
                weaponIcon = playerShoot.weapon.icon;
                assignIcons(weaponIcon, isAltWeapon);
            }
        }

        else if (playerShoot.altWeapon && isAltWeapon) {
            if (altWeaponIcon != playerShoot.altWeapon.icon) {
                altWeaponIcon = playerShoot.altWeapon.icon;
                assignIcons(altWeaponIcon, isAltWeapon);
            }
        }
    }

    private void assignIcons(Sprite sprite, bool isAlt) {
        image.color = Color.white;
        if (isAlt) {
            image.sprite = sprite;
        }
        else {
            image.sprite = sprite;
        }
    }
}
