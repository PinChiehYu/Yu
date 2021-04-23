using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TankData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    [SerializeField]
    private int Height;
    [SerializeField]
    private int Width;
    [SerializeField]
    private int Depth;

    [HideInInspector]
    public Vector3 Size => new Vector3(Width, Height, Depth);
}
