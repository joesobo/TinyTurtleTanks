using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindActiveLevel : MonoBehaviour
{
    private LevelSelectionController levelSelection;

    void Start()
    {
        levelSelection = FindObjectOfType<LevelSelectionController>();
    }
}
