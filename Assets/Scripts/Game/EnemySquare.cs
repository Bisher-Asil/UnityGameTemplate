using UnityEngine;
using System;

/// <summary>
/// Represents an enemy square that the player shoots
/// </summary>
public class EnemySquare : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CircleCollider2D circleCollider;

    private float currentHealth;
    private Color originalColor;

    public event Action OnDestroyed;

    private void Start()
    {
        currentHealth = maxHealth;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (circleCollider == null)
            circleCollider = GetComponent<CircleCollider2D>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        Debug.Log($"[EnemySquare] Created with {maxHealth} health");
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;

        // Visual feedback - flash color
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashDamage());
        }

        Debug.Log($"[EnemySquare] Took {damage} damage. Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private System.Collections.IEnumerator FlashDamage()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
    }

private void Die()
    {
        Debug.Log("[EnemySquare] Destroyed");

        // Play death explosion and sound
        if (VFXManager.Instance != null)
        {
            VFXManager.Instance.PlayDeathExplosion(transform.position);
        }
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayExplosionSound();
        }
        if (ScreenShake.Instance != null)
        {
            ScreenShake.Instance.ShakeLight();
        }

        // Drop points
        if (PointDropper.Instance != null)
        {
            PointDropper.Instance.DropPoints(transform.position, 10);
        }

        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}
