using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : BaseMenu {
    private bool pauseActive = false;
    private bool menuActive = false;
    private GameSettings settings;
    private LevelRunner levelRunner;
    private bool stopScale = false;

    private QuitMenu quitMenu;

    private void Start() {
        quitMenu = FindObjectOfType<QuitMenu>();
        gameObject.transform.localScale = Vector3.zero;
        settings = FindObjectOfType<GameSettings>();
        levelRunner = FindObjectOfType<LevelRunner>();
    }

    private void Update() {
        if (!levelRunner.isDead) {
            if (!settings.isPaused && Input.GetKeyDown(KeyCode.Escape)) {
                TogglePauseMenu();
            }

            if (pauseActive) {
                settings.isPaused = true;
                if (gameObject.transform.localScale.x == 1) {
                    stopScale = true;
                }
                if (!stopScale) {
                    LeanTween.scale(gameObject, Vector3.one, 0.4f).setIgnoreTimeScale(true);
                }
            }
        }
    }

    private void TogglePauseMenu() {
        pauseActive = !pauseActive;
        menuActive = !menuActive;
    }

    public void OnClose() {
        LeanTween.scale(gameObject, Vector3.zero, 0.4f).setIgnoreTimeScale(true);
        if (settings.isPaused) {
            settings.isPaused = false;
        }
        pauseActive = false;
        menuActive = false;
        stopScale = false;
    }

    public override void OpenQuit() {
        quitMenu.menu = this;
        LeanTween.scale(gameObject, Vector3.zero, 0.4f).setIgnoreTimeScale(true);
        LeanTween.scale(quitMenu.gameObject, Vector3.one, 0.4f).setIgnoreTimeScale(true);
    }

    public override void CloseQuit() {
        LeanTween.scale(gameObject, Vector3.one, 0.4f).setIgnoreTimeScale(true);
        LeanTween.scale(quitMenu.gameObject, Vector3.zero, 0.4f).setIgnoreTimeScale(true);
    }
}
