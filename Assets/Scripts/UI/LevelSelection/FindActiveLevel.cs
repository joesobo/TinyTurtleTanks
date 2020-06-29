using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindActiveLevel : MonoBehaviour
{
    private GameSettings settings;
    public List<GameObject> worldPrefabs;
    private GameObject childWorld;

    void OnEnable()
    {
        settings = FindObjectOfType<GameSettings>();
        int activeWorld = settings.currentLevel-1;
        if(childWorld != null){
            Destroy(childWorld);
        }
        childWorld = Instantiate(worldPrefabs[activeWorld], Vector3.zero, Quaternion.identity, this.transform);
        childWorld.transform.localPosition = Vector3.zero;
        childWorld.transform.localScale = Vector3.one * 30;
    }
}
