using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoidManager : MonoBehaviour {
    //private GameSettings gameSettings;

    public enum GizmoType { Never, SelectedOnly, Always }

    public BoidSettings settings;
    private List<Boid> boids = new List<Boid>();

    private float viewRadius;
    private float avoidRadius;

    public GizmoType showSpawnRegion;
    private Vector3 boundSize;
    private Vector3 center;

    public Transform target;
    public Transform avoidTarget;

    public int totalBoids = 0;
    private BoidSpawner boidSpawner;

    private void Start() {
        //gameSettings = FindObjectOfType<GameSettings>();
        boidSpawner = FindObjectOfType<BoidSpawner>();
        boidSpawner.StartSpawner(settings);
        boids = FindObjectsOfType<Boid>().ToList();
        foreach (Boid b in boids) {
            b.Initialize(target, avoidTarget, settings);
            totalBoids++;
        }

        boundSize = settings.boundSize;
        center = settings.center;
        viewRadius = settings.perceptionRadius;
        avoidRadius = settings.avoidanceRadius;
    }

    private void Update() {
        if (boids != null) {
            foreach (Boid boid in boids) {
                List<Boid> neighborList = GetNeighbors(boid);

                BoidBehavior(boid, neighborList);

                boid.UpdateBoid();
            }
        }
    }

    public int GetFlockSize() {
        return boids.Count;
    }

    public void RemoveBoid(Boid boid) {
        boids.Remove(boid);
    }

    private void BoidBehavior(Boid curBoid, List<Boid> neighborList) {
        curBoid.numPerceivedFlockmates = neighborList.Count;

        for (int i = 0; i < neighborList.Count; i++) {
            if (curBoid != neighborList[i]) {
                Boid newBoid = neighborList[i];
                Vector3 offset = newBoid.position - curBoid.position;
                float sqrDist = offset.x * offset.x + offset.y * offset.y + offset.z * offset.z;

                curBoid.avgFlockHeading += newBoid.forward;
                curBoid.centerOfFlockmates += newBoid.position;

                if (sqrDist < avoidRadius * avoidRadius) {
                    curBoid.avgAvoidanceHeading -= offset / sqrDist;
                }
            }

            if (curBoid.numPerceivedFlockmates != 0) {
                curBoid.avgFlockHeading /= curBoid.numPerceivedFlockmates;
                curBoid.centerOfFlockmates /= curBoid.numPerceivedFlockmates;
                curBoid.avgAvoidanceHeading /= curBoid.numPerceivedFlockmates;
            }
        }
    }

    private List<Boid> GetNeighbors(Boid curBoid) {
        List<Boid> allBoidList = boids.ToList();
        List<Boid> neighborList = new List<Boid>();
        Collider[] contextColliders = Physics.OverlapSphere(curBoid.position, settings.perceptionRadius);

        Boid newBoid;
        foreach (Collider c in contextColliders) {
            newBoid = c.transform.gameObject.GetComponentInParent<Boid>();

            //check object isn't current
            if (newBoid != curBoid) {
                //check object is a boid and in this flock
                if (allBoidList.Contains(newBoid)) {
                    neighborList.Add(newBoid);
                }
            }
        }

        return neighborList;
    }

    private void OnDrawGizmos() {
        if (showSpawnRegion == GizmoType.Always) {
            DrawGizmos();
        }
    }

    private void OnDrawGizmosSelected() {
        if (showSpawnRegion == GizmoType.SelectedOnly) {
            DrawGizmos();
        }
    }

    private void DrawGizmos() {
        Gizmos.color = new Color(0, 0.5f, 1, 0.3f);
        Gizmos.DrawCube(center, boundSize);
    }

    public void CreateBoid(Vector3 pos) {
        AddBoidToList(boidSpawner.CreateBoid(pos));
    }

    public void AddBoidToList(Boid boid) {
        boid.Initialize(target, avoidTarget, settings);
        boids.Add(boid);
    }
}
