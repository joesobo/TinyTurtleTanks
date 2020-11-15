using UnityEngine;

public class SettingLoader : MonoBehaviour {
    private GameSettings settings;
    public UnityEngine.UI.Image vfxButton;
    public UnityEngine.UI.Image soundButton;
    public UnityEngine.UI.Slider soundSlider;
    public UnityEngine.UI.Image musicButton;
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Image particleButton;
    public UnityEngine.UI.Slider particleSlider;
    public UnityEngine.UI.Image grassButton;
    public UnityEngine.UI.Image footPrintButton;
    public UnityEngine.UI.Image cloudButton;
    public UnityEngine.UI.Image moonButton;
    public UnityEngine.UI.Image birdButton;
    public UnityEngine.UI.Image daylightCycleButton;
    public UnityEngine.UI.Image cheatsButton;

    private UnityEngine.UI.Text soundSliderText;
    private UnityEngine.UI.Text musicSliderText;
    private UnityEngine.UI.Text particleSliderText;

    public Color inActiveColor;
    public Color ActiveColor;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();

        soundSliderText = soundSlider.GetComponentInChildren<UnityEngine.UI.Text>();
        musicSliderText = musicSlider.GetComponentInChildren<UnityEngine.UI.Text>();
        particleSliderText = particleSlider.GetComponentInChildren<UnityEngine.UI.Text>();

        SetupSettings();
    }

    public void SetupSettings() {
        LoadColor(settings.useVFX, vfxButton);
        LoadColor(settings.useSound, soundButton);
        LoadColor(settings.useMusic, musicButton);
        LoadColor(settings.useParticle, particleButton);
        LoadColor(settings.useGrass, grassButton);
        LoadColor(settings.useFootPrints, footPrintButton);
        LoadColor(settings.useClouds, cloudButton);
        LoadColor(settings.useMoons, moonButton);
        LoadColor(settings.useBirds, birdButton);
        LoadColor(settings.daylightCycle, daylightCycleButton);
        LoadColor(settings.useCheats, cheatsButton);

        soundSlider.value = settings.soundVolume;
        musicSlider.value = settings.musicVolume;
        particleSlider.value = settings.particleSlider;

        UpdateSoundText();
        UpdateMusicText();
        UpdateParticleText();
    }

    public void LoadSettings() {
        this.gameObject.SetActive(true);
    }

    public void UnloadSettings() {
        this.gameObject.SetActive(false);
    }

    public void ChangeVFXSetting() {
        settings.useVFX = !settings.useVFX;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useVFX, vfxButton);
    }

    public void ChangeSoundSetting() {
        settings.useSound = !settings.useSound;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useSound, soundButton);
    }

    public void UpdateSoundVolume() {
        settings.soundVolume = soundSlider.value;
        settings.UpdatePlayerPrefs();
        UpdateSoundText();
    }

    private void UpdateSoundText() {
        soundSliderText.text = ((int)(soundSlider.value * 100)).ToString() + '%';
    }

    private void UpdateMusicText() {
        musicSliderText.text = ((int)(musicSlider.value * 100)).ToString() + '%';
    }

    private void UpdateParticleText() {
        particleSliderText.text = particleSlider.value.ToString() + 'x';
    }

    public void ChangeMusicSetting() {
        settings.useMusic = !settings.useMusic;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useMusic, musicButton);
    }

    public void UpdateMusicVolume() {
        settings.musicVolume = musicSlider.value;
        settings.UpdatePlayerPrefs();
        UpdateMusicText();
    }

    public void UpdateParticleCount() {
        settings.particleSlider = particleSlider.value;
        settings.UpdatePlayerPrefs();
        UpdateParticleText();
    }

    public void ChangeParticleSetting() {
        settings.useParticle = !settings.useParticle;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useParticle, particleButton);
    }

    public void ChangeGrassSetting() {
        settings.useGrass = !settings.useGrass;
        settings.useSeaweed = !settings.useSeaweed;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useGrass, grassButton);
    }

    public void ChangeFootprintSetting() {
        settings.useFootPrints = !settings.useFootPrints;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useFootPrints, footPrintButton);
    }

    public void ChangeCloudSetting() {
        settings.useClouds = !settings.useClouds;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useClouds, cloudButton);
    }

    public void ChangeMoonSetting() {
        settings.useMoons = !settings.useMoons;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useMoons, moonButton);
    }

    public void ChangeBirdSetting() {
        settings.useBirds = !settings.useBirds;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useBirds, birdButton);
    }

    public void ChangeDaylightSetting() {
        settings.daylightCycle = !settings.daylightCycle;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.daylightCycle, daylightCycleButton);
    }

    public void ChangeCheatsSetting() {
        settings.useCheats = !settings.useCheats;
        settings.UpdatePlayerPrefs();
        LoadColor(settings.useCheats, cheatsButton);
    }

    public void LoadColor(bool condition, UnityEngine.UI.Image button) {
        if (condition) {
            button.color = ActiveColor;
        }
        else {
            button.color = inActiveColor;
        }
    }

    public void ResetSettings() {
        settings.ResetPlayerPrefs();
        SetupSettings();
    }
}
