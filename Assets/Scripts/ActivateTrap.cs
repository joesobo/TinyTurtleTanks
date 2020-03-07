using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrap : MonoBehaviour
{
    public GameObject spikes;
    public int moveSpeed = 5;
    private bool moveSpike = false;
    private int maxSpike = 0;

    private void Update() {
        if(moveSpike){
            if(spikes.transform.localPosition.y < maxSpike){
                spikes.transform.localPosition = new Vector3(
                    spikes.transform.localPosition.x, 
                    spikes.transform.localPosition.y + (moveSpeed * Time.deltaTime), 
                    spikes.transform.localPosition.z);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player activated trap");
            moveSpike = true;
        }
    }

    public void Delete(){
        Destroy(gameObject);
    }
}
