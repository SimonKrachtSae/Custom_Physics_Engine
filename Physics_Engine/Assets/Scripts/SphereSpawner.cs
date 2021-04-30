using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public static SphereSpawner Instance { get; private set; }

    [SerializeField] public GameObject spherePrefab;
    [SerializeField] public GameObject bombPrefab;
    private float timeUntilNextSpawn;
    public float timeBetweenSpawns;
    public List<Transform> spawnPoints;
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        spawnPoints = new List<Transform>();
    }
    private void FixedUpdate()
    {
        timeUntilNextSpawn -= Time.deltaTime;
        if(timeUntilNextSpawn <= 0)
        {
            int rand = Random.Range(0, 10);
            if (rand < 6)
            {
                SpawnSphere();
            }
            else if(rand >= 6)
            {
                SpawnBomb();
            }
            timeUntilNextSpawn = timeBetweenSpawns;
        }
    }
    void SpawnSphere()
    {
        int rand = Random.Range(0, spawnPoints.Count);
        GameObject sphere = Instantiate(spherePrefab, spawnPoints[rand].position, Quaternion.identity);
        float f = Random.Range(1f, 2f);
        sphere.GetComponent<MyRigidbody>().Mass = f;
        sphere.GetComponent<SphereCollider>().SetScale(f);
        sphere.GetComponent<SphereCollider>().Bounciness = Random.Range(0.5f, 1f);
    }
    void SpawnBomb()
    {
        int rand = Mathf.RoundToInt(Random.Range(0f, spawnPoints.Count-1));
        Instantiate(bombPrefab, spawnPoints[rand].position, Quaternion.identity);
    }
    public void AddSpawnPoint(Transform spawnPoint)
    {
        if (spawnPoints == null)
            return;
        if (spawnPoints.Contains(spawnPoint))
            return;

        spawnPoints.Add(spawnPoint);
    }

    public void RemoveSpawnPoint(Transform spawnPoint)
    {
        if (spawnPoints == null)
            return;
        if (spawnPoints.Contains(spawnPoint))
            spawnPoints.Remove(spawnPoint);
        Destroy(spawnPoint);
    }
}
