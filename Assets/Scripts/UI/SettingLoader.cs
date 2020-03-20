using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLoader : MonoBehaviour
{
    public void LoadSettings(){
        this.gameObject.SetActive(true);
    }

    public void UnloadSettings(){
        this.gameObject.SetActive(false);
    }
}
