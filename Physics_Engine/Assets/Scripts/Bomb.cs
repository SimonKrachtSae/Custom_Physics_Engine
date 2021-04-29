using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sphere))]
public class Bomb : MonoBehaviour
{
    private Sphere thisSphere;
    private MeshRenderer meshRenderer;

    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;

    public bool explode = false;

    private float timeBeforeDestroyed = 0.0f;
    private float timeBeforeExplosion = 5.0f;
    bool timesUp = false;
    private void Start()
    {
        thisSphere = GetComponent<Sphere>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        //GetObjectsInRange();
        if(explode)
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
                float distance = (transform.position - PhysicsManager.Instance.physicalObjects[i].rb.transform.position).magnitude - thisSphere.radius; 
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
    void AddExplosionForce(PhysicsObject physicsObject)
    {
        Vector3 direction = transform.position - physicsObject.transform.position;
        float newDistance = direction.magnitude - thisSphere.radius;
        direction = direction.normalized;
        physicsObject.rb.LinearVelocity -= (direction * explosionForce) * physicsObject.rb.inverseMass;
    }
}
