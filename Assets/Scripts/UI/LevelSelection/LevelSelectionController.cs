using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionController : MonoBehaviour
{
    public int currentSelectedWorld = -1;
    public int currentWorld = 1;
    public int nextWorldToUnlock = 2;
    public Material defaultMat;
    public Material lockMat;
    public Material activeMat;

    public List<LevelPicker> levels;
    private GameSettings settings;

    void Start()
    {
        settings = FindObjectOfType<GameSettings>();
    }

    public void UpdateActiveMaterials()
    {
        settings.currentLevel = currentSelectedWorld;
        if(currentSelectedWorld != -1){
            foreach (LevelPicker level in levels)
            {
                if(level.levelNumber == currentSelectedWorld){
                    level.meshRenderer.material = activeMat;
                }else{
                    level.meshRenderer.material = defaultMat;
                }
            }
        }
    }
}
