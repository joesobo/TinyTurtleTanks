using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public bool shield = false;
    public bool speed = false;
    public bool jump = false;

    private float saveSpeed;
    private float saveJump;

    public int waitForSecondsShield = 5;
    public int waitForSecondsSpeed = 5;
    public int waitForSecondsJump = 5;

    private PlayerController playerController;

    private GameSettings settings;

    private void Start() {
        settings = FindObjectOfType<GameSettings>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (shield)
        {
            shield = false;
            StartCoroutine("StartShield");
        }

        if (speed)
        {
            speed = false;
            saveSpeed = playerController.speed;
            StartCoroutine("StartSpeed");
        }

        if (jump)
        {
            jump = false;
            saveJump = playerController.jumpForce;
            StartCoroutine("StartJump");
        }
    }

    IEnumerator StartShield()
    {
        findChildByName("Shield").gameObject.SetActive(true);
        if(!settings.isPaused){
            yield return new WaitForSeconds(waitForSecondsShield);
        }
        findChildByName("Shield").gameObject.SetActive(false);
    }

    IEnumerator StartSpeed()
    {
        playerController.speed = 15;
        yield return new WaitForSeconds(waitForSecondsSpeed);
        playerController.speed = saveSpeed;
    }

    IEnumerator StartJump()
    {
        playerController.jumpForce = 1000;
        yield return new WaitForSeconds(waitForSecondsJump);
        playerController.jumpForce = saveJump;
    }

    private Transform findChildByName(string text)
    {
        Transform saveChild = null;

        foreach (Transform child in transform)
        {
            if (child.name == text)
            {
                saveChild = child;
            }
        }

        return saveChild;
    }
}
