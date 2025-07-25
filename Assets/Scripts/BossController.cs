using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private int health;
    [SerializeField] private PlayerController player;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private CameraController camShaker;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootTimer;
    [SerializeField] private Transform shootTrajectory;
    [SerializeField] private float teleportTimer;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private SpriteRenderer attackIndicator;

    private bool hasDied = false;
    private ParticleSystem spawnedParticlesExplosion;
    private GameObject spawnedBullet;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        camShaker = GameObject.Find("Main Camera").GetComponent<CameraController>();
        enemySpawner = Camera.main.transform.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    private void Start()
    {
        InvokeRepeating("ChangeLocation", teleportTimer, teleportTimer);
        InvokeRepeating("HandleAttackCycle", shootTimer, shootTimer);
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance > stoppingDistance)
            transform.position += (playerTransform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        if (health <= 0 && !hasDied)
            Die();

        Vector2 direction = (playerTransform.position - shootTrajectory.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shootTrajectory.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !hasDied)
        {
            health -= 1;
            collision.GetComponent<BulletController>().DieInstant();
        }
    }

    private void Die()
    {
        hasDied = true;
        Destroy(gameObject, 7f);
        sprite.enabled = false;
        player.killCount += 1;
        spawnedParticlesExplosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        KillParticle();
        camShaker.Shake();
        enemySpawner.canSpawnFromBoss = true;
        enemySpawner.AllowSpawning();
        player.health += 5;
    }

    private void KillParticle()
    {
        Destroy(spawnedParticlesExplosion, 7f);
    }

    private void ShootPlayer()
    {
       if (!hasDied) spawnedBullet = Instantiate(bullet, transform.position, shootTrajectory.rotation);
    }

    private void ChangeLocation() 
    {
        if (hasDied) return;
        Vector3 spawnPos = enemySpawner.GetSpawnPosition(enemySpawner.chosenSpawnPoint);
        spawnedParticlesExplosion = Instantiate(explosionParticle, transform.position, shootTrajectory.rotation);
        KillParticle();
        transform.position = spawnPos;
    }

    private void NoAttackIndicate()
    {
        attackIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void HandleAttackCycle()
    {
        if (hasDied) return;
        attackIndicator.GetComponent<SpriteRenderer>().enabled = true;
        Invoke("ShootPlayer", 0.25f);
        Invoke("NoAttackIndicate", 0.5f);
    }

}
