using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLoader : MonoBehaviour {
    private GameSettings settings;
    public GameObject vfxButton;
    public GameObject soundButton;
    public GameObject particleButton;
    public GameObject grassButton;
    public GameObject footPrintButton;
    public GameObject cloudButton;

    private UnityEngine.UI.Image vfxImg;
    private UnityEngine.UI.Image soundImg;
    private UnityEngine.UI.Image particleImg;
    private UnityEngine.UI.Image grassImg;
    private UnityEngine.UI.Image footPrintImg;
    private UnityEngine.UI.Image cloudImg;

    public Color inActiveColor;
    public Color ActiveColor;

    private void Awake() {
        settings = FindObjectOfType<GameSettings>();

        vfxImg = vfxButton.GetComponent<UnityEngine.UI.Image>();
        soundImg = soundButton.GetComponent<UnityEngine.UI.Image>();
        particleImg = particleButton.GetComponent<UnityEngine.UI.Image>();
        grassImg = grassButton.GetComponent<UnityEngine.UI.Image>();
        footPrintImg = footPrintButton.GetComponent<UnityEngine.UI.Image>();
        cloudImg = cloudButton.GetComponent<UnityEngine.UI.Image>();

        SetupSettings();
    }
    
    public void SetupSettings() {
        LoadColor(settings.useVFX, vfxImg);
        LoadColor(settings.useSound, soundImg);
        LoadColor(settings.useParticle, particleImg);
        LoadColor(settings.useGrass, grassImg);
        LoadColor(settings.useFootPrints, footPrintImg);
        LoadColor(settings.useClouds, cloudImg);
    }

    public void LoadSettings() {
        this.gameObject.SetActive(true);
    }

    public void UnloadSettings() {
        this.gameObject.SetActive(false);
    }

    public void ChangeVFXSetting() {
        settings.useVFX = !settings.useVFX;
        LoadColor(settings.useVFX, vfxImg);
    }

    public void ChangeSoundSetting() {
        settings.useSound = !settings.useSound;
        LoadColor(settings.useSound, soundImg);
    }

    public void ChangeParticleSetting() {
        settings.useParticle = !settings.useParticle;
        LoadColor(settings.useParticle, particleImg);
    }

    public void ChangeGrassSetting() {
        settings.useGrass = !settings.useGrass;
        LoadColor(settings.useGrass, grassImg);
    }

    public void ChangeFootprintSetting() {
        settings.useFootPrints = !settings.useFootPrints;
        LoadColor(settings.useFootPrints, footPrintImg);
    }

    public void ChangeCloudSetting() {
        settings.useClouds = !settings.useClouds;
        LoadColor(settings.useClouds, cloudImg);
    }

    public void LoadColor(bool condition, UnityEngine.UI.Image button) {
        if (condition) {
            button.color = ActiveColor;
        }
        else {
            button.color = inActiveColor;
        }
    }
}
