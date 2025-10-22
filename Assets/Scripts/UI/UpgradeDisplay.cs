using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays a single upgrade in the store UI
/// </summary>
public class UpgradeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeDescriptionText;
    [SerializeField] private TextMeshProUGUI upgradeLevel;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Image lockedOverlay;

    private Upgrade upgrade;

    private void Awake()
    {
        // Auto-find UI elements if not assigned
        if (upgradeNameText == null)
            upgradeNameText = transform.Find("UpgradeNameText")?.GetComponent<TextMeshProUGUI>();
        if (upgradeDescriptionText == null)
            upgradeDescriptionText = transform.Find("UpgradeDescriptionText")?.GetComponent<TextMeshProUGUI>();
        if (upgradeLevel == null)
            upgradeLevel = transform.Find("UpgradeLevel")?.GetComponent<TextMeshProUGUI>();
        if (costText == null)
            costText = transform.Find("CostText")?.GetComponent<TextMeshProUGUI>();
        if (buyButton == null)
            buyButton = transform.Find("BuyButton")?.GetComponent<Button>();
        if (lockedOverlay == null)
        {
            Transform overlayTransform = transform.Find("LockedOverlay");
            if (overlayTransform != null)
                lockedOverlay = overlayTransform.GetComponent<Image>();
        }
    }

    public void Initialize(Upgrade upgrade)
    {
        this.upgrade = upgrade;

        // Configure RectTransform for proper sizing
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(600, 160);
        }

        // Configure text sizes
        if (upgradeNameText != null)
        {
            upgradeNameText.text = upgrade.displayName;
            upgradeNameText.fontSize = 32;
            upgradeNameText.color = Color.white;
        }

        if (upgradeDescriptionText != null)
        {
            upgradeDescriptionText.text = upgrade.description;
            upgradeDescriptionText.fontSize = 18;
            upgradeDescriptionText.color = new Color(0.9f, 0.9f, 0.9f);
        }

        if (upgradeLevel != null)
        {
            upgradeLevel.fontSize = 20;
            upgradeLevel.color = Color.yellow;
        }

        if (costText != null)
        {
            costText.fontSize = 20;
            costText.color = Color.green;
        }

        if (buyButton != null)
            buyButton.onClick.AddListener(OnBuyClicked);

        Refresh();

        Debug.Log($"[UpgradeDisplay] Initialized upgrade display for {upgrade.displayName}");
    }

    public void Refresh()
    {
        if (upgrade == null || UpgradeManager.Instance == null)
            return;

        // Update level
        if (upgradeLevel != null)
            upgradeLevel.text = $"Lv. {upgrade.currentLevel}";

        // Check if dependency is met
        bool canBuy = upgrade.IsDependencyMet(UpgradeManager.Instance.GetAllUpgrades());
        bool canAfford = upgrade.CanAfford(CurrencyManager.Instance.GetCurrentPoints());

        // Update cost
        if (costText != null)
        {
            costText.text = $"Cost: {upgrade.GetCurrentCost()}";
        }

        // Update button state
        if (buyButton != null)
        {
            buyButton.interactable = canBuy && canAfford;
        }

        // Show locked overlay if dependency not met
        if (lockedOverlay != null)
        {
            lockedOverlay.gameObject.SetActive(!canBuy);
        }
    }

private void OnBuyClicked()
    {
        if (UpgradeManager.Instance == null)
            return;

        // Play button click sound
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayButtonClickSound();
        }

        bool success = UpgradeManager.Instance.TryPurchaseUpgrade(upgrade.id);

        if (success)
        {
            Debug.Log($"[UpgradeDisplay] Successfully purchased {upgrade.displayName}");
            
            // Play success sound
            if (GameAudioManager.Instance != null)
            {
                GameAudioManager.Instance.PlayPurchaseSuccessSound();
            }
            
            Refresh();
        }
        else
        {
            Debug.Log($"[UpgradeDisplay] Failed to purchase {upgrade.displayName}");
            
            // Play fail sound
            if (GameAudioManager.Instance != null)
            {
                GameAudioManager.Instance.PlayPurchaseFailSound();
            }
        }
    }
}
