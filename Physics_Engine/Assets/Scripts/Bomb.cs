using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private SphereCollider thisSphere;
    private MeshRenderer meshRenderer;

    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;

    public bool Explode = false;

    private float timeBeforeDestroyed = 0.0f;
    private float timeBeforeExplosion = 5.0f;
    private bool timesUp = false;
    private void Start()
    {
        thisSphere = GetComponent<SphereCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        //GetObjectsInRange();
        if(Explode)
        {
            timeBeforeExplosion -= Time.deltaTime;
            if (Mathf.Round(timeBeforeExplosion) % 1 == 0 && timeBeforeExplosion != 0.0f)
            {
                if (Mathf.Round(timeBeforeExplosion) % 2 == 0)
                {
                    meshRenderer.material.color = Color.red;
                }
                else
                {
                    meshRenderer.material.color = Color.white;
                }
            }
            if (timeBeforeExplosion <= 0.0f)
            {
                timesUp = true;
            }
            if (timesUp)
            {
                GetObjectsInRange();
                timeBeforeDestroyed += Time.fixedDeltaTime;
            }
        }
        if(timeBeforeDestroyed >= 0.05f)
        {
            Destroy(this.gameObject);
        }
    }
    private void GetObjectsInRange()
    {
        for(int i = 0; i < PhysicsManager.Instance.physicalObjects.Count; i++)
        {
            if (PhysicsManager.Instance.physicalObjects[i] != null)
            {
                float distance = (transform.position - PhysicsManager.Instance.physicalObjects[i].rb.transform.position).magnitude - thisSphere.Radius; 
                if(distance < explosionRadius)
                {
                    if(PhysicsManager.Instance.physicalObjects[i] != this)
                    {
                        AddExplosionForce(PhysicsManager.Instance.physicalObjects[i]);
                    }
                }
            }
        }
    }
    void AddExplosionForce(Collider physicsObject)
    {
        Vector3 direction = transform.position - physicsObject.transform.position;
        float newDistance = direction.magnitude - thisSphere.Radius;
        direction = direction.normalized;
        Vector3 newForce = (direction * explosionForce) * physicsObject.rb.InverseMass;
        physicsObject.rb.LinearVelocity -= newForce;
        if(physicsObject is SphereCollider)
        AddTorque(newForce, physicsObject, direction);
    }
    private void AddTorque(Vector3 force, Collider sphere, Vector3 closestPoint)
    {
        Vector3 distance = closestPoint - sphere.transform.position;
        Vector3 torque = Vector3.Cross(distance, force);
        sphere.rb.AngularVelocity = torque * sphere.rb.InverseMass;
    }
}
