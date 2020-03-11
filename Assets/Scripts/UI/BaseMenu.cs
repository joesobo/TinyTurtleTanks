using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseMenu : MonoBehaviour
{
    public void OpenControls()
    {

    }

    public void onMenu()
    {
        SceneManager.LoadScene(0);
    }

    public abstract void OpenQuit();

    public abstract void CloseQuit();
}