using System;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    private BoidSettings settings;

    //State
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    private Vector3 velocity;

    //To update
    private Vector3 acceleration;
    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centerOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    //Cached
    MeshRenderer boidMat;
    Transform cachedTransform;
    Transform target;
    Transform avoidTarget;

    private void Awake() {
        boidMat = transform.GetComponentInChildren<MeshRenderer>();
        cachedTransform = transform;
    }

    public void Initialize(Transform target, Transform avoidTarget, BoidSettings settings) {
        this.settings = settings;

        settings.Setup();

        this.target = target;
        this.avoidTarget = avoidTarget;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = (settings.GetMinSpeed() + settings.GetMaxSpeed()) / 2;
        velocity = transform.forward * startSpeed;
    }

    public void SetColor(Color col) {
        if (boidMat != null) {
            boidMat.material.color = col;
        }
    }

    public void UpdateBoid() {
        Vector3 acceleration = Vector3.zero;

        //Avoid Target Behavior
        if (avoidTarget != null) {
            Vector3 offsetToTarget = (avoidTarget.position - position);
            if (offsetToTarget.magnitude < settings.avoidanceRadius) {
                acceleration += SteerTowards(-offsetToTarget) * settings.avoidTargetWeight;
            }
        }

        //Target Behavior
        if (target != null) {
            Vector3 offsetToTarget = (target.position - position);
            acceleration += SteerTowards(offsetToTarget) * settings.targetWeight;
        }

        //Normal Behavior
        if (numPerceivedFlockmates != 0) {
            centerOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCenter = (centerOfFlockmates - position);

            Vector3 alignmentForce = SteerTowards(avgFlockHeading) * settings.alignWeight;

            Vector3 cohesionForce = SteerTowards(offsetToFlockmatesCenter) * settings.cohesionWeight;

            Vector3 seperationForce = SteerTowards(avgAvoidanceHeading) * settings.seperateWeight;

            //debug rays
            DebugBehaviorRays(alignmentForce, cohesionForce, seperationForce);

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        //Collision Behavior
        if (IsHeadingForCollision()) {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settings.avoidCollisonWeight;
            acceleration += collisionAvoidForce;
        }

        //Edge Behavior
        //acceleration += EdgeDetection();

        //update values
        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, settings.GetMinSpeed(), settings.GetMaxSpeed());
        velocity = dir * speed;

        //update positions
        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = dir;

        transform.position = cachedTransform.position;
        transform.forward = dir;

        position = cachedTransform.position;
        forward = dir;
    }

    private void DebugBehaviorRays(Vector3 alignmentForce, Vector3 cohesionForce, Vector3 seperationForce) {
        if (settings.forwardRays) {
            Debug.DrawRay(position, forward, Color.green);
        }
        if (settings.alignmentRays) {
            Debug.DrawRay(position, alignmentForce, Color.yellow);
        }
        if (settings.cohesionRays) {
            Debug.DrawRay(position, cohesionForce, Color.blue);
        }
        if (settings.seperationRays) {
            Debug.DrawRay(position, seperationForce, Color.red);
        }
    }

    private Vector3 EdgeDetection() {
        Vector3 edgeAvoidForce;
        Vector3 centerOffset = settings.center - this.transform.position;
        float t = centerOffset.magnitude / (settings.boundSize.x / 2);

        //within majority of radius so no influence
        if (t < settings.edgeRadiusAvoidance) {
            edgeAvoidForce = Vector3.zero;
        }
        else {
            edgeAvoidForce = centerOffset * t * t;
        }

        return edgeAvoidForce * settings.edgeWeight;
    }

    private Vector3 SteerTowards(Vector3 vector) {
        Vector3 v = vector.normalized * settings.GetMaxSpeed() - velocity;
        return Vector3.ClampMagnitude(v, UnityEngine.Random.Range(settings.maxSteerForce - 0.5f, settings.maxSteerForce + 0.5f));
    }

    private bool IsHeadingForCollision() {
        RaycastHit hit;
        if (Physics.SphereCast(position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDist, settings.obstacleMask)) {
            if (settings.obstacleRays) {
                Debug.DrawRay(position, forward * settings.boundsRadius, Color.red, 1, true);
            }
            return true;
        }
        return false;
    }

    private Vector3 ObstacleRays() {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++) {
            Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if (!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDist, settings.obstacleMask)) {
                if (settings.obstacleRays) {
                    Debug.DrawRay(position, dir * settings.boundsRadius, Color.green, 1, true);
                }

                return dir;
            }
        }
        return forward;
    }
}
