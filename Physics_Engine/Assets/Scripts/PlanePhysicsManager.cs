using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class PlanePhysicsManager : MonoBehaviour
{
    [SerializeField] private Sphere sphere;
    [SerializeField] private Plane plane;
    public float distance;
    Vector3 normal;
    Vector3 closestPoint;
    Vector3 closestPointOnPlane;
    private void FixedUpdate()
    {
        CheckDistance();  
    }

    private void CheckDistance()
    {
        normal = plane.transform.up;
        distance = Vector.Dot(normal, sphere.transform.position -plane.transform.position);
        distance -= sphere.radius;
        closestPoint = sphere.transform.position + sphere.radius * (-normal);
        closestPointOnPlane = Vector.ProjectOnPlane(closestPoint, normal);
        if(distance <= 0)
        {
            Seperate();
        }
        //closestPointOnPlane = plane.transform.localRotation * (new Vector3(Mathf.Clamp(closestPoint.x,-plane.X_HalfSize, plane.X_HalfSize), Mathf.Clamp(closestPoint.y, -plane.Y_HalfSize, plane.Y_HalfSize),Mathf.Clamp(closestPoint.z, -plane.Z_HalfSize, plane.Z_HalfSize)));
    }
    private void Seperate()
    {
        sphere.transform.position = closestPointOnPlane + plane.transform.up * sphere.radius;
    }

    private void OnDrawGizmos()
    {
        CheckDistance();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(plane.transform.position,normal);
        Gizmos.DrawSphere(closestPoint, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(closestPointOnPlane, 0.1f);
    }
}
