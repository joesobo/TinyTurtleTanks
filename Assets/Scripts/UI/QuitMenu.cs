using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    public PauseMenu pauseMenu;

    public void OnYes(){
        Application.Quit();
    }

    public void OnNo(){
        pauseMenu.CloseQuit();
    }
}
