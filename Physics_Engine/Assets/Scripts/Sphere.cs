using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using System;

public class Sphere : PhysicsObject
{
    public float radius { get => transform.lossyScale.x / 2; }
}
