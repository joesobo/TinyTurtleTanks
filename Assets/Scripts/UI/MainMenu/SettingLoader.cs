using UnityEngine;

public class SettingLoader : MonoBehaviour {
    private GameSettings settings;
    public UnityEngine.UI.Image vfxButton;
    public UnityEngine.UI.Image soundButton;
    public UnityEngine.UI.Slider soundSlider;
    public UnityEngine.UI.Image musicButton;
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Image particleButton;
    public UnityEngine.UI.Image grassButton;
    public UnityEngine.UI.Image footPrintButton;
    public UnityEngine.UI.Image cloudButton;
    public UnityEngine.UI.Image moonButton;
    public UnityEngine.UI.Image birdButton;
    public UnityEngine.UI.Image daylightCycleButton;
    public UnityEngine.UI.Image cheatsButton;

    private UnityEngine.UI.Text soundSliderText;
    private UnityEngine.UI.Text musicSliderText;

    public Color inActiveColor;
    public Color ActiveColor;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();

        soundSliderText = soundSlider.GetComponentInChildren<UnityEngine.UI.Text>();
        musicSliderText = musicSlider.GetComponentInChildren<UnityEngine.UI.Text>();

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

        UpdateSoundText();
        UpdateMusicText();
    }

    public void LoadSettings() {
        this.gameObject.SetActive(true);
    }

    public void UnloadSettings() {
        this.gameObject.SetActive(false);
    }

    public void ChangeVFXSetting() {
        settings.useVFX = !settings.useVFX;
        settings.UpdateAll();
        LoadColor(settings.useVFX, vfxButton);
    }

    public void ChangeSoundSetting() {
        settings.useSound = !settings.useSound;
        settings.UpdateAll();
        LoadColor(settings.useSound, soundButton);
    }

    public void UpdateSoundVolume() {
        settings.soundVolume = soundSlider.value;
        settings.UpdateAll();
        UpdateSoundText();
    }

    private void UpdateSoundText() {
        soundSliderText.text = ((int)(soundSlider.value * 100)).ToString() + '%';
    }

    private void UpdateMusicText() {
        musicSliderText.text = ((int)(musicSlider.value * 100)).ToString() + '%';
    }

    public void ChangeMusicSetting() {
        settings.useMusic = !settings.useMusic;
        settings.UpdateAll();
        LoadColor(settings.useMusic, musicButton);
    }

    public void UpdateMusicVolume() {
        settings.musicVolume = musicSlider.value;
        settings.UpdateAll();
        UpdateMusicText();
    }

    public void ChangeParticleSetting() {
        settings.useParticle = !settings.useParticle;
        settings.UpdateAll();
        LoadColor(settings.useParticle, particleButton);
    }

    public void UpdateParticleSetting() {
        settings.UpdateParticleSettings();
        settings.UpdateAll();
    }

    public void ChangeGrassSetting() {
        settings.useGrass = !settings.useGrass;
        settings.useSeaweed = !settings.useSeaweed;
        settings.UpdateAll();
        LoadColor(settings.useGrass, grassButton);
    }

    public void ChangeFootprintSetting() {
        settings.useFootPrints = !settings.useFootPrints;
        settings.UpdateAll();
        LoadColor(settings.useFootPrints, footPrintButton);
    }

    public void ChangeCloudSetting() {
        settings.useClouds = !settings.useClouds;
        settings.UpdateAll();
        LoadColor(settings.useClouds, cloudButton);
    }

    public void ChangeMoonSetting() {
        settings.useMoons = !settings.useMoons;
        settings.UpdateAll();
        LoadColor(settings.useMoons, moonButton);
    }

    public void ChangeBirdSetting() {
        settings.useBirds = !settings.useBirds;
        settings.UpdateAll();
        LoadColor(settings.useBirds, birdButton);
    }

    public void ChangeDaylightSetting() {
        settings.daylightCycle = !settings.daylightCycle;
        settings.UpdateAll();
        LoadColor(settings.daylightCycle, daylightCycleButton);
    }

    public void ChangeCheatsSetting() {
        settings.useCheats = !settings.useCheats;
        settings.UpdateAll();
        LoadColor(settings.useCheats, cheatsButton);
    }

    public void LoadColor(bool condition, UnityEngine.UI.Image button) {
        if (button) {
            if (condition) {
                button.color = ActiveColor;
            }
            else {
                button.color = inActiveColor;
            }
        }
    }

    public void ResetSettings() {
        settings.ResetPlayerPrefs();
        SetupSettings();
    }
}
