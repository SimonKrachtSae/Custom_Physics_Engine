using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class UltimatePhysicsManager : MonoBehaviour
{
    public static UltimatePhysicsManager Instance { get; private set; }
    public List<PhysicsObject> physicalObjects;
    public Vector3 directionBetweenSpheres;
    public float realDistance;

    private bool isSphereAndAabbColliding = false;
    private Vector3 directionBetweenSphereAndAabb = new Vector3();
    private Vector3 closestPointBetweenSphereAndAabb = new Vector3();

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
                CheckForCollision(physicalObjects[i], physicalObjects[j]);
            }
        }
    }
    private void CheckForCollision(PhysicsObject a, PhysicsObject b)
    {
        if(a is Sphere)
        {
            if(b is AABB)
            {
                GenerateCollisionBetweenSphereAndAabb(a, b);
            }
            else if(b is Sphere)
            {
                GenerateCollisionBetweenSpheres(a, b);
            }
        }
        else if(a is AABB)
        {
            if(b is Sphere)
            {
                GenerateCollisionBetweenSphereAndAabb(b, a);
            }
            //if(a is AABB)
            //{
            //    return;
            //}
        }
    }
    private void GenerateCollisionBetweenSpheres(PhysicsObject a, PhysicsObject b)
    {
        directionBetweenSpheres = Vector.GetDirectionVector(a.transform.position,b.transform.position);
        CheckDistanceBetweenSpheres(a,b);
    }
    private void ApplyCollisionOnSpheres(PhysicsObject a, PhysicsObject b)
    {
        float collisionForce = a.Speed * a.rb.mass + b.Speed * b.rb.mass;
        Vector3 delta = (directionBetweenSpheres + a.rb.linearAcceleration + b.rb.linearAcceleration).normalized;
        Vector3 normal = delta * collisionForce;
        a.rb.linearAcceleration = -normal * a.rb.inverseMass;
        b.rb.linearAcceleration = normal * b.rb.inverseMass;
    }
    private void SeperateSpheres(PhysicsObject a, PhysicsObject b)
    {
        Vector3 seperationPoint = b.transform.position - directionBetweenSpheres.normalized * (a.radius + b.radius);
        a.transform.position = seperationPoint;
    }
    private void CheckDistanceBetweenSpheres(PhysicsObject a, PhysicsObject b)
    {
        float middlepointDistance = directionBetweenSpheres.magnitude;
        realDistance = middlepointDistance - a.radius - b.radius;
        
        if (realDistance < 0)
        {
            SeperateSpheres(a, b);
            ApplyCollisionOnSpheres(a, b);
        }
    }
    private void GenerateCollisionBetweenSphereAndAabb(PhysicsObject sphere, PhysicsObject aabb)
    {
        CheckForCollisionBetweenSphereAndAabb(sphere,aabb);
        if (isSphereAndAabbColliding)
        {
            Seperate(sphere, aabb);
            ApplyCollision(sphere, aabb);
        }
    }
    private void CheckForCollisionBetweenSphereAndAabb(PhysicsObject sphere, PhysicsObject aabb)
    {
        SetClosestPoint(sphere,aabb);
        float distance = Vector.GetDirectionVector(sphere.transform.position, closestPointBetweenSphereAndAabb).magnitude - sphere.radius;
        if (distance < 0)
        {
            isSphereAndAabbColliding = true;
        }
        else
        {
            isSphereAndAabbColliding = false;
        }
    }
    private void Seperate(PhysicsObject sphere, PhysicsObject Aabb)
    {
        directionBetweenSphereAndAabb = sphere.transform.position - closestPointBetweenSphereAndAabb;
        Vector3 seperationPoint = closestPointBetweenSphereAndAabb + directionBetweenSphereAndAabb.normalized * (sphere.radius);
        sphere.transform.position = seperationPoint;
    }
    private void ApplyCollision(PhysicsObject sphere, PhysicsObject aabb)
    {
        float CollisionForce = sphere.Speed * sphere.rb.mass;
        Vector3 delta = sphere.transform.position - aabb.transform.position;
        Vector3 normal = delta * CollisionForce;
        sphere.rb.linearAcceleration = normal * sphere.rb.inverseMass;
    }
    private void SetClosestPoint(PhysicsObject sphere, PhysicsObject aabb)
    {
        Matrix trs = Matrix.TRS(aabb.transform.position, aabb.transform.localEulerAngles, aabb.transform.lossyScale);
        closestPointBetweenSphereAndAabb = Vector.GetDirectionVector(aabb.transform.position, sphere.transform.position);
        closestPointBetweenSphereAndAabb = new Vector3(Mathf.Clamp(closestPointBetweenSphereAndAabb.x, -aabb.xHalfSize, aabb.xHalfSize), Mathf.Clamp(closestPointBetweenSphereAndAabb.y, -aabb.yHalfSize, aabb.yHalfSize), Mathf.Clamp(closestPointBetweenSphereAndAabb.z, -aabb.zHalfSize, aabb.zHalfSize));
        closestPointBetweenSphereAndAabb = trs * closestPointBetweenSphereAndAabb;
    }

    public void AddRigidbody(PhysicsObject physicsObject)
    {
        if (physicsObject == null)
            return;
        if (physicalObjects.Contains(physicsObject))
            return;

        physicalObjects.Add(physicsObject);
    }

    public void RemoveRigidbody(PhysicsObject physicsObject)
    {
        if (physicsObject == null)
            return;
        if (physicalObjects.Contains(physicsObject))
            return;
        physicalObjects.Add(physicsObject);
    }
}
