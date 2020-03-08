using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    private bool pauseActive = false;
    public GameSettings settings;
    private Health playerHealth;
    private bool activate = false;

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
        settings = FindObjectOfType<GameSettings>();
    }

    private void Update() {
        if(pauseActive){
            LeanTween.scale(gameObject, Vector3.one, 0.4f);
            settings.isPaused = true;
        }
    }

    public void ActivateLose()
    {
        pauseActive = true;
        //LeanTween.scale(gameObject, Vector3.one, 0.4f);
    }
}
