using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundCenter : MonoBehaviour
{
    public float orbitSpeed = 5;
    private GameSettings settings;
    public bool useWind;
    public Vector3 rotateDir;

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();
        float x = Random.Range(0, 1);
        float y = Random.Range(0, Mathf.Max(1-x));
        float z = Random.Range(0, Mathf.Max(0, 1-y-x));
        rotateDir = new Vector3(x, y, z);
    }

    private void FixedUpdate()
    {
        if (!settings.isPaused)
        {
            if (useWind)
            {
                transform.RotateAround(Vector3.zero, Vector3.forward, orbitSpeed * Time.deltaTime);
            }
            else
            {
                transform.RotateAround(Vector3.zero, rotateDir, orbitSpeed * Time.deltaTime);
            }
        }
    }
}
