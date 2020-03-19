using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditLoader : MonoBehaviour
{
    public void LoadCredits(){
        this.gameObject.SetActive(true);
    }

    public void UnloadCredits(){
        this.gameObject.SetActive(false);
    }
}
