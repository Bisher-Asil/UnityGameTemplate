using UnityEngine;

/// <summary>
/// Bootstrap script to initialize game managers and scene setup
/// </summary>
public class GameBootstrapper : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("[GameBootstrapper] Initializing game...");

        // Ensure all required managers exist
        if (GameManager.Instance == null)
        {
            Debug.LogError("[GameBootstrapper] GameManager not found!");
        }

        if (CurrencyManager.Instance == null)
        {
            Debug.LogError("[GameBootstrapper] CurrencyManager not found!");
        }

        if (UpgradeManager.Instance == null)
        {
            Debug.LogError("[GameBootstrapper] UpgradeManager not found!");
        }

        if (DataManager.Instance == null)
        {
            Debug.LogError("[GameBootstrapper] DataManager not found!");
        }

        if (SceneTransitionManager.Instance == null)
        {
            Debug.LogError("[GameBootstrapper] SceneTransitionManager not found!");
        }

        // Load game data
        if (DataManager.Instance != null)
        {
            DataManager.Instance.LoadGame();
        }

        Debug.Log("[GameBootstrapper] Game initialization complete");
    }
}
