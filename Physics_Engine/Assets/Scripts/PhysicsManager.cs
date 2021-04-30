using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager Instance { get; private set; }
    public List<Collider> physicalObjects;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        physicalObjects = new List<Collider>();

    }
    private void FixedUpdate()
    {
        for (int i = 0; i < physicalObjects.Count; i++)
        {
            for(int j = i + 1; j < physicalObjects.Count; j++)
            {
                CheckForCollision(physicalObjects[i], physicalObjects[j]);
            }
        }
    }
    private void CheckForCollision(Collider a, Collider b)
    {
            if (a.isTrigger || b.isTrigger)
                return;
        if(a is SphereCollider)
        {
            if(b is AABB_Collider)
            {
                CheckForCollision((SphereCollider)a, (AABB_Collider)b);
            }
            else if(b is SphereCollider)
            {
                CheckForCollision((SphereCollider)a, (SphereCollider)b);
            }
            if(b is Plane)
            {
                CheckForCollision((SphereCollider)a, (Plane)b);
            }
        }
        else if(a is AABB_Collider )
        {
            if(b is SphereCollider)
            {
                CheckForCollision((SphereCollider)b, (AABB_Collider)a);
            }
        }
        else if(a is Plane)
        {
            if(b is SphereCollider)
            {
                CheckForCollision((SphereCollider)b, (Plane)a);
            }
        }

    }

    #region SphereToShereCollision
    private void CheckForCollision(SphereCollider a, SphereCollider b)
    {
        Vector3 direction = b.transform.position - a.transform.position;
        CheckDistance(a,b, direction);
    }
    private void CheckDistance(SphereCollider a, SphereCollider b,Vector3 direction)
    {
        float middlepointDistance = direction.magnitude;
        float distance = middlepointDistance - a.Radius - b.Radius;

        if (distance < 0)
        {
            if (b.GetComponent<Bomb>())
            {
                b.GetComponent<Bomb>().Explode = true;
            }
            else if (a.GetComponent<Bomb>())
            {
                a.GetComponent<Bomb>().Explode = true;
            }
            Seperate(a, b, direction);
            ApplyCollision(a, b, direction);
        }
    }
    private void Seperate(SphereCollider a, SphereCollider b, Vector3 direction)
    {
        Vector3 seperationPoint = b.transform.position - direction.normalized * (a.Radius + b.Radius);
        a.transform.position = seperationPoint;
    }
    private void ApplyCollision(SphereCollider a, SphereCollider b, Vector3 direction)
    {
        float collisionForce = a.rb.LinearVelocity.magnitude * a.rb.Mass * a.Bounciness + b.rb.LinearVelocity.magnitude * b.rb.Mass * b.Bounciness;
        Vector3 delta = (direction + a.rb.LinearVelocity + b.rb.LinearVelocity).normalized;
        Vector3 normal = delta * collisionForce;
        a.rb.LinearVelocity = -normal * a.rb.InverseMass;
        b.rb.LinearVelocity = normal * b.rb.InverseMass;
    }
    #endregion
    
    #region SphereToAABBCollision
    private void CheckForCollision(SphereCollider sphere, AABB_Collider aabb)
    {
        Vector3 closestPoint;
        if (CheckDistance(sphere, aabb, out closestPoint))
        {
            if (aabb.GetComponent<WorldBounds>())
                return;

            Seperate(sphere, aabb, closestPoint);
            ApplyCollision(sphere, aabb, closestPoint);
        }
    }
    private bool CheckDistance(SphereCollider sphere, AABB_Collider aabb,out Vector3 closestPoint)
    {
        closestPoint = GetClosestPointOnAABB(sphere,aabb);
        float distance = (closestPoint - sphere.transform.position).magnitude - sphere.Radius;

        if(aabb.GetComponent<WorldBounds>())
        {
            if(distance > 0)
            {
                RemovePhysicsObject(sphere);
                Destroy(sphere.gameObject);
            }
        }

        if (distance < 0)
            return true;
        else
            return false;
    }
    private Vector3 GetClosestPointOnAABB(SphereCollider sphere, AABB_Collider aabb)
    {
        Vector3 closestPoint = sphere.transform.position - aabb.transform.position;
        closestPoint = new Vector3(Mathf.Clamp(closestPoint.x, -aabb.X_HalfSize, aabb.X_HalfSize), Mathf.Clamp(closestPoint.y, -aabb.Y_HalfSize, aabb.Y_HalfSize), Mathf.Clamp(closestPoint.z, -aabb.Z_HalfSize, aabb.Z_HalfSize));
        closestPoint = aabb.transform.position + closestPoint ;
        return closestPoint;
    }
    private void Seperate(SphereCollider sphere, AABB_Collider aabb, Vector3 closestPoint)
    {

        Vector3 direction = sphere.transform.position - closestPoint;
        Vector3 seperationPoint = closestPoint + direction.normalized * (sphere.Radius + 0.00001f) ;
        sphere.transform.position = seperationPoint;
    }
    private void ApplyCollision(SphereCollider sphere, AABB_Collider aabb, Vector3 closestPoint)
    {
        float CollisionForce = sphere.rb.LinearVelocity.magnitude * sphere.rb.Mass;
        Vector3 delta = sphere.transform.position - closestPoint;
        Vector3 normal = delta.normalized * CollisionForce;
        //normal = new Vector3(sphere.rb.LinearVelocity.x, normal.y, sphere.rb.LinearVelocity.z) * sphere.rb.inverseMass;

        normal = normal * sphere.Bounciness * sphere.rb.InverseMass;
        sphere.rb.LinearVelocity += normal * sphere.rb.InverseMass;
        AddTorque(normal, sphere, closestPoint);
    }
    private void AddTorque(Vector3 force, SphereCollider sphere, Vector3 closestPoint)
    {
        Vector3 distance = closestPoint - sphere.transform.position;
        Vector3 torque = Vector3.Cross(distance, force);
        sphere.rb.AngularVelocity = torque * sphere.rb.InverseMass;
    }
    #endregion
    #region SphereToPlaneCollision
    private void CheckForCollision(SphereCollider sphere, Plane plane)
    {
        CheckDistance(sphere, plane);
    }
    private void CheckDistance(SphereCollider sphere, Plane plane)
    {
        Vector3 normal = plane.transform.up;
        float distance = Vector.Dot(normal, sphere.transform.position - plane.transform.position);
        distance -= sphere.Radius;
        Vector3 closestPointOnSphere = sphere.transform.position + sphere.Radius * (-plane.transform.up);
        Vector3 closestPointOnPlane = Vector.ProjectOnPlane(closestPointOnSphere,plane.transform.up);
        if (distance <= 0)
        {
            Seperate(sphere, plane, closestPointOnPlane);
            ApplyCollision(sphere,plane, closestPointOnPlane, closestPointOnSphere);
        }
    }
    private void Seperate(SphereCollider sphere,Plane plane, Vector3 closestPointOnPlane)
    {
        sphere.transform.position = closestPointOnPlane + plane.transform.up.normalized * (sphere.Radius + 0.05f);
    }
    private void ApplyCollision(SphereCollider sphere, Plane plane,Vector3 normal, Vector3 closestPoint)
    {
        float collisionForce = sphere.rb.LinearVelocity.magnitude * sphere.rb.Mass;
        Vector3 force = plane.transform.up * collisionForce;
        sphere.rb.LinearVelocity += force;
        AddTorque(sphere.rb.LinearVelocity , sphere, closestPoint);
    }
    #endregion
    public void AddPhysicsObject(Collider physicsObject)
    {
        if (physicsObject == null)
            return;
        if (physicalObjects.Contains(physicsObject))
            return;

        physicalObjects.Add(physicsObject);
    }

    public void RemovePhysicsObject(Collider physicsObject)
    {
        if (physicsObject == null)
            return;
        if (physicalObjects.Contains(physicsObject))
            physicalObjects.Remove(physicsObject);
        Destroy(physicsObject);
          
    }

}
