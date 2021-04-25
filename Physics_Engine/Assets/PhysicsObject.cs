using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class PhysicsObject : MonoBehaviour
{
    public float radius { get => transform.lossyScale.x / 2; }

    public float xSize { get => transform.lossyScale.x; }
    public float ySize { get => transform.lossyScale.y; }
    public float zSize { get => transform.lossyScale.z; }

    public float xHalfSize { get => transform.lossyScale.x / 2; }
    public float yHalfSize { get => transform.lossyScale.y / 2; }
    public float zHalfSize { get => transform.lossyScale.z / 2; }


    public Vector3 closestPoint;
    public float distanceBetweenPoints;
    public float Speed;
    private Vector3 pointA;
    private Vector3 pointB;
    private bool click = false;
    private float timeBetweenPoints;
    public Vector3 MoveDir;

    public MyRigidbody rb;

    private void Awake()
    {
        rb = transform.GetComponent<MyRigidbody>();
    }
    private void Start()
    {
        UltimatePhysicsManager.Instance.AddRigidbody(this); 
    }
    private void OnDestroy()
    {
        UltimatePhysicsManager.Instance.RemoveRigidbody(this);
    }
    private void FixedUpdate()
    {
        SetSpeedAndMoveDir();
    }
    public void SetSpeedAndMoveDir()
    {
        timeBetweenPoints += Time.fixedDeltaTime;
        if(!click)
        {
            pointA = transform.position;
            click = true;
            return;
        }


        if(click)
        {
            pointB = transform.position;
            Speed = (pointA - pointB).magnitude / timeBetweenPoints;
            MoveDir = Vector.GetDirectionVector(pointA, pointB).normalized;
            click = false;
            timeBetweenPoints = 0.0f;
            return;
        }
    }
}
