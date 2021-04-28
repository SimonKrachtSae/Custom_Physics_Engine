using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyPhysics 
{
    public const float SEPERATION_VALUE = 0.01f;

    public static Vector3 Gravity { get => s_gravity; set => s_gravity = value; }
    private static Vector3 s_gravity = new Vector3(0, -9.81f, 0);
}
