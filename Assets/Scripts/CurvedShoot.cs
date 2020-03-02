using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedShoot : MonoBehaviour
{
    public Rigidbody rb;
    public Transform shootPos;
    public Transform target;

    public int resolution = 30;
    [Range(0,0.1f)]
    public float dropY = 5;

    public float h = 25;
    public float gravity = -18;

    public bool debugPath;

    private void Start() {
        rb.useGravity = false;
    }

    private void Update() {
        // if(Input.GetKeyDown(KeyCode.T)){
        //     Launch();
        // }
        // if(debugPath){
        //     DrawPath();
        // }
        DrawCurvePath();
    }

    void Launch(){
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        rb.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData(){
        float displacementY = target.position.y - shootPos.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - shootPos.position.x, 0, target.position.z - shootPos.position.z);
        float time = (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY-h)/gravity));
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath(){
        LaunchData launchData = CalculateLaunchData();

        Vector3 previousDrawPoint = shootPos.position;

        int resolution = 30;
        for(int i = 1; i <= resolution; i++){
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = shootPos.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    void DrawCurvePath(){
        Vector3 previousDrawPoint = shootPos.position;
        Vector3 nextDrawPoint = previousDrawPoint + this.transform.forward;

        for(int i = 0; i < resolution; i++){
            Debug.DrawLine(previousDrawPoint, nextDrawPoint, Color.green);
            previousDrawPoint = nextDrawPoint;
            nextDrawPoint = previousDrawPoint + this.transform.forward;
            nextDrawPoint.y -= dropY * i * i;
        }
    }

    struct LaunchData {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
