using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class MyRigidbody : MonoBehaviour
{
    public Vector3 AngularVelocity;

    [SerializeField]
    public float mass;
    [SerializeField]
    private float drag;
    [SerializeField]
    private bool useGravity;
    [SerializeField]
    private float gravityScale = 1;
    [SerializeField]
    private bool isKinematic;

    public float inverseMass;

    public Vector3 LinearVelocity = new Vector3(0,0,0);
    Vector3 rotation;
    private void Awake()
    {
        rotation = transform.eulerAngles;
    }
    private void FixedUpdate()
    {
        if (useGravity)
            LinearVelocity += Physics.gravity * gravityScale * Time.deltaTime;

        if(!isKinematic)
        {
            transform.position += LinearVelocity * Time.deltaTime;
        }
        rotation -= Mathf.Rad2Deg * AngularVelocity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation);
        AddDrag();
    }
    private void AddDrag()
    {
        if(LinearVelocity.magnitude != 0)
        {
            LinearVelocity -= LinearVelocity * drag * Time.deltaTime;
        }
    }
    private void OnValidate()
    {
        if (mass <= 0)
        {
            mass = 1e-7f;
        }
        inverseMass = 1.0f / mass;
        drag = Mathf.Clamp(drag, 0.0f, 1.0f);
        gravityScale = Mathf.Clamp(gravityScale, 0.0f, 1.0f);
    }
}
