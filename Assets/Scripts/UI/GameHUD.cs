using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the in-game HUD displaying stats and controls
/// </summary>
public class GameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI sizeText;
    [SerializeField] private Button storeButton;

    private void Awake()
    {
        // Auto-find UI elements if not assigned
        if (pointsText == null)
            pointsText = transform.Find("PointsText")?.GetComponent<TextMeshProUGUI>();
        if (roundText == null)
            roundText = transform.Find("RoundText")?.GetComponent<TextMeshProUGUI>();
        if (damageText == null)
            damageText = transform.Find("DamageText")?.GetComponent<TextMeshProUGUI>();
        if (speedText == null)
            speedText = transform.Find("SpeedText")?.GetComponent<TextMeshProUGUI>();
        if (sizeText == null)
            sizeText = transform.Find("SizeText")?.GetComponent<TextMeshProUGUI>();
        if (storeButton == null)
            storeButton = transform.Find("StoreButton")?.GetComponent<Button>();

        // Log warnings for any missing references
        if (pointsText == null) Debug.LogWarning("[GameHUD] PointsText not found!");
        if (roundText == null) Debug.LogWarning("[GameHUD] RoundText not found!");
        if (damageText == null) Debug.LogWarning("[GameHUD] DamageText not found!");
        if (speedText == null) Debug.LogWarning("[GameHUD] SpeedText not found!");
        if (sizeText == null) Debug.LogWarning("[GameHUD] SizeText not found!");
        if (storeButton == null) Debug.LogWarning("[GameHUD] StoreButton not found!");
    }

    private void Start()
    {
        // Setup button listener
        if (storeButton != null)
        {
            storeButton.onClick.AddListener(GoToStore);
            Debug.Log("[GameHUD] Store button listener added successfully");
        }
        else
        {
            Debug.LogError("[GameHUD] Store button is NULL! Cannot add listener!");
        }

        // Subscribe to events
        CurrencyManager.OnPointsChanged += UpdatePointsDisplay;
        UpgradeManager.OnUpgradePurchased += UpdateStatsDisplay;
        GameController.OnRoundStarted += OnRoundChanged;
        GameController.OnRoundEnded += OnRoundChanged;

        // Initial update
        UpdateAllDisplay();

        Debug.Log("[GameHUD] HUD initialized");
    }

    private void OnDestroy()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.OnPointsChanged -= UpdatePointsDisplay;
        if (UpgradeManager.Instance != null)
            UpgradeManager.OnUpgradePurchased -= UpdateStatsDisplay;
        if (GameController.Instance != null)
        {
            GameController.OnRoundStarted -= OnRoundChanged;
            GameController.OnRoundEnded -= OnRoundChanged;
        }
    }

    private void Update()
    {
        // Update HUD continuously for real-time stats
        UpdateRoundDisplay();
        UpdateStatsDisplay("");
    }

    private void UpdateAllDisplay()
    {
        UpdatePointsDisplay(CurrencyManager.Instance.GetCurrentPoints());
        UpdateRoundDisplay();
        UpdateStatsDisplay("");
    }

    private void UpdatePointsDisplay(long newPoints)
    {
        if (pointsText != null)
        {
            pointsText.text = $"Points: {newPoints}";
        }
    }

    private void UpdateRoundDisplay()
    {
        if (roundText != null && GameController.Instance != null)
        {
            roundText.text = $"Round: {GameController.Instance.GetCurrentRound()}";
        }
    }

    private void UpdateStatsDisplay(string upgradeId)
    {
        if (AutoShooter.Instance == null || UpgradeManager.Instance == null)
            return;

        // Damage
        if (damageText != null)
        {
            float damage = AutoShooter.Instance.GetCurrentDamage();
            damageText.text = $"Damage: {damage:F1}";
        }

        // Speed (Cooldown)
        if (speedText != null)
        {
            float cooldown = AutoShooter.Instance.GetCurrentCooldown();
            speedText.text = $"Fire Rate: {cooldown:F2}s";
        }

        // Size
        if (sizeText != null)
        {
            float size = MouseCursor.Instance != null ? MouseCursor.Instance.GetCurrentSize() : 0.5f;
            sizeText.text = $"Cursor Size: {size:F2}";
        }
    }

    private void OnRoundChanged(int round)
    {
        UpdateRoundDisplay();
    }

    private void GoToStore()
    {
        Debug.Log("[GameHUD] Going to store");

        // Save game before leaving
        if (DataManager.Instance != null)
        {
            DataManager.Instance.SaveGame();
        }

        // Load store scene
        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.LoadScene("StoreScene");
        }
    }
}
