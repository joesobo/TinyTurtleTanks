using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAdjuster : MonoBehaviour {
    private AudioSource source;
    private GameSettings settings;

    void Start() {
        source = GetComponent<AudioSource>();
        settings = FindObjectOfType<GameSettings>();

        source.volume = settings.soundVolume;

        if (!settings.useSound) {
            source.volume = 0;
        }
    }
}
