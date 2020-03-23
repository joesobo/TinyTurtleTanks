using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingLoader : MonoBehaviour
{
    private GameSettings settings;
    public Image vfxButton;
    public Image soundButton;
    public Image particleButton;
    public Image grassButton;
    public Image trailButton;
    public Image cloudButton;

    public Color inActiveColor;
    public Color ActiveColor;

    private void Awake()
    {
        settings = FindObjectOfType<GameSettings>();
        SetupSettings();
    }

    public void SetupSettings(){
        LoadColor(settings.useVFX, vfxButton);
        LoadColor(settings.useSound, soundButton);
        LoadColor(settings.useParticle, particleButton);
        LoadColor(settings.useGrass, grassButton);
        LoadColor(settings.useTrails, trailButton);
        LoadColor(settings.useClouds, cloudButton);
    }

    public void LoadSettings()
    {
        this.gameObject.SetActive(true);
    }

    public void UnloadSettings()
    {
        this.gameObject.SetActive(false);
    }

    public void ChangeVFXSetting()
    {
        settings.useVFX = !settings.useVFX;
        LoadColor(settings.useVFX, vfxButton);
    }

    public void ChangeSoundSetting()
    {
        settings.useSound = !settings.useSound;
        LoadColor(settings.useSound, soundButton);
    }

    public void ChangeParticleSetting()
    {
        settings.useParticle = !settings.useParticle;
        LoadColor(settings.useParticle, particleButton);
    }

    public void ChangeGrassSetting()
    {
        settings.useGrass = !settings.useGrass;
        LoadColor(settings.useGrass, grassButton);
    }

    public void ChangeTrailSetting()
    {
        settings.useTrails = !settings.useTrails;
        LoadColor(settings.useTrails, trailButton);
    }

    public void ChangeCloudSetting()
    {
        settings.useClouds = !settings.useClouds;
        LoadColor(settings.useClouds, cloudButton);
    }

    public void LoadColor(bool condition, Image button){
        if (condition)
        {
            button.color = ActiveColor;
        }
        else
        {
            button.color = inActiveColor;
        }
    }
}
