using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : BaseMenu {
    private bool pauseActive = false;
    private GameSettings settings;
    private LevelSingleton levelSingleton;
    private QuitMenu quitMenu;
    private bool stopScale = false;

    private void Start() {
        quitMenu = FindObjectOfType<QuitMenu>();
        gameObject.transform.localScale = Vector3.zero;
        settings = FindObjectOfType<GameSettings>();
        levelSingleton = FindObjectOfType<LevelSingleton>();
    }

    private void Update() {
        if (pauseActive) {
            if (gameObject.transform.localScale.x == 1) {
                stopScale = true;
            }
            if (!stopScale) {
                LeanTween.scale(gameObject, Vector3.one, 0.4f).setIgnoreTimeScale(true);
            }
        }
    }

    public void ActivateWin() {
        pauseActive = true;
        levelSingleton.UnlockNextLevel();
    }

    public void OnMenu() {
        SceneManager.LoadScene(0);
    }

    public void OnNext() {
        levelSingleton.activeLevel++;
        SceneManager.LoadScene(levelSingleton.activeLevel);
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
