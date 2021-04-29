using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trigger : MonoBehaviour
{
    public PhysicsObject thisObject;
    public event Action OnTriggerEnter;
    private void MyOnTriggerEnter()
    {
        if(OnTriggerEnter != null)
        {
            OnTriggerEnter();
        }
    }
    private void Start()
    {
        if(thisObject == null)
            thisObject = GetComponent<PhysicsObject>();

        if(thisObject is Sphere)
        {
            thisObject = (Sphere)thisObject;
        }
        else if (thisObject is AABB)
        {
            thisObject = (AABB)thisObject;
        }
        else if(thisObject is Plane)
        {
            thisObject = (Plane)thisObject;
        }
    }
  
    private void OnDrawGizmos()
    {
    }
}
