using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Sphere Sphere;
    public AABB Aabb;
    private Vector3 closestPoint;
    float distance;
    private void FixedUpdate()
    {
        GetClosestPoint();
        DistanceCheck();
    }
    void DistanceCheck()
    {
        distance = (Sphere.transform.position - closestPoint).magnitude - Sphere.radius;
        if(distance <= 0.0f)
        {
            Seperate();
            ApplyCollision();
        }
    }
    private void Seperate()
    {
        Vector3 direction = Sphere.transform.position - closestPoint;
        direction = direction.normalized;
        Vector3 sepPoint = closestPoint + direction * (Sphere.radius);
        Sphere.transform.position = sepPoint;
    }
    private void ApplyCollision()
    {
        float CollisionForce = Sphere.rb.LinearVelocity.magnitude * Sphere.rb.mass;
        Vector3 direction = Sphere.transform.position - closestPoint;
        Vector3 newForce = direction * CollisionForce;
        Sphere.rb.LinearVelocity = newForce * Sphere.rb.inverseMass;
    }
    void GetClosestPoint()
    {
        closestPoint = Sphere.transform.position - Aabb.transform.position;
        closestPoint = new Vector3(Mathf.Clamp(closestPoint.x, -Aabb.X_HalfSize, Aabb.X_HalfSize), Mathf.Clamp(closestPoint.y, -Aabb.Y_HalfSize, Aabb.Y_HalfSize), Mathf.Clamp(closestPoint.z, -Aabb.Z_HalfSize, Aabb.Z_HalfSize));
        closestPoint = Aabb.transform.position + closestPoint;
    }
    private void OnDrawGizmos()
    {
        closestPoint = Sphere.transform.position - Aabb.transform.position;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Sphere.transform.position, Aabb.transform.position);

        closestPoint = new Vector3(Mathf.Clamp(closestPoint.x, -Aabb.X_HalfSize, Aabb.X_HalfSize), Mathf.Clamp(closestPoint.y, -Aabb.Y_HalfSize, Aabb.Y_HalfSize), Mathf.Clamp(closestPoint.z, -Aabb.Z_HalfSize, Aabb.Z_HalfSize));
        closestPoint = Aabb.transform.position + closestPoint;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(closestPoint, 0.1f);
        Gizmos.DrawLine(Sphere.transform.position, closestPoint);
    }
}
