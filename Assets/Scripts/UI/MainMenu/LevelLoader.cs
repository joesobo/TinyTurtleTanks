using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text textProgress;

    public void LoadLevel(int sceneIndex){
        StartCoroutine(LoadLevelAsynchronously(sceneIndex));
    }

    IEnumerator LoadLevelAsynchronously(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress/.9f);
           
           slider.value = progress;
           textProgress.text = progress * 100f + "%";   

            yield return null;
        }
    }
}
