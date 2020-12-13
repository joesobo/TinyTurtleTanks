using UnityEngine;

public class EnemySoundManager : MonoBehaviour {
    public enum Clip { jump, shootMain, shootAlt };

    public AudioClip jumpAudioClip;
    public AudioClip shootMainAudioClip;
    public AudioClip shootAltAudioClip;

    private AudioSource source;
    private GameSettings settings;

    private void Awake() {
        source = GetComponent<AudioSource>();
        settings = FindObjectOfType<GameSettings>();

        UpdateSettings();
    }

    public void UpdateSettings() {
        if (source) {
            source.volume = settings.soundVolume;
        }
    }

    public void Play(Clip audioClip) {
        if (settings.useSound) {
            switch (audioClip) {
                case Clip.jump:
                    if (!source.isPlaying) {
                        source.PlayOneShot(jumpAudioClip);
                    }
                    break;
                case Clip.shootMain:
                    source.PlayOneShot(shootMainAudioClip);
                    break;
                case Clip.shootAlt:
                    source.PlayOneShot(shootAltAudioClip);
                    break;
                default:
                    break;
            }
        }
    }
}
