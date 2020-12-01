using UnityEngine;

public class ParticleController : MonoBehaviour {
    private ParticleSystem particle;
    private GameSettings settings;

    private void Awake() {
        particle = GetComponent<ParticleSystem>();
        settings = FindObjectOfType<GameSettings>();

        UpdateSettings();
    }

    public void UpdateSettings() {
        settings.SetParticleValues(particle);
    }
}
