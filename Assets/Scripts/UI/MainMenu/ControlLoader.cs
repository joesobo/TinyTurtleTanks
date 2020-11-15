using UnityEngine;

public class ControlLoader : MonoBehaviour {
    public void LoadControls() {
        this.gameObject.SetActive(true);
    }

    public void UnloadControls() {
        this.gameObject.SetActive(false);
    }
}
