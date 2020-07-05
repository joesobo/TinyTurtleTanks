using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour {
    public BaseMenu menu;

    public void OnYes() {
        Application.Quit();
    }

    public void OnNo() {
        menu.CloseQuit();
    }
}
