using UnityEngine;
using System;

/// <summary>
/// Handles automatic shooting from the mouse cursor
/// </summary>
public class AutoShooter : MonoBehaviour
{
    public static AutoShooter Instance { get; private set; }

    [SerializeField] private float baseShootCooldown = 1f;
    [SerializeField] private float baseDamage = 5f;
    [SerializeField] private float damageUpgradeMultiplier = 2.5f;
    [SerializeField] private float speedUpgradeMultiplier = 0.1f;

    private float shootCooldown;
    private float shootTimer = 0f;

    // Events
    public static event Action OnShot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        shootCooldown = baseShootCooldown;

        // Subscribe to upgrade events
        UpgradeManager.OnUpgradePurchased += OnUpgradePurchased;
    }

    private void OnDestroy()
    {
        UpgradeManager.OnUpgradePurchased -= OnUpgradePurchased;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    private void OnUpgradePurchased(string upgradeId)
    {
        if (upgradeId == "damage" || upgradeId == "speed")
        {
            UpdateShootStats();
        }
    }

    public void UpdateShootStats()
    {
        int damageLevel = UpgradeManager.Instance.GetUpgradeLevel("damage");
        int speedLevel = UpgradeManager.Instance.GetUpgradeLevel("speed");

        // Calculate cooldown with speed upgrades (reduces cooldown)
        shootCooldown = baseShootCooldown - (speedLevel * speedUpgradeMultiplier);
        shootCooldown = Mathf.Max(0.1f, shootCooldown); // Minimum 0.1 second cooldown

        Debug.Log($"[AutoShooter] Updated stats - Damage Level: {damageLevel}, Speed Level: {speedLevel}, Cooldown: {shootCooldown:F2}s");
    }

private void Shoot()
    {
        if (MouseCursor.Instance == null)
            return;

        Vector3 cursorPos = MouseCursor.Instance.transform.position;
        float damage = GetCurrentDamage();

        // Play shoot effect and sound
        if (VFXManager.Instance != null)
        {
            VFXManager.Instance.PlayShootEffect(cursorPos);
        }
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayShootSound();
        }

        // Raycast from cursor position
        RaycastHit2D[] hits = Physics2D.RaycastAll(cursorPos, Vector2.zero, 0.1f);

        bool hitSomething = false;
        foreach (RaycastHit2D hit in hits)
        {
            EnemySquare square = hit.collider?.GetComponent<EnemySquare>();
            if (square != null)
            {
                square.TakeDamage(damage);
                hitSomething = true;

                // Play hit effect and sound
                if (VFXManager.Instance != null)
                {
                    VFXManager.Instance.PlayHitEffect(hit.point);
                }
                if (GameAudioManager.Instance != null)
                {
                    GameAudioManager.Instance.PlayHitSound();
                }
            }
        }

        OnShot?.Invoke();

        if (hitSomething)
        {
            Debug.Log($"[AutoShooter] Shot fired at {cursorPos} - Damage: {damage}");
        }
    }

    public float GetCurrentDamage()
    {
        int damageLevel = UpgradeManager.Instance.GetUpgradeLevel("damage");
        float damage = baseDamage + (damageLevel * damageUpgradeMultiplier);
        return damage;
    }

    public float GetCurrentCooldown()
    {
        return shootCooldown;
    }
}
