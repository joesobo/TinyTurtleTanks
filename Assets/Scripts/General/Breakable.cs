using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
    public List<GameObject> trapList;
    public List<GameObject> pickupList;
    private BreakableSpawner breakableSpawner;

    void Start() {
        breakableSpawner = FindObjectOfType<BreakableSpawner>();
    }

    public void Break() {
        //spawn trap
        if (Random.Range(0, 3) == 0) {
            int index = Random.Range(0, trapList.Count);
            Instantiate(trapList[index], this.transform.position + (3 * this.transform.position.normalized), this.transform.rotation);
            //spawn pickup
        }
        else {
            int index = Random.Range(0, pickupList.Count);
            Instantiate(pickupList[index], this.transform.position + (0.75f * this.transform.position.normalized), this.transform.rotation);
        }
        breakableSpawner.totalCrates--;
        Destroy(gameObject);
    }
}
