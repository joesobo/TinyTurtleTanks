using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool pauseActive = false;
    private bool menuActive = false;
    private GameSettings settings;
    private LevelRunner levelRunner;
    private QuitMenu quitMenu;

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
        settings = FindObjectOfType<GameSettings>();
        levelRunner = FindObjectOfType<LevelRunner>();
        quitMenu = FindObjectOfType<QuitMenu>();
    }

    private void Update()
    {
        if (!levelRunner.isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseActive = !pauseActive;
                menuActive = !menuActive;
            }

            if (pauseActive)
            {
                if (menuActive)
                {
                    LeanTween.scale(gameObject, Vector3.one, 0.4f);
                }
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
        menuActive = false;
    }

    public void OpenControls()
    {

    }

    public void OpenQuit()
    {
        quitMenu.pauseMenu = this;
        menuActive = false;
        LeanTween.scale(gameObject, Vector3.zero, 0.4f);
        LeanTween.scale(quitMenu.gameObject, Vector3.one, 0.4f);
    }

    public void CloseQuit(){
        menuActive = true;
        LeanTween.scale(gameObject, Vector3.one, 0.4f);
        LeanTween.scale(quitMenu.gameObject, Vector3.zero, 0.4f);
    }

    public void onMenu()
    {
        SceneManager.LoadScene(0);
    }
}
