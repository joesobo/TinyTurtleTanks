using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool pauseActive = false;
    private GameSettings settings;
    private LevelRunner levelRunner;

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
        settings = FindObjectOfType<GameSettings>();
        levelRunner = FindObjectOfType<LevelRunner>();
    }

    private void Update()
    {
        if (!levelRunner.isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseActive = !pauseActive;
            }

            if (pauseActive)
            {
                LeanTween.scale(gameObject, Vector3.one, 0.4f);

                settings.isPaused = true;
            }
            else
            {
                OnClose();
            }
        }
    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.4f);
        if (settings.isPaused)
        {
            settings.isPaused = false;
        }
        pauseActive = false;
    }

    public void onMenu()
    {
        SceneManager.LoadScene(0);
    }
}
