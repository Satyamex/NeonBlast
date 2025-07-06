using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }

    public void Die()
    {
        Destroy(gameObject, 15f);
    }

    public void DieInstant()
    {
        Destroy(gameObject);
    }
}
