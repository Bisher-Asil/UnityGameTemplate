using UnityEngine;
using System.Collections;

/// <summary>
/// Handles camera shake and screen effects for game feel
/// </summary>
public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }

    private Camera mainCamera;
    private Vector3 originalPosition;
    private bool isShaking = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            originalPosition = mainCamera.transform.position;
        }
    }

    public void Shake(float duration = 0.15f, float magnitude = 0.1f)
    {
        if (!isShaking)
        {
            StartCoroutine(DoShake(duration, magnitude));
        }
    }

    public void ShakeLight()
    {
        Shake(0.1f, 0.05f);
    }

    public void ShakeMedium()
    {
        Shake(0.2f, 0.15f);
    }

    public void ShakeHeavy()
    {
        Shake(0.3f, 0.25f);
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
                yield break;
        }

        isShaking = true;
        originalPosition = mainCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.position = new Vector3(
                originalPosition.x + x,
                originalPosition.y + y,
                originalPosition.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Return to original position
        mainCamera.transform.position = originalPosition;
        isShaking = false;
    }
}
