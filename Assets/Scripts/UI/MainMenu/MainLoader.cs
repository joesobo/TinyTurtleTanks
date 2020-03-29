using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLoader : MonoBehaviour
{
    public List<GameObject> mainObjects;

    public void LoadMain(){
        LoadObjects(true);
    }

    public void UnloadMain(){
        LoadObjects(false);
    }

    private void LoadObjects(bool active){
        foreach (GameObject obj in mainObjects)
        {
            obj.SetActive(active);
        }
    }
}
