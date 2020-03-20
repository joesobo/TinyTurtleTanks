using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public List<Transform> shootPoints;
    private Transform parent;
    public int waitForSeconds = 2;
    public bool doRotate = false;
    public float rotateSpeed = 3;
    private AudioSource source;
    private GameSettings settings;

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();
        parent = GameObject.FindGameObjectWithTag("Planet").transform;
        StartCoroutine("StartShoot");
        source = GetComponent<AudioSource>();
    }

    private void Update() {
        if(doRotate){
            transform.RotateAround(transform.position, transform.up, rotateSpeed * Time.deltaTime * 90f);
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            ShootAtPoints();
            yield return new WaitForSeconds(waitForSeconds);
        }
    }

    private void ShootAtPoints()
    {
        foreach (Transform shootPoint in shootPoints)
        {
            Instantiate(bullet, shootPoint.position, shootPoint.rotation, parent);
            if(settings.useSound){
                source.Play();
            }
        }
    }
}
