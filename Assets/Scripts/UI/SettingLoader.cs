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

    public Color inActiveColor;
    public Color ActiveColor;

    private void Awake() {
        settings = FindObjectOfType<GameSettings>();
    }

    public void LoadSettings(){
        this.gameObject.SetActive(true);
    }

    public void UnloadSettings(){
        this.gameObject.SetActive(false);
    }

    public void ChangeVFXSetting(){
        settings.useVFX = !settings.useVFX;
        if(settings.useVFX){
            vfxButton.color = ActiveColor;
        }else{
            vfxButton.color = inActiveColor;
        }
         
    }

    public void ChangeSoundSetting(){
        settings.useSound = !settings.useSound;
        if(settings.useSound){
            soundButton.color = ActiveColor;
        }else{
            soundButton.color = inActiveColor;
        }
         
    }

    public void ChangeParticleSetting(){
        settings.useParticle = !settings.useParticle;
        if(settings.useParticle){
            particleButton.color = ActiveColor;
        }else{
            particleButton.color = inActiveColor;
        }
         
    }

    public void ChangeGrassSetting(){
        settings.useGrass = !settings.useGrass;
        if(settings.useGrass){
            grassButton.color = ActiveColor;
        }else{
            grassButton.color = inActiveColor;
        }
         
    }
}
