using UnityEngine;

public class PlayerSoundManager : MonoBehaviour {
    public enum Clip { jump, shootMain, shootAlt, powerup, land, splash };

    public AudioClip jumpAudioClip;
    public AudioClip shootMainAudioClip;
    public AudioClip shootAltAudioClip;
    public AudioClip powerupClip;
    public AudioClip landClip;
    public AudioClip splashClip;

    private AudioSource source;
    private GameSettings settings;

    private void Awake() {
        source = GetComponent<AudioSource>();
        settings = FindObjectOfType<GameSettings>();

        UpdateSettings();
    }

    public void UpdateSettings() {
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
                case Clip.land:
                    source.PlayOneShot(landClip);
                    break;
                case Clip.splash:
                    source.PlayOneShot(splashClip);
                    break;
                default:
                    break;
            }
        }
    }
}
