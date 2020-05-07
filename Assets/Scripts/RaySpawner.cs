using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;

    public int num = 10;
    public int radius = 50;
    public int checkDst = 5;
    public float offsetScale = -0.25f;

    public LayerMask layerMask;

    void Start()
    {
        for (int i = 0; i < num; i++)
        {
            GenerateObject();
        }
    }

    private void GenerateObject()
    {
        //Cast ray in random direction
        RaycastHit hit = new RaycastHit();
        while (hit.collider == null)
        {

            //Generate random point in radius and random direction
            Vector3 point = Random.onUnitSphere * radius;
            Vector3 dir = Random.insideUnitCircle.normalized;
            if (Physics.Raycast(point, dir, out hit, Mathf.Infinity, layerMask))
            {
                //Vector3 pos = hit.point + (Vector3.up * offsetScale);
                GameObject obj = Instantiate(prefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal), parent);
                obj.transform.position += transform.TransformDirection(-obj.transform.up) * offsetScale;
            }
        }
    }
}
