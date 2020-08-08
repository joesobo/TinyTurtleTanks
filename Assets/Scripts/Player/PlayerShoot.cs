using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public Weapon weapon;
    public Transform shootPoint;
    public Transform parent;
    private bool delayOn = false;

    private GameSettings settings;
    private AudioSource source;
    public CameraShake screenShake;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        source = GetComponent<AudioSource>();
    }

    private void Update() {
        if (!settings.isPaused) {
            if (Input.GetKeyDown(KeyCode.Return) && !delayOn) {
                //start delay for next shot
                delayOn = true;
                StartCoroutine("DelayCo");
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
        }
    }

    IEnumerator DelayCo() {
        if (weapon.currentClip > 0) {
            yield return new WaitForSeconds(weapon.timeBetweenShots);
        }else{
            yield return new WaitForSeconds(weapon.reloadTime);
            weapon.reload();
        }
        delayOn = false;
    }
}
