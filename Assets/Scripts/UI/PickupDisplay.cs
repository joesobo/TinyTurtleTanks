using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupDisplay : MonoBehaviour {
    private Image image;
    private Animator displayAnimator;
    private Animator pickupAnimator;
    private float seconds;

    public void begin(float sec, Color color) {
        displayAnimator = transform.parent.GetComponent<Animator>();
        pickupAnimator = GetComponent<Animator>();
        pickupAnimator.speed = 1 / sec;
        seconds = sec;

        image = GetComponent<Image>();
        image.color = color;

        StartCoroutine("StartShrink");
    }

    IEnumerator StartShrink() {
        displayAnimator.SetTrigger("SlideIn");
        pickupAnimator.SetTrigger("isShrinking");
        yield return new WaitForSeconds(seconds);
        displayAnimator.SetTrigger("SlideOut");
    }
}
