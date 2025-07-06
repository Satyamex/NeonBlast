using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gunSprite;
    [SerializeField] private Transform firePointTransform;
    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem gunshotParticles;

    private Vector3 mouseWorldPos;
    private Vector2 gunPointDir;

    private void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gunPointDir = mouseWorldPos - transform.position;
        transform.right = gunPointDir;
        if (mouseWorldPos.x < transform.position.x) transform.localScale = new Vector3(1, -1, 1);
        else transform.localScale = new Vector3(1, 1, 1);
        if (Input.GetMouseButtonDown(0)) 
        {
            GameObject spawnedBullet = Instantiate(bullet, firePointTransform.position, transform.rotation);
            spawnedBullet.GetComponent<BulletController>().Die();
            ParticleSystem spawnedGunshotParticle = 
                Instantiate(gunshotParticles, firePointTransform.position, transform.rotation);
            gunshotParticles.Play();
            Destroy(spawnedGunshotParticle.gameObject, 1f);
        }
    }
}
