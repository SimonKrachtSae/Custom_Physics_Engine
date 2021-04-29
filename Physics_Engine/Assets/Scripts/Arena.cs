using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AABB))]
public class Arena : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
}
