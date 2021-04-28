using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class Plane : PhysicsObject
{
    public Sphere sphere;
    public float X_HalfSize { get => X_Size / 2.0f; }
    public float Y_HalfSize { get => Y_Size / 2.0f; }
    public float Z_HalfSize { get => Z_Size / 2.0f; }

    public float X_Size { get => transform.lossyScale.x; }
    public float Y_Size { get => transform.lossyScale.y; }
    public float Z_Size { get => transform.lossyScale.z; }
    
}
