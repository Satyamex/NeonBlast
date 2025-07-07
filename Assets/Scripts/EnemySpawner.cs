using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTimer;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject[] spawnPoints = new GameObject[16];

    public bool canSpawn = true;
    public bool canSpawnFromBoss = true;
    public int chosenSpawnPoint;

    private void Update()
    {
        chosenSpawnPoint = Random.Range(0, 16);
        if (canSpawn) SpawnEnemy();
    }

    private void SpawnEnemy() 
    {
        Vector3 spawnPosition = GetSpawnPosition(chosenSpawnPoint);
        GameObject enemy = Instantiate(enemy1, spawnPosition, Quaternion.identity);
        canSpawn = false;
        Invoke("AllowSpawning", spawnTimer);
    }

    public Vector3 GetSpawnPosition(int chosenSpawnPoint)
    {
        return spawnPoints[chosenSpawnPoint].transform.position;
    }

    public void AllowSpawning() 
    {
        if (canSpawnFromBoss) canSpawn = true;
    }
}

