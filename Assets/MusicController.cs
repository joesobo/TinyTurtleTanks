using UnityEngine;

public class MusicController : MonoBehaviour {
    private AudioSource source;
    private GameSettings settings;

    private void Awake() {
        source = GetComponent<AudioSource>();
        settings = FindObjectOfType<GameSettings>();

        UpdateSettings();
    }

    public void UpdateSettings() {
        source.volume = settings.musicVolume;
    }
}
