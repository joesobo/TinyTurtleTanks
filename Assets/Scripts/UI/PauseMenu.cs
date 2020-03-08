using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool pauseActive = false;

    private void Start() {
        gameObject.transform.localScale = Vector3.zero;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseActive = !pauseActive;
        }

        if(pauseActive){
            LeanTween.scale(gameObject, Vector3.one, 0.4f);
        }else{
            LeanTween.scale(gameObject, Vector3.zero, 0.4f);
        }
    }

    public void OnClose(){
        LeanTween.scale(gameObject, Vector3.zero, 0.4f);
    }

    void DestroyMe(){
        Destroy(gameObject);
    }
}
