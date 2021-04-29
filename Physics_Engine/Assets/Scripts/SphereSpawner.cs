using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject bombPrefab;
    private float timeUntilNextSpawn;
    public float timeBetweenSpawns;
    public List<Transform> spawnPoints;
    private void Start()
    {
    }
    private void FixedUpdate()
    {
        timeUntilNextSpawn -= Time.deltaTime;
        if(timeUntilNextSpawn <= 0)
        {
            int rand = Random.Range(0, 10);
            if (rand < 5)
            {
                SpawnSphere();
            }
            else if(rand > 5)
            {
                SpawnBomb();
            }
            timeUntilNextSpawn = timeBetweenSpawns;
        }
    }
    void SpawnSphere()
    {
        int rand = Mathf.RoundToInt(Random.Range(0f, spawnPoints.Count));
        GameObject sphere = Instantiate(spherePrefab, spawnPoints[rand].position, Quaternion.identity);
        float f = Random.Range(1f, 2f);
        sphere.GetComponent<MyRigidbody>().mass = f;
        sphere.GetComponent<Sphere>().SetScale(f);
        sphere.GetComponent<Sphere>().bounciness = Random.Range(0.5f, 1f);
        sphere = new GameObject();
    }
    void SpawnBomb()
    {
        int rand = Mathf.RoundToInt(Random.Range(0f, spawnPoints.Count));
        Instantiate(bombPrefab, spawnPoints[rand].position, Quaternion.identity);
    }
}
