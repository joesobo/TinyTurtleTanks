using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : BaseMenu
{
    private bool pauseActive = false;
    private GameSettings settings;
    private QuitMenu quitMenu;
    private bool stopScale = false;

    private void Start()
    {
        quitMenu = FindObjectOfType<QuitMenu>();
        gameObject.transform.localScale = Vector3.zero;
        settings = FindObjectOfType<GameSettings>();
    }

    private void Update()
    {
        if (pauseActive)
        {
            if (gameObject.transform.localScale.x == 1)
            {
                stopScale = true;
            }
            if (!stopScale)
            {
                LeanTween.scale(gameObject, Vector3.one, 0.4f);
            }
        }
    }

    public void ActivateWin()
    {
        pauseActive = true;
        //LeanTween.scale(gameObject, Vector3.one, 0.4f);
    }

    public override void OpenQuit()
    {
        quitMenu.menu = this;
        LeanTween.scale(gameObject, Vector3.zero, 0.4f);
        LeanTween.scale(quitMenu.gameObject, Vector3.one, 0.4f);
    }

    public override void CloseQuit()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.4f);
        LeanTween.scale(quitMenu.gameObject, Vector3.zero, 0.4f);
    }
}
