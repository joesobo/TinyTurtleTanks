using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -18;
    private GameSettings settings;

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();
    }

    public void Attract(GravityBody body)
    {
        Vector3 targetDir = (body.transform.position - transform.position).normalized;
        Vector3 bodyUp = body.transform.up;

        body.transform.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.transform.rotation;
        if (!settings.isPaused)
        {
            if (body.useGrav)
            {
                body.getRb().AddForce(targetDir * gravity);
            }
        }
    }
}
