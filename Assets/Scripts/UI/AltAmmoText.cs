﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltAmmoText : MonoBehaviour {
    private PlayerShoot playerShoot;
    private Text text;

    private void Start() {
        playerShoot = FindObjectOfType<PlayerShoot>();
        text = transform.GetComponent<Text>();
    }

    private void Update() {
        text.text = "x" + (playerShoot.altWeapon.maxInPlay - playerShoot.altWeapon.inPlay);
    }
}