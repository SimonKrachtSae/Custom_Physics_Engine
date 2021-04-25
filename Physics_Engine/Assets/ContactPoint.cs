using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactPoint : MonoBehaviour
{
    public Vector3 Point { get; }
    public Vector3 Normal { get; }

    public ContactPoint(Vector3 point, Vector3 normal)
    {
        Point = point;
        Normal = normal;
    }
}
