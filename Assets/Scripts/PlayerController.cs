using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private CameraController cameraShaker;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI healthCountText;

    private float xInput, yInput;
    private Vector3 moveDirection;

    public int health = 10;
    public int killCount = 0;

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(xInput, yInput, 0f).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if (health <= 0) Die();
        killCountText.text = killCount.ToString();
        healthCountText.text = health.ToString();
    }

    private void Die()
    {
        return;
    }
}