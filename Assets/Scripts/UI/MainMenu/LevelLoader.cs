using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public GameObject loadingScreen;
    public UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Text textProgress;

    private void Start() {
        UnloadLevelLoader();
    }

    public void LoadLevelLoader() {
        this.gameObject.SetActive(true);
    }

    public void UnloadLevelLoader() {
        this.gameObject.SetActive(false);
    }

    public void LoadLevel(int sceneIndex) {
        StartCoroutine(LoadLevelAsynchronously(sceneIndex));
    }

    IEnumerator LoadLevelAsynchronously(int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            textProgress.text = progress * 100f + "%";

            yield return null;
        }
    }
}
