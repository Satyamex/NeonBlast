using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float smoothRate = 5f;

    [Header("Shake Settings")]
    [SerializeField] private float traumaDecay = 1f;
    [SerializeField] private float maxShakeTranslation = 0.5f;
    [SerializeField] private float maxShakeRotation = 5f;
    [SerializeField] private AnimationCurve traumaToShake = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField, Range(0f, 1f)] private float shakeTrauma = 0.3f;

    private float trauma = 0f;
    private float seed;
    private Vector3 shakeOffset;
    private float shakeRotation;

    private void Awake()
    {
        seed = Random.value * 100f;
    }

    private void LateUpdate()
    {
        // Smooth Follow
        Vector3 targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothRate * Time.deltaTime);

        // Camera Shake
        if (trauma > 0f)
        {
            float shakeStrength = traumaToShake.Evaluate(trauma);
            float time = Time.time * 50f;

            float x = maxShakeTranslation * shakeStrength * (Mathf.PerlinNoise(seed, time) * 2 - 1);
            float y = maxShakeTranslation * shakeStrength * (Mathf.PerlinNoise(seed + 1, time) * 2 - 1);
            float rot = maxShakeRotation * shakeStrength * (Mathf.PerlinNoise(seed + 2, time) * 2 - 1);

            shakeOffset = new Vector3(x, y, 0f);
            shakeRotation = rot;

            trauma = Mathf.Clamp01(trauma - traumaDecay * Time.deltaTime);
        }
        else
        {
            shakeOffset = Vector3.zero;
            shakeRotation = 0f;
        }

        // Apply Shake
        transform.position += shakeOffset;
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation);
    }

    public void Shake()
    {
        // Set trauma without stacking
        if (trauma < 0.05f)
        {
            trauma = Mathf.Clamp01(shakeTrauma);
        }
    }
}
