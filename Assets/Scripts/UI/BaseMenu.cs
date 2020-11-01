using UnityEngine;

public abstract class BaseMenu : MonoBehaviour {
    public void OpenControls() {

    }

    public void OpenSettings() {

    }

    public abstract void OpenQuit();

    public abstract void CloseQuit();
}