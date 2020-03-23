using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    public GameSettings settings;

    public Vector2 footprintSize = new Vector2(1, 1);
    public float waitForSeconds = 2;
    public float offset = 0.1f;
    public LayerMask terrainLayer;
    public GameObject trailPrefab;
    public GameObject footprintParent;
    public Color color;
    [Range(1,30)]
    public int speed = 10;

    private Rigidbody rb;

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();

        if(footprintParent == null){
            footprintParent = GameObject.Find("FootprintParent");
        }

        rb = this.gameObject.GetComponent<Rigidbody>();

        if(settings.useTrails){
            StartCoroutine("StartSpawn");
        }
    }

    IEnumerator StartSpawn()
    {
        AddTrailStep();
        yield return new WaitForSeconds(waitForSeconds);
        StartCoroutine("StartSpawn");
    }

    public void AddTrailStep()
    {
        if (rb.velocity.magnitude > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.position, out hit, .75f + .1f, terrainLayer))
            {
                GameObject footprint = Instantiate(trailPrefab, hit.point, transform.rotation, footprintParent.transform);
                footprint.transform.Rotate(90, 0, 0);
                footprint.transform.localScale = footprintSize;
                footprint.transform.position += hit.normal * offset;

                footprint.GetComponent<Footprint>().color = color;
                footprint.GetComponent<Footprint>().speed = speed;
            }
        }
    }
}
