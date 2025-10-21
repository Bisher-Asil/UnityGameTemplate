using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Handles saving and loading game data
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private const string POINTS_KEY = "player_points";
    private const string UPGRADE_LEVEL_KEY = "upgrade_level_";
    private const string ROUND_KEY = "player_round";

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

    public void SaveGame()
    {
        // Save points
        if (CurrencyManager.Instance != null)
        {
            long points = CurrencyManager.Instance.GetCurrentPoints();
            PlayerPrefs.SetString(POINTS_KEY, points.ToString());
        }

        // Save upgrade levels
        if (UpgradeManager.Instance != null)
        {
            var upgrades = UpgradeManager.Instance.GetAllUpgrades();
            foreach (var upgrade in upgrades)
            {
                PlayerPrefs.SetInt(UPGRADE_LEVEL_KEY + upgrade.Key, upgrade.Value.currentLevel);
            }
        }

        PlayerPrefs.Save();
        Debug.Log("[DataManager] Game saved");
    }

    public void LoadGame()
    {
        // Load points
        if (PlayerPrefs.HasKey(POINTS_KEY))
        {
            if (long.TryParse(PlayerPrefs.GetString(POINTS_KEY), out long points))
            {
                CurrencyManager.Instance.SetPoints(points);
                Debug.Log($"[DataManager] Loaded points: {points}");
            }
        }

        // Load upgrade levels
        if (UpgradeManager.Instance != null)
        {
            var upgrades = UpgradeManager.Instance.GetAllUpgrades();
            foreach (var upgrade in upgrades)
            {
                if (PlayerPrefs.HasKey(UPGRADE_LEVEL_KEY + upgrade.Key))
                {
                    int level = PlayerPrefs.GetInt(UPGRADE_LEVEL_KEY + upgrade.Key);
                    UpgradeManager.Instance.SetUpgradeLevel(upgrade.Key, level);
                }
            }
        }

        Debug.Log("[DataManager] Game loaded");
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        CurrencyManager.Instance.SetPoints(0);

        if (UpgradeManager.Instance != null)
        {
            var upgrades = UpgradeManager.Instance.GetAllUpgrades();
            foreach (var upgrade in upgrades)
            {
                UpgradeManager.Instance.SetUpgradeLevel(upgrade.Key, 0);
            }
        }

        Debug.Log("[DataManager] Game reset");
    }
}
