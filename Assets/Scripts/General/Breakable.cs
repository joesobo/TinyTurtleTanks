using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public List<GameObject> trapList;
    public List<GameObject> pickupList;

    public void Break(){
        if(Random.Range(0,1) == 0){
            int index = Random.Range(0, trapList.Count-1);
            Instantiate(trapList[index], this.transform.position, this.transform.rotation);
        }else{
            int index = Random.Range(0, pickupList.Count-1);
            Instantiate(pickupList[index], this.transform.position, this.transform.rotation);
        }
        Destroy(gameObject);
    }
}
