using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidSettings : ScriptableObject {
    [Header("Boid Settings")]
    [MinTo(0, 10)] public Vector2 minSpeedRange;
    [MinTo(0, 20)] public Vector2 maxSpeedRange;
    private float minSpeed = -1;
    private float maxSpeed = -1;
    public float perceptionRadius = 2.5f;
    public float avoidanceRadius = 1;
    public float maxSteerForce = 3;
    [Range(0.5f, 1f)]
    public float edgeRadiusAvoidance = 0.75f;

    [Header("Weights")]
    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float seperateWeight = 1;
    public float targetWeight = 1;
    public float avoidTargetWeight = 1;
    public float edgeWeight = 1;

    [Header("Area Bounds")]
    [MinTo(0, 200)] public Vector2 spawnRange;
    [MinTo(0, 200)] public Vector2 moveRange;

    [Header("Collisions")]
    public LayerMask obstacleMask;
    public float boundsRadius = 0.27f;
    public float avoidCollisonWeight = 10;
    public float collisionAvoidDist = 5;

    [Header("Debug Rays")]
    public bool obstacleRays = false;
    public bool forwardRays = false;
    public bool alignmentRays = false;
    public bool cohesionRays = false;
    public bool seperationRays = false;

    public void Setup() {
        minSpeed = Random.Range(minSpeedRange.x, minSpeedRange.y);
        maxSpeed = Random.Range(maxSpeedRange.x, maxSpeedRange.y);
    }

    public float GetMinSpeed() {
        if (minSpeed == -1) {
            minSpeed = Random.Range(minSpeedRange.x, minSpeedRange.y);
        }
        return minSpeed;
    }

    public float GetMaxSpeed() {
        if (maxSpeed == -1) {
            maxSpeed = Random.Range(maxSpeedRange.x, maxSpeedRange.y);
        }
        return maxSpeed;
    }
}
