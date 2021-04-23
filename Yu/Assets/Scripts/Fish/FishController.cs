using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FishRenderer))]
public class FishController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 velocity;

    private FishRenderer fishRenderer;

    void Start()
    {
        fishRenderer = GetComponent<FishRenderer>();

        velocity = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        velocity.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateRendering();
    }

    private void UpdateMovement()
    {
        Vector3 nextPosition = transform.position + velocity * Time.deltaTime;
        transform.position = nextPosition;
    }

    private void UpdateRendering()
    {
        fishRenderer.UpdateRendering();
    }
}
