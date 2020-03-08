using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Start() {
        gameObject.transform.localScale = Vector3.zero;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            LeanTween.scale(gameObject, Vector3.one, 0.4f);
        }
    }

    public void OnClose(){
        LeanTween.scale(gameObject, Vector3.zero, 0.4f).setOnComplete(DestroyMe);
    }

    void DestroyMe(){
        Destroy(gameObject);
    }
}
