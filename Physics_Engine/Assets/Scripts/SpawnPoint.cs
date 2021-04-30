using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        SphereSpawner.Instance.AddSpawnPoint(this.transform);
    }
    private void OnDestroy()
    {
        SphereSpawner.Instance.RemoveSpawnPoint(this.transform);
    }
}
