using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using System;

public class SphereCollider : Collider
{
    public float Radius { get => transform.lossyScale.x / 2; }
    public float Scale { get => rb.Mass; }
    public float Bounciness;
    public void SetScale(float f)
    {
        transform.localScale = Vector3.one * f;
    }
}
