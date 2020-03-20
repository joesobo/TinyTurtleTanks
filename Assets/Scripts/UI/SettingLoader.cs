using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingLoader : MonoBehaviour
{
    private GameSettings settings;
    public Image vfxButton;

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
}
