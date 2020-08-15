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
        if (weaponIcon != playerShoot.weapon.icon || altWeaponIcon != playerShoot.altWeapon.icon) {
            assignIcons();
        }
    }

    private void assignIcons() {
        weaponIcon = playerShoot.weapon.icon;
        altWeaponIcon = playerShoot.altWeapon.icon;

        if (isAltWeapon) {
            image.sprite = altWeaponIcon;
        }
        else {
            image.sprite = weaponIcon;
        }
    }
}
