using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private PlayerController player;
    [SerializeField] private int damage;
    [SerializeField] private CameraController camShaker;

    private Vector3 moveDirection;
    private ParticleSystem spawnedExpolisonParticles;
    private bool hasDied = false;

    public int health;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        camShaker = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    private void Update()
    {
        moveDirection = playerTransform.position - transform.position;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hasDied)
        {
            Invoke("Die", 2f);
            player.health -= damage;
            sprite.enabled = false;
            spawnedExpolisonParticles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            spawnedExpolisonParticles.Play();
            camShaker.Shake();
            hasDied = true;
        }

        if (collision.gameObject.tag == "Enemy" && !hasDied)
        {
            Invoke("Die", 7f);
            player.killCount += 1;
            sprite.enabled = false;
            spawnedExpolisonParticles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            spawnedExpolisonParticles.Play();
            camShaker.Shake();
            collision.GetComponent<BulletController>().DieInstant();
            hasDied = true;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Destroy(spawnedExpolisonParticles.gameObject);
    }
}
