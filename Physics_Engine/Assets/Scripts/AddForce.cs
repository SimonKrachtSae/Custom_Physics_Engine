using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

[RequireComponent(typeof(MyRigidbody))]
public class AddForce : MonoBehaviour
{

    [SerializeField] private Vector3 forces;

    private MyRigidbody rb;

    private void Awake()
    {
        rb = GetComponent<MyRigidbody>();
    }
    private void FixedUpdate()
    {
        if(rb != null)
        {
            rb.LinearVelocity += forces * rb.InverseMass;
            forces = Vector3.zero;
        }
    }

}
