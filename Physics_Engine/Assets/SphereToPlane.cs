using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class SphereToPlane : MonoBehaviour
{
    public Sphere sphere;
    public Plane plane;

    public float distance;
    Vector3 closestPoint;
    Vector3 normal;
    
    public void OnDrawGizmos()
    {
        distance = Vector.Dot(normal, sphere.transform.position - plane.transform.position);
        distance -= sphere.radius;
        normal = plane.transform.up;
        closestPoint = sphere.transform.position + sphere.radius * (-normal);
        if(distance <= 0)
        {
            plane.transform.position = closestPoint - (closestPoint - plane.transform.position);
        }
       // Vector3 closestPointOnPlane = Vector.ProjectOnPlane(sphere.transform.position, -normal);
       closestPoint= new Vector3(Mathf.Clamp(closestPoint.x, -plane.X_HalfSize, plane.X_HalfSize), Mathf.Clamp(closestPoint.y, -plane.Y_HalfSize, plane.Y_HalfSize), Mathf.Clamp(closestPoint.z, -plane.Z_HalfSize, plane.Z_HalfSize));
       // Matrix4x4 rot = Matrix4x4.Rotate(plane.transform.rotation);
       // closestPointOnPlane = rot * closestPointOnPlane;
       // closestPointOnPlane = transform.position + closestPointOnPlane;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(closestPoint, 0.1f);
        //Gizmos.DrawSphere(closestPointOnPlane, 0.1f);
    }
}
