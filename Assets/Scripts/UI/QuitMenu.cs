using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour {
    public BaseMenu menu;

    public void OnYes() {
        SceneManager.LoadScene(0);
    }

    public void OnNo() {
        menu.CloseQuit();
    }
}
