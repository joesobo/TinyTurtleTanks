using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Material bulletMat;
    public Transform shootPoint;
    public Transform parent;
    private bool delayOn = false;

    private GameSettings settings;
    private AudioSource source;
    public CameraShake screenShake;

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();
        source = GetComponent<AudioSource>();
        bullet.GetComponent<MeshRenderer>().material = bulletMat;
    }

    private void Update()
    {
        if (!settings.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Return) && !delayOn)
            {
                //start delay for next shot
                delayOn = true;
                StartCoroutine("DelayCo");
                //create bullet
                Instantiate(bullet, shootPoint.position, this.transform.rotation, parent);
                //play sound
                if (settings.useSound)
                {
                    source.Play();
                }
                //apply screen shake
                StartCoroutine(screenShake.Shake());
            }
        }
    }

    IEnumerator DelayCo()
    {
        yield return new WaitForSeconds(0.3f);
        delayOn = false;
    }
}
