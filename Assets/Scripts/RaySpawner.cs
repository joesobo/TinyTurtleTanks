using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RaySpawner : MonoBehaviour {
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

    //how many times it tries to spawn an object before failing
    private int maxTries = 60;
    private int currentTries = 0;

    public bool useColliders = true;
    public bool spawnTopDown = false;
    public bool useGizmoRadius = false;
    public bool useGizmoHeight = false;
    public bool useGizmoRayHeight = false;
    public bool useRandomRotation = false;
    public bool useRandomColor = false;
    public bool useSlopeCutoff = false;
    public bool useRandomScaleAxis = false;

    public float minRayHeight = 0;
    public float maxRayHeight = 50;

    public float minHeight = 0;
    public float maxHeight = 50;

    public Color startColor;
    public Color endColor;

    public float slopeCutoff = -0.5f;

    private List<int> objectsIndex = new List<int>();
    private List<GameObject> objects = new List<GameObject>();

    void Start() {
        GenAll();
    }

    public void GenAll() {
        for (int i = 0; i < num; i++) {
            currentTries = 0;
            GenerateObject();
        }

        if (!useColliders) {
            TurnOffColliders();
        }
    }

    private void GenerateObject() {
        //Cast ray in random direction
        RaycastHit hit = new RaycastHit();
        int numberOfCollidersFound = 1;
        while (hit.collider == null && numberOfCollidersFound != 0) {
            if (currentTries < maxTries) {
                currentTries++;

                //Generate starting point and direction
                Vector3 point;
                Vector3 dir;
                if (spawnTopDown) {
                    point = Random.onUnitSphere * radius;
                    dir = -point.normalized;
                }
                else {
                    point = Random.onUnitSphere * Random.Range(minRayHeight, maxRayHeight);
                    dir = Random.insideUnitCircle.normalized;
                }

                //Start raycast
                if (Physics.Raycast(point, dir, out hit, checkDst, layerMask)) {
                    //check max and min height
                    float distFromCenter = Vector3.Distance(hit.point, Vector3.zero);
                    if (distFromCenter > minHeight && distFromCenter < maxHeight) {
                        //check slope
                        Vector3 heading = hit.point - Vector3.zero;
                        float dist = heading.magnitude;
                        Vector3 direction = heading / dist;
                        float slopeResult = Vector3.Dot(direction, hit.normal);
                        if (!useSlopeCutoff || slopeResult >= slopeCutoff) {
                            //find normal rotation based on surface
                            Quaternion normalRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                            //overlap checking
                            Vector3 overlapTestBoxScale = Vector3.one * maxDensity;
                            Collider[] collidersInsideOverlapBox = new Collider[1];
                            numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, normalRotation, objectMask);

                            //if no overlaps
                            if (numberOfCollidersFound == 0) {
                                //spawn object
                                int index = Random.Range(0, prefabs.Count);
                                GameObject obj = Instantiate(prefabs[index], hit.point, normalRotation, parent);
                                objectsIndex.Add(index);

                                //change scale and rotation
                                if (useRandomScaleAxis) {
                                    obj.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
                                }
                                else {
                                    float setScale = Random.Range(minScale, maxScale);
                                    obj.transform.localScale = new Vector3(setScale, setScale, setScale);
                                }

                                if (useRandomRotation) {
                                    obj.transform.Rotate(0, Random.Range(0, 361), 0, Space.Self);
                                }

                                //offset to bring closer to ground
                                obj.transform.position += transform.TransformDirection(-obj.transform.up) * offsetScale * obj.transform.localScale.y;

                                if (useRandomColor) {
                                    MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                                    if (renderer) {
                                        Material tempMat = new Material(renderer.sharedMaterial);
                                        tempMat.color = Color.Lerp(startColor, endColor, Random.value);
                                        renderer.sharedMaterial = tempMat;
                                    }
                                }

                                objects.Add(obj);

                                currentTries = 0;
                            }
                        }
                    }
                }
            }
            else {
                print("Took too long to find a placement");
                break;
            }
        }
    }

    public void Save() {
        using (
            BinaryWriter writer = new BinaryWriter(File.Open(Path.Combine(Application.persistentDataPath, "saveFile"), FileMode.Create))
        ) {
            GameDataWriter gameDataWriter = new GameDataWriter(writer);
            //count
            gameDataWriter.Write(objects.Count);
            //collider
            if (useColliders) {
                gameDataWriter.Write(1);
            }
            else {
                gameDataWriter.Write(0);
            }
            MeshRenderer meshRenderer;
            for (int i = 0; i < objects.Count; i++) {
                GameObject g = objects[i];
                meshRenderer = g.GetComponent<MeshRenderer>();
                //prefab version
                gameDataWriter.Write(objectsIndex[i]);
                gameDataWriter.Write(g.transform.localPosition);
                gameDataWriter.Write(g.transform.localRotation);
                gameDataWriter.Write(g.transform.localScale);
                gameDataWriter.Write(meshRenderer.material.color);
            }
        }
    }

    public void Load() {
        using (
            BinaryReader reader = new BinaryReader(File.Open(Path.Combine(Application.persistentDataPath, "saveFile"), FileMode.Open))
        ) {
            GameDataReader gameDataReader = new GameDataReader(reader);

            int index;
            Vector3 p, s;
            Quaternion r;
            Color c;
            int count = gameDataReader.ReadInt();
            int useCollider = gameDataReader.ReadInt();
            for (int i = 0; i < count; i++) {
                //prefab version
                index = gameDataReader.ReadInt();
                p = gameDataReader.ReadVector3();
                r = gameDataReader.ReadQuaternion();
                s = gameDataReader.ReadVector3();
                c = gameDataReader.ReadColor();

                GameObject obj = Transform.Instantiate(prefabs[index], Vector3.zero, Quaternion.identity, parent);
                obj.transform.localPosition = p;
                obj.transform.rotation = r;
                obj.transform.localScale = s;
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                Material tempMat = new Material(renderer.sharedMaterial);
                tempMat.color = c;
                renderer.sharedMaterial = tempMat;

                objects.Add(obj);
            }
            if (useCollider == 0) {
                TurnOffColliders();
            }
        }
    }

    public void ClearObjects() {
        foreach (GameObject g in objects) {
            DestroyImmediate(g);
        }
        objects.Clear();
        objectsIndex.Clear();
    }

    private void TurnOffColliders() {
        foreach (GameObject g in objects) {
            g.GetComponent<BoxCollider>().size = Vector3.zero;
        }
    }

    void OnDrawGizmosSelected() {
        if (useGizmoHeight) {
            Gizmos.color = new Color(1, 0, 0, 0.25f);
            Gizmos.DrawSphere(Vector3.zero, minHeight);

            Gizmos.color = new Color(0, 0, 1, 0.25f);
            Gizmos.DrawSphere(Vector3.zero, maxHeight);
        }

        if (useGizmoRayHeight) {
            Gizmos.color = new Color(1, 0, 0, 0.25f);
            Gizmos.DrawSphere(Vector3.zero, minRayHeight);

            Gizmos.color = new Color(0, 0, 1, 0.25f);
            Gizmos.DrawSphere(Vector3.zero, maxRayHeight);
        }

        if (useGizmoRadius) {
            Gizmos.color = new Color(0, 1, 0, 0.25f);
            Gizmos.DrawSphere(Vector3.zero, radius);
        }
    }
}