using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRenderer : MonoBehaviour
{
    public float tankDepth = 45f;
    public float perspectiveCoeff = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Material material;

    private bool isSetup = false;

    private Vector2 baseScale;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;

        baseScale = transform.localScale;
    }

    // Update is called once per frame
    public void UpdateRendering()
    {
        float depth = transform.position.z;
        float coeff = ScaleCoeff(depth);
        material.SetFloat("radius", (1f - coeff) * 30f);
        transform.localScale = new Vector3(baseScale.x * coeff, baseScale.y * coeff, 1f);
    }

    private float ScaleCoeff(float depth)
    {
        return perspectiveCoeff + (tankDepth - depth) * (1f - perspectiveCoeff) / tankDepth;
    }
}
