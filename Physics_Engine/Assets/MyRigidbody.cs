using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class MyRigidbody : MonoBehaviour
{
    [SerializeField]
    public float mass;
    [SerializeField]
    public float Drag;

    public float inverseMass;
    
    public Vector3 linearAcceleration = new Vector3(0,0,0);
    public Vector3 angularAcceleration = new Vector3(0, 0, 0);
    public Vector3 angularVelocity = new Vector3(0, 0, 0);
    private Vector3 rotation;
    [SerializeField]
    private float gravityScale = 1;

    private bool isGravityAdded = false;

    public bool useGravity;
    private void FixedUpdate()
    {
        if (useGravity)
            linearAcceleration += Physics.gravity * gravityScale * Time.deltaTime;

        transform.position += linearAcceleration * Time.deltaTime;
        rotation += Mathf.Rad2Deg * angularAcceleration * Time.deltaTime;
        //linearAcceleration = Vector3.zero;
        AddDrag();
    }
    private void AddDrag()
    {
        if(linearAcceleration.magnitude != 0)
        {
            linearAcceleration -= linearAcceleration.normalized * Drag * Time.deltaTime;
        }
    }
    private void OnValidate()
    {
        if (mass <= 0)
        {
            mass = 1e-7f;
        }
        inverseMass = 1.0f / mass;
        Drag = Mathf.Clamp(Drag, 0.0f, 1.0f);
        gravityScale = Mathf.Clamp(gravityScale, 0.0f, 1.0f);
    }
    public void AddForce(Vector3 force)
    {
        linearAcceleration += force * inverseMass;
    }
    public void AddForceAtPosition(Vector3 _force, Vector3 _position)
    {
        Vector3 distance = _position - transform.position;
        Vector3 torque = Vector3.Cross(distance, _force);

        AddTorque(torque);
        AddForce(_force);
    }
    public void AddTorque(Vector3 torque)
    {
        angularAcceleration += torque * inverseMass;
    }
}
