using UnityEngine;

public class PlayerSoundManager : MonoBehaviour {
    public enum Clip { jump, shootMain, shootAlt, powerup };

    public AudioClip jumpAudioClip;
    public AudioClip shootMainAudioClip;
    public AudioClip shootAltAudioClip;
    public AudioClip powerupClip;

    private AudioSource source;
    private GameSettings settings;

    private void Awake() {
        source = GetComponent<AudioSource>();
        settings = FindObjectOfType<GameSettings>();

        UpdateSettings();
    }

    private void UpdateSettings() {
        source.volume = settings.soundVolume;
    }

    public void Play(Clip audioClip) {
        if (settings.useSound) {
            switch (audioClip) {
                case Clip.jump:
                    source.PlayOneShot(jumpAudioClip);
                    break;
                case Clip.shootMain:
                    source.PlayOneShot(shootMainAudioClip);
                    break;
                case Clip.shootAlt:
                    source.PlayOneShot(shootAltAudioClip);
                    break;
                case Clip.powerup:
                    source.PlayOneShot(powerupClip);
                    break;
                default:
                    break;
            }
        }
    }
}
