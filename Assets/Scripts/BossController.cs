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

    private bool hasDied = false;
    private ParticleSystem spawnedParticlesExplosion;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        camShaker = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance > stoppingDistance) 
            transform.position += (playerTransform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        if (health <= 0 && !hasDied)
            Die();
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
    }

    private void KillParticle() 
    {
        Destroy(spawnedParticlesExplosion, 7f);
    }
}
