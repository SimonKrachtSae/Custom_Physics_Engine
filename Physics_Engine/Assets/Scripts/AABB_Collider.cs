using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB_Collider : Collider
{
    public float X_Size { get => transform.lossyScale.x; }
    public float Y_Size { get => transform.lossyScale.y; }
    public float Z_Size { get => transform.lossyScale.z; }

    public float X_HalfSize { get => transform.lossyScale.x / 2.0f; }
    public float Y_HalfSize { get => transform.lossyScale.y / 2.0f; }
    public float Z_HalfSize { get => transform.lossyScale.z / 2.0f; }
}
