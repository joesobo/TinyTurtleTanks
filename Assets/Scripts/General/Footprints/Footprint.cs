using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int speed = 10;
    public Color color;
    private GameSettings settings;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        settings = FindObjectOfType<GameSettings>();
    }

    private void Update()
    {
        if (!settings.isPaused)
        {
            color = new Color(color.r, color.g, color.b, color.a - (Time.deltaTime / speed));
            spriteRenderer.color = color;

            if (spriteRenderer.color.a < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
