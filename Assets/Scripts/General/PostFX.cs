using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostFX : MonoBehaviour
{
    private GameSettings settings;
    public GameObject fx;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        fx.SetActive(settings.useVFX);
    }
}
