using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPicker : MonoBehaviour
{
    private Vector3 currentScale;
    public Vector3 growScale;
    public float speed = 1;
    public float levelNumber;
    public LevelSelectionController levelSelection;

    void Start()
    {
        levelSelection = FindObjectOfType<LevelSelectionController>();
        currentScale = transform.localScale;
    }

    void OnMouseOver()
    {
        if(levelSelection.currentWorld >= levelNumber){
            transform.localScale = Vector3.Lerp(transform.localScale, growScale, Time.deltaTime * speed);
        }
    }

    void OnMouseExit()
    {
        transform.localScale = currentScale;
    }
}
