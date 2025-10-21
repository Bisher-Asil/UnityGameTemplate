using UnityEngine;
using System.Collections;

/// <summary>
/// Manages dropping and collecting points when enemies are defeated
/// </summary>
public class PointDropper : MonoBehaviour
{
    public static PointDropper Instance { get; private set; }

    [SerializeField] private GameObject pointPrefab;
    // Note: fallDuration and fallHeight are reserved for future animation features

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void DropPoints(Vector3 position, long pointAmount)
    {
        if (pointPrefab == null)
        {
            // If no prefab, just add points directly
            CurrencyManager.Instance.AddPoints(pointAmount);
            Debug.LogWarning("[PointDropper] No point prefab assigned! Adding points directly.");
            return;
        }

        // Spawn visual point GameObject
        GameObject pointObj = Instantiate(pointPrefab, position, Quaternion.identity);
        StartCoroutine(CollectPointAfterDelay(pointObj, pointAmount, 0.5f));
    }

    private IEnumerator CollectPointAfterDelay(GameObject pointObj, long pointAmount, float delay)
    {
        // Wait for the point to exist for a moment (visual feedback)
        yield return new WaitForSeconds(delay);

        if (pointObj != null)
        {
            // Play point collect effect and sound at the point's position
            Vector3 collectPos = pointObj.transform.position;
            
            if (VFXManager.Instance != null)
            {
                VFXManager.Instance.PlayPointCollectEffect(collectPos);
            }
            if (GameAudioManager.Instance != null)
            {
                GameAudioManager.Instance.PlayPointCollectSound();
            }

            // Destroy the visual point
            Destroy(pointObj);
        }

        // Add points to currency
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddPoints(pointAmount);
        }
    }
}
