using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using System;

public class Sphere : PhysicsObject
{
    public float radius { get => transform.lossyScale.x / 2; }
    public float scale { get => rb.mass; }
    public float bounciness;
    public void SetScale(float f)
    {
        transform.localScale = Vector3.one * f;
    }
}
