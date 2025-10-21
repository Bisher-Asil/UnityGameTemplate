using UnityEngine;
using System.Collections;

/// <summary>
/// Manages visual effects and particle systems throughout the game
/// </summary>
public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }

    [Header("Particle Prefabs")]
    [SerializeField] private GameObject shootEffectPrefab;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private GameObject deathExplosionPrefab;
    [SerializeField] private GameObject pointCollectEffectPrefab;

    [Header("Settings")]
    [SerializeField] private float effectLifetime = 2f;

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

    public void PlayShootEffect(Vector3 position)
    {
        if (shootEffectPrefab != null)
        {
            CreateEffect(shootEffectPrefab, position);
        }
        else
        {
            // Fallback: Simple visual indicator
            CreateSimpleFlash(position, Color.cyan, 0.3f);
        }
    }

    public void PlayHitEffect(Vector3 position)
    {
        if (hitEffectPrefab != null)
        {
            CreateEffect(hitEffectPrefab, position);
        }
        else
        {
            // Fallback: Simple visual indicator
            CreateSimpleFlash(position, Color.red, 0.2f);
        }
    }

    public void PlayDeathExplosion(Vector3 position)
    {
        if (deathExplosionPrefab != null)
        {
            CreateEffect(deathExplosionPrefab, position);
        }
        else
        {
            // Fallback: Multiple colored particles
            for (int i = 0; i < 8; i++)
            {
                Vector3 offset = Random.insideUnitCircle * 0.5f;
                CreateSimpleFlash(position + offset, Color.yellow, 0.5f);
            }
        }
    }

    public void PlayPointCollectEffect(Vector3 position)
    {
        if (pointCollectEffectPrefab != null)
        {
            CreateEffect(pointCollectEffectPrefab, position);
        }
        else
        {
            // Fallback: Simple visual indicator
            CreateSimpleFlash(position, Color.green, 0.3f);
        }
    }

    private void CreateEffect(GameObject prefab, Vector3 position)
    {
        GameObject effect = Instantiate(prefab, position, Quaternion.identity);
        Destroy(effect, effectLifetime);
    }

    private void CreateSimpleFlash(Vector3 position, Color color, float duration)
    {
        GameObject flash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        flash.transform.position = position;
        flash.transform.localScale = Vector3.one * 0.3f;
        
        Renderer renderer = flash.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }

        // Remove collider
        Collider collider = flash.GetComponent<Collider>();
        if (collider != null)
        {
            Destroy(collider);
        }

        StartCoroutine(FadeAndDestroy(flash, duration));
    }

    private IEnumerator FadeAndDestroy(GameObject obj, float duration)
    {
        float elapsed = 0f;
        Renderer renderer = obj.GetComponent<Renderer>();
        Color startColor = renderer != null ? renderer.material.color : Color.white;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - (elapsed / duration);
            
            if (renderer != null)
            {
                Color color = startColor;
                color.a = alpha;
                renderer.material.color = color;
            }

            // Scale down
            float scale = Mathf.Lerp(0.3f, 0.1f, elapsed / duration);
            obj.transform.localScale = Vector3.one * scale;

            yield return null;
        }

        Destroy(obj);
    }
}
