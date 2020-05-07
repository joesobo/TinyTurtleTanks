using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Transform parent;

    public int num = 10;
    public int radius = 50;
    public int checkDst = 5;
    public float offsetScale = -0.25f;
    [Range(0, 10)]
    public float maxDensity = 1;

    public float minScale = 0.5f;
    public float maxScale = 2;

    public LayerMask layerMask;
    public LayerMask objectMask;

    public int maxTries = 10;
    private int currentTries = 0;

    public bool keepColliders = true;
    public bool spawnTopDown = false;
    public bool useGizmos = false;
    public bool useRandomRotation = false;
    public bool useRandomColor = false;

    public float minRayHeight = 0;
    public float maxRayHeight = 50;

    public float minHeight = 0;
    public float maxHeight = 50;

    public Color startColor;
    public Color endColor;

    void Start()
    {
        ClearObjects();
        for (int i = 0; i < num; i++)
        {
            GenerateObject();
        }

        if (!keepColliders)
        {
            TurnOffColliders();
        }
    }

    private void GenerateObject()
    {
        //Cast ray in random direction
        RaycastHit hit = new RaycastHit();
        int numberOfCollidersFound = 1;
        while (hit.collider == null && numberOfCollidersFound != 0)
        {
            if (currentTries < maxTries)
            {
                currentTries++;

                //Generate starting point and direction
                Vector3 point;
                Vector3 dir;
                if (spawnTopDown)
                {
                    point = Random.onUnitSphere * radius;
                    dir = -point.normalized;
                }
                else
                {
                    point = Random.onUnitSphere * Random.Range(minRayHeight, maxRayHeight);
                    dir = Random.insideUnitCircle.normalized;
                }

                //Start raycast
                if (Physics.Raycast(point, dir, out hit, checkDst, layerMask))
                {
                    //check max and min height
                    float distFromCenter = Vector3.Distance(hit.point, Vector3.zero);
                    if (distFromCenter > minHeight && distFromCenter < maxHeight)
                    {
                        //find normal rotation based on surface
                        Quaternion normalRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                        //overlap checking
                        Vector3 overlapTestBoxScale = Vector3.one * maxDensity;
                        Collider[] collidersInsideOverlapBox = new Collider[1];
                        numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, normalRotation, objectMask);

                        //if no overlaps
                        if (numberOfCollidersFound == 0)
                        {
                            //spawn object
                            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)], hit.point, normalRotation, parent);

                            //change scale and rotation
                            obj.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
                            if (useRandomRotation)
                            {
                                obj.transform.Rotate(0, Random.Range(0, 361), 0, Space.Self);
                            }

                            //offset to bring closer to ground
                            obj.transform.position += transform.TransformDirection(-obj.transform.up) * offsetScale * obj.transform.localScale.y;

                            if (useRandomColor)
                            {
                                obj.GetComponent<MeshRenderer>().material.color = Color.Lerp(startColor, endColor, Random.value);
                            }

                            currentTries = 0;
                        }
                    }
                }
            }
            else
            {
                print("Took too long to find a placement");
                break;
            }
        }
    }

    private void ClearObjects()
    {
        foreach (Transform child in parent.transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    private void TurnOffColliders()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject.GetComponent<BoxCollider>());
        }
    }

    void OnDrawGizmosSelected()
    {
        if (useGizmos)
        {
            Gizmos.color = new Color(1, 0, 0, 0.25f);
            Gizmos.DrawSphere(Vector3.zero, minHeight);

            Gizmos.color = new Color(0, 0, 1, 0.25f);
            Gizmos.DrawSphere(Vector3.zero, maxHeight);
        }
    }
}