using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWorld : MonoBehaviour
{
    private GameObject lockImage;
    private GameObject lockButton;
    public Material unlockMat;
    public Material lockMat;
    public GameObject planet;
    private LevelSelectionController levelSelection;
    public int worldNumber;

    void Start()
    {
        lockImage = transform.GetChild(0).gameObject;
        lockButton = transform.GetChild(1).gameObject;
        levelSelection = FindObjectOfType<LevelSelectionController>();
    }

    public void ActivateWorld()
    {
        if (levelSelection.nextWorldToUnlock == worldNumber)
        {
            lockImage.SetActive(false);
            lockButton.SetActive(false);
            planet.GetComponent<MeshRenderer>().material = unlockMat;
            levelSelection.currentWorld++;
            levelSelection.nextWorldToUnlock++;
        }
    }

    public void DeactivateWorld()
    {
        lockImage.SetActive(true);
        lockButton.SetActive(true);
        planet.GetComponent<MeshRenderer>().material = lockMat;
        levelSelection.currentWorld--;
            levelSelection.nextWorldToUnlock--;
    }
}
