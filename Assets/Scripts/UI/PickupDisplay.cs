using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupDisplay : MonoBehaviour {
    private Image image;
    private Animator animator;
    private float seconds;

    public void reset(float sec, Color color) {
        animator = GetComponent<Animator>();
        animator.speed = 1 / sec;
        seconds = sec;

        image = GetComponent<Image>();
        image.color = color;

        StartCoroutine("StartShrink");
    }

    IEnumerator StartShrink() {
        animator.SetBool("isShrinking", true);
        yield return new WaitForSeconds(seconds);
        animator.SetBool("isShrinking", false);
    }
}
