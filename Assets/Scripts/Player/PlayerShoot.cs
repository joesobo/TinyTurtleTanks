using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    private Transform parent;
    private bool delayOn = false;

    private GameSettings settings;

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();
        parent = GameObject.FindGameObjectWithTag("Planet").transform;
    }

    private void Update()
    {
        if (!settings.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Return) && !delayOn)
            {
                delayOn = true;
                StartCoroutine("DelayCo");
                Instantiate(bullet, shootPoint.position, this.transform.rotation, parent);
            }
        }
    }

    IEnumerator DelayCo()
    {
        yield return new WaitForSeconds(0.3f);
        delayOn = false;
    }
}
