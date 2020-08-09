using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public Weapon weapon;
    public AltWeapon altWeapon;
    public Transform shootPoint;
    public Transform parent;
    public Transform altParent;
    private bool mainDelayOn = false;
    private bool altDelayOn = false;

    private GameSettings settings;
    private AudioSource source;
    public CameraShake screenShake;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        source = GetComponent<AudioSource>();
    }

    private void Update() {
        if (!settings.isPaused) {
            if (Input.GetMouseButtonDown(0) && !mainDelayOn) {
                //start delay for next shot
                mainDelayOn = true;
                StartCoroutine("MainDelayCo");
                //create bullet
                weapon.shoot(shootPoint.position, this.transform.rotation, parent);
                //play sound
                if (settings.useSound) {
                    source.volume = settings.soundVolume;
                    source.Play();
                }
                //apply screen shake
                StartCoroutine(screenShake.Shake());
            }

            if (Input.GetMouseButtonDown(1) && !altDelayOn) {
                //start delay for next shot
                altDelayOn = true;
                StartCoroutine("AltDelayCo");
                //create bullet
                altWeapon.shoot(shootPoint.position, this.transform.rotation, altParent);
                //play sound
                // if (settings.useSound) {
                //     source.volume = settings.soundVolume;
                //     source.Play();
                // }
                //apply screen shake
                StartCoroutine(screenShake.Shake());
            }
        }
    }

    IEnumerator MainDelayCo() {
        if (weapon.currentClip > 0) {
            yield return new WaitForSeconds(weapon.timeBetweenShots);
        }else{
            yield return new WaitForSeconds(weapon.reloadTime);
            weapon.reload();
        }
        mainDelayOn = false;
    }

    IEnumerator AltDelayCo() {
        yield return new WaitForSeconds(altWeapon.timeBetweenUses);
        altDelayOn = false;
    }
}
