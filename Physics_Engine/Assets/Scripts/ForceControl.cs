using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

[RequireComponent(typeof(MyRigidbody))]
public class ForceControl : MonoBehaviour
{

    public Vector3 Forces;

    private MyRigidbody rb;
    private void OnValidate()
    {
    }
    private void Awake()
    {
        rb = GetComponent<MyRigidbody>();
    }
    private void FixedUpdate()
    {
        if(rb != null)
        {
            rb.LinearVelocity += Forces * rb.inverseMass;
            Forces = Vector3.zero;
        }
    }

}
