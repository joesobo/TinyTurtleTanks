using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public Weapon weapon;
    [HideInInspector]
    public Weapon newWeapon = null;
    [HideInInspector]
    private Weapon tempWeapon = null;
    public bool useNewWeapon = false;
    private int waitForSecondsWeapon = 5;

    public AltWeapon altWeapon;
    [HideInInspector]
    public AltWeapon newAltWeapon = null;
    [HideInInspector]
    private AltWeapon tempAltWeapon = null;
    public bool useNewAlt = false;
    private int waitForSecondsAlt = 5;

    public Transform shootPoint;
    public Transform dropPoint;
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
        altWeapon.inPlay = 0;
    }

    private void Update() {
        //Temp switch weapons
        if (useNewWeapon) {
            useNewWeapon = false;
            StartCoroutine("UseNewWeapon");
        }

        if (useNewAlt) {
            useNewAlt = false;
            StartCoroutine("UseNewAltWeapon");
        }

        //Shooting
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

            if (Input.GetMouseButtonDown(1) && !altDelayOn && altWeapon.inPlay < altWeapon.maxInPlay) {
                //start delay for next shot
                altDelayOn = true;
                StartCoroutine("AltDelayCo");
                //create bullet
                altWeapon.shoot(dropPoint.position, this.transform.rotation, altParent);
                altWeapon.inPlay++;
                //play sound
                if (settings.useSound) {
                    // source.volume = settings.soundVolume;
                    // source.Play();
                }
                //apply screen shake
                StartCoroutine(screenShake.Shake());
            }
        }
    }

    //Reload and delay shooting
    IEnumerator MainDelayCo() {
        if (weapon.currentClip-1 > 0) {
            yield return new WaitForSeconds(weapon.timeBetweenShots);
        }else{
            yield return new WaitForSeconds(weapon.reloadTime);
            weapon.reload();
        }
        mainDelayOn = false;
    }

    //Delay shooting
    IEnumerator AltDelayCo() {
        yield return new WaitForSeconds(altWeapon.timeBetweenUses);
        altDelayOn = false;
    }

    IEnumerator UseNewWeapon() {
        tempWeapon = weapon;
        weapon = newWeapon;
        yield return new WaitForSeconds(waitForSecondsWeapon);
        weapon = tempWeapon;
        newWeapon = null;
        tempWeapon = null;
    }

    IEnumerator UseNewAltWeapon() {
        tempAltWeapon = altWeapon;
        altWeapon = newAltWeapon;
        yield return new WaitForSeconds(waitForSecondsAlt);
        altWeapon = tempAltWeapon;
        newAltWeapon = null;
        tempAltWeapon = null;
    }
}
