using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager Instance { get; private set; }
    public List<PhysicsObject> physicalObjects;
    private bool isColliding = false;
    private Vector3 closestPoint = new Vector3();
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        physicalObjects = new List<PhysicsObject>();

    }
    private void FixedUpdate()
    {
        for (int i = 0; i < physicalObjects.Count; i++)
        {
            for(int j = i + 1; j < physicalObjects.Count; j++)
            {
                closestPoint = Vector3.zero;
                CheckForCollision(physicalObjects[i], physicalObjects[j]);
            }
        }
    }
    private void CheckForCollision(PhysicsObject a, PhysicsObject b)
    {
        if (a.isTrigger || b.isTrigger)
            return;

        if(a is Sphere)
        {
            if(b is AABB)
            {
                GenerateCollision((Sphere)a, (AABB)b);
            }
            else if(b is Sphere)
            {
                if(b.GetComponent<Bomb>())
                {
                    b.GetComponent<Bomb>().explode = true;
                }
                GenerateCollision((Sphere)a, (Sphere)b);
            }
        }
        else if(a is AABB )
        {
            if(b is Sphere)
            {
                GenerateCollision((Sphere)b, (AABB)a);
            }
        }
    }

    #region SphereToShereCollision
    private void GenerateCollision(Sphere a, Sphere b)
    {
        Vector3 direction = Vector.GetDirectionVector(a.transform.position,b.transform.position);
        CheckDistance(a,b, direction);
    }
    private void CheckDistance(Sphere a, Sphere b,Vector3 direction)
    {
        float middlepointDistance = direction.magnitude;
        float realDistance = middlepointDistance - a.radius - b.radius;
        
        if (realDistance < 0)
        {
            Seperate(a, b, direction);
            ApplyCollision(a, b, direction);
        }
    }
    private void Seperate(Sphere a, Sphere b, Vector3 direction)
    {
        Vector3 seperationPoint = b.transform.position - direction.normalized * (a.radius + b.radius);
        a.transform.position = seperationPoint;
    }
    private void ApplyCollision(Sphere a, Sphere b, Vector3 direction)
    {
        float collisionForce = a.rb.LinearVelocity.magnitude * a.rb.mass + b.rb.LinearVelocity.magnitude * b.rb.mass;
        Vector3 delta = (direction + a.rb.LinearVelocity + b.rb.LinearVelocity).normalized;
        Vector3 normal = delta * collisionForce;
        a.rb.LinearVelocity = -normal * a.rb.inverseMass;
        b.rb.LinearVelocity = normal * b.rb.inverseMass;
    }
    #endregion
    
    #region SphereToAABBCollision
    private void GenerateCollision(Sphere sphere, AABB aabb)
    {
        isColliding = false;
        CheckForCollision(sphere,aabb);
        if (isColliding)
        {
            Seperate(sphere, aabb);
            ApplyCollision(sphere, aabb);
        }
    }
    private void CheckForCollision(Sphere sphere, AABB aabb)
    {
        SetClosestPointOnAABB(sphere,aabb);
        float distance = (closestPoint - sphere.transform.position).magnitude - sphere.radius;
        if (distance < 0)
            isColliding = true;
        else
            isColliding = false;
    }
    private void SetClosestPointOnAABB(Sphere sphere, AABB aabb)
    {
        closestPoint = Vector.GetDirectionVector(aabb.transform.position, sphere.transform.position);
        closestPoint = new Vector3(Mathf.Clamp(closestPoint.x, -aabb.X_HalfSize, aabb.X_HalfSize), Mathf.Clamp(closestPoint.y, -aabb.Y_HalfSize, aabb.Y_HalfSize), Mathf.Clamp(closestPoint.z, -aabb.Z_HalfSize, aabb.Z_HalfSize));
        closestPoint = aabb.transform.position + closestPoint;
    }
    private void Seperate(Sphere sphere, AABB aabb)
    {
        float newX = sphere.transform.position.x;
        float newY = sphere.transform.position.y;
        float newZ = sphere.transform.position.z;

        Vector3 direction = sphere.transform.position - closestPoint;
        Vector3 seperationPoint = closestPoint + direction.normalized * (sphere.radius + 0.00001f) ;
        sphere.transform.position = seperationPoint;
    }
    private void ApplyCollision(Sphere sphere, AABB aabb)
    {
        float CollisionForce = sphere.rb.LinearVelocity.magnitude * sphere.rb.mass;
        Vector3 delta = sphere.transform.position - closestPoint;
        Vector3 normal = delta.normalized * CollisionForce;
        normal = new Vector3(sphere.rb.LinearVelocity.x, normal.y, sphere.rb.LinearVelocity.z) * sphere.rb.inverseMass;
        sphere.rb.LinearVelocity = normal * sphere.rb.inverseMass;
        AddTorque(normal, sphere);
    }
    private void AddTorque(Vector3 force, Sphere sphere)
    {
        Vector3 distance = closestPoint - sphere.transform.position;
        Vector3 torque = Vector3.Cross(distance, force);
        sphere.rb.AngularVelocity = torque * sphere.rb.inverseMass;
    }
    public void AddForceAtPosition(Vector3 _force, Vector3 _position, ForceMode _mode)
    {
        Vector3 distance = _position - transform.position;
        Vector3 torque = Vector3.Cross(distance, _force);

        //AddTorque(torque);
        //AddForce(_force, _mode);
    }
    #endregion

    public void AddPhysicsObject(PhysicsObject physicsObject)
    {
        if (physicsObject == null)
            return;
        if (physicalObjects.Contains(physicsObject))
            return;

        physicalObjects.Add(physicsObject);
    }

    public void RemovePhysicsObject(PhysicsObject physicsObject)
    {
        if (physicsObject == null)
            return;
        if (physicalObjects.Contains(physicsObject))
            physicalObjects.Remove(physicsObject);
          
    }

}
