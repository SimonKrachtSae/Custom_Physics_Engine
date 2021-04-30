using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AABB_Collider))]
public class WorldBounds : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
}
