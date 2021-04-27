using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class PhysicsObject : MonoBehaviour
{
    public MyRigidbody rb;
    public bool isTrigger;

    private void Awake()
    {
        rb = transform.GetComponent<MyRigidbody>();
    }
    private void Start()
    {
        UltimatePhysicsManager.Instance.AddPhysicsObject(this); 
    }
    private void OnDestroy()
    {
        UltimatePhysicsManager.Instance.RemovePhysicsObject(this);
    }
}
