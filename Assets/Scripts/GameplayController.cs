using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject boss1;

    private Vector3 bossSpawnPosition;
    private bool bossSpawned = false;

    private void Update()
    {
        if (player.killCount >= 4 && bossSpawned == false) 
        { 
            enemySpawner.canSpawnFromBoss = false;
            bossSpawned = true;
            SpawnBoss();
        }
    }

    private void SpawnBoss() 
    {
        bossSpawnPosition = enemySpawner.GetSpawnPosition(enemySpawner.chosenSpawnPoint);
        GameObject spawnedBoss = Instantiate(boss1, bossSpawnPosition, Quaternion.identity);
    }
}
