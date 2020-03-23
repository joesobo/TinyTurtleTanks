using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int speed = 10;
    public Color color;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        color = new Color(color.r, color.g, color.b, color.a - (Time.deltaTime / speed));
        spriteRenderer.color = color;

        if(spriteRenderer.color.a < 0.1f){
            Destroy(gameObject);
        }
    }
}
