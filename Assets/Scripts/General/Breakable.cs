using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public List<GameObject> trapList;
    public List<GameObject> pickupList;

    public void Break(){
        //spawn trap
        if(Random.Range(0,2) == 0){
            int index = Random.Range(0, trapList.Count-1);
            Instantiate(trapList[index], this.transform.position + (3 * this.transform.position.normalized), this.transform.rotation);
        //spawn pickup
        }else{
            int index = Random.Range(0, pickupList.Count-1);
            Instantiate(pickupList[index], this.transform.position + (0.75f * this.transform.position.normalized), this.transform.rotation);
        }
        Destroy(gameObject);
    }
}
