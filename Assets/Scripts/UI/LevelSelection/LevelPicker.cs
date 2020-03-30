using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPicker : MonoBehaviour
{
    private Vector3 currentScale;
    public Vector3 growScale;
    public float speed = 1;
    public int levelNumber;
    private LevelSelectionController levelSelection;
    public MeshRenderer meshRenderer;
    private Material defaultMat;
    private Material activeMat;

    void Start()
    {
        levelSelection = FindObjectOfType<LevelSelectionController>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        currentScale = transform.localScale;
        defaultMat = levelSelection.defaultMat;
        activeMat = levelSelection.activeMat;
    }

    void OnMouseOver()
    {
        if (levelSelection.currentWorld >= levelNumber)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, growScale, Time.deltaTime * speed);

            if (Input.GetMouseButtonDown(0))
            {
                levelSelection.currentSelectedWorld = levelNumber;
                levelSelection.UpdateActiveMaterials();
            }
        }
    }

    void OnMouseExit()
    {
        transform.localScale = currentScale;
    }
}
