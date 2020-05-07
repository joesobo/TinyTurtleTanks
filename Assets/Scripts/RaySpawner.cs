using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clean up inspector
//Add random color range
//Add random angle tilt
//Make it update in edit mode
//Add edge detection
//Add cluster spawning
//Prevent overlapping objects
[ExecuteInEditMode]
public class RaySpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Transform parent;

    public int num = 10;
    public int radius = 50;
    public int checkDst = 5;
    public float offsetScale = -0.25f;

    public float minScale = 0.5f;
    public float maxScale = 2;

    public LayerMask layerMask;

    void Start()
    {
        ClearObjects();
    }

    void Update()
    {
        ClearObjects();
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
            if (Physics.Raycast(point, dir, out hit, checkDst, layerMask))
            {
                GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal), parent);
                obj.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
                obj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Random.Range(0,361), transform.eulerAngles.z);
                //offset to bring closer to ground
                obj.transform.position += transform.TransformDirection(-obj.transform.up) * offsetScale * obj.transform.localScale.y;
            }
        }
    }

    private void ClearObjects(){
        foreach (Transform child in parent.transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
