using UnityEditor.Rendering;
using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager>
{
    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    void Awake()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }
    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        originalPosition = transform.localPosition;

        // Generate a single random direction
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector3 shakeOffset = new Vector3(direction.x, direction.y, 0) * magnitude;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localPosition = originalPosition + shakeOffset;
            yield return null;
            elapsed += Time.deltaTime;
        }

        transform.localPosition = originalPosition;
    }

}


