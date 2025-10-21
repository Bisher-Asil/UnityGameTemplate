using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents a single upgrade with its level, cost, and dependencies
/// </summary>
[System.Serializable]
public class Upgrade
{
    public string id;
    public string displayName;
    public string description;
    public int currentLevel = 0;
    public long baseCost = 100;
    public float costMultiplier = 1.5f;
    public string dependsOnUpgradeId; // null if no dependency
    public int dependencyRequiredLevel = 1; // Required level of dependency upgrade

    public long GetCurrentCost()
    {
        if (currentLevel == 0)
            return baseCost;

        return (long)(baseCost * Mathf.Pow(costMultiplier, currentLevel));
    }

    public bool CanAfford(long playerPoints)
    {
        return playerPoints >= GetCurrentCost();
    }

    public bool IsDependencyMet(Dictionary<string, Upgrade> allUpgrades)
    {
        if (string.IsNullOrEmpty(dependsOnUpgradeId))
            return true; // No dependency

        if (!allUpgrades.ContainsKey(dependsOnUpgradeId))
            return false;

        return allUpgrades[dependsOnUpgradeId].currentLevel >= dependencyRequiredLevel;
    }
}

/// <summary>
/// Manages all upgrades and their progression
/// </summary>
public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    private Dictionary<string, Upgrade> upgrades = new Dictionary<string, Upgrade>();

    // Events
    public static event Action<string> OnUpgradePurchased;
    public static event Action OnUpgradesChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeUpgrades();
    }

    private void InitializeUpgrades()
    {
        // Damage Upgrade - No dependency
        upgrades["damage"] = new Upgrade
        {
            id = "damage",
            displayName = "Weapon Damage",
            description = "Increase shooting damage",
            currentLevel = 0,
            baseCost = 50,
            costMultiplier = 1.5f,
            dependsOnUpgradeId = null
        };

        // Speed Upgrade - Depends on Damage
        upgrades["speed"] = new Upgrade
        {
            id = "speed",
            displayName = "Fire Rate",
            description = "Decrease time between shots",
            currentLevel = 0,
            baseCost = 100,
            costMultiplier = 1.5f,
            dependsOnUpgradeId = "damage",
            dependencyRequiredLevel = 1
        };

        // Size Upgrade - Depends on Speed
        upgrades["size"] = new Upgrade
        {
            id = "size",
            displayName = "Cursor Size",
            description = "Increase cursor size",
            currentLevel = 0,
            baseCost = 150,
            costMultiplier = 1.5f,
            dependsOnUpgradeId = "speed",
            dependencyRequiredLevel = 1
        };

        Debug.Log("[UpgradeManager] Upgrades initialized");
    }

    public Upgrade GetUpgrade(string upgradeId)
    {
        if (upgrades.ContainsKey(upgradeId))
            return upgrades[upgradeId];

        Debug.LogWarning($"[UpgradeManager] Upgrade '{upgradeId}' not found");
        return null;
    }

    public Dictionary<string, Upgrade> GetAllUpgrades()
    {
        return new Dictionary<string, Upgrade>(upgrades);
    }

    public bool TryPurchaseUpgrade(string upgradeId)
    {
        if (!upgrades.ContainsKey(upgradeId))
        {
            Debug.LogWarning($"[UpgradeManager] Upgrade '{upgradeId}' not found");
            return false;
        }

        Upgrade upgrade = upgrades[upgradeId];

        // Check if dependency is met
        if (!upgrade.IsDependencyMet(upgrades))
        {
            Debug.Log($"[UpgradeManager] Cannot purchase {upgradeId}: dependency not met");
            return false;
        }

        // Check if player can afford it
        long cost = upgrade.GetCurrentCost();
        if (!CurrencyManager.Instance.TrySpendPoints(cost))
        {
            Debug.Log($"[UpgradeManager] Cannot afford {upgradeId}");
            return false;
        }

        // Apply upgrade
        upgrade.currentLevel++;
        OnUpgradePurchased?.Invoke(upgradeId);
        OnUpgradesChanged?.Invoke();

        Debug.Log($"[UpgradeManager] Purchased {upgradeId}. New level: {upgrade.currentLevel}");
        return true;
    }

    public int GetUpgradeLevel(string upgradeId)
    {
        if (upgrades.ContainsKey(upgradeId))
            return upgrades[upgradeId].currentLevel;

        return 0;
    }

    public void SetUpgradeLevel(string upgradeId, int level)
    {
        if (upgrades.ContainsKey(upgradeId))
        {
            upgrades[upgradeId].currentLevel = Mathf.Max(0, level);
            OnUpgradesChanged?.Invoke();
        }
    }
}
