using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Manages the store UI and displays upgrade information
/// </summary>
public class StoreUIManager : MonoBehaviour
{
    public static StoreUIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI playerPointsText;
    [SerializeField] private Button returnToGameButton;
    [SerializeField] private Transform upgradeContainer;
    [SerializeField] private GameObject upgradeDisplayPrefab;

    private Dictionary<string, UpgradeDisplay> upgradeDisplays = new Dictionary<string, UpgradeDisplay>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Auto-find UI elements if not assigned
        if (playerPointsText == null)
            playerPointsText = transform.Find("PlayerPointsText")?.GetComponent<TextMeshProUGUI>();
        if (returnToGameButton == null)
            returnToGameButton = transform.Find("ReturnToGameButton")?.GetComponent<Button>();
        if (upgradeContainer == null)
            upgradeContainer = transform.Find("UpgradeContainer");

        // Log warnings for missing references
        if (playerPointsText == null) Debug.LogWarning("[StoreUIManager] PlayerPointsText not found!");
        if (returnToGameButton == null) Debug.LogWarning("[StoreUIManager] ReturnToGameButton not found!");
        if (upgradeContainer == null) Debug.LogWarning("[StoreUIManager] UpgradeContainer not found!");
        if (upgradeDisplayPrefab == null) Debug.LogWarning("[StoreUIManager] UpgradeDisplayPrefab not assigned!");
    }

    private void Start()
    {
        // Setup button listeners
        if (returnToGameButton != null)
        {
            returnToGameButton.onClick.AddListener(ReturnToGame);
        }

        // Subscribe to events
        CurrencyManager.OnPointsChanged += UpdatePointsDisplay;
        UpgradeManager.OnUpgradesChanged += RefreshUpgradeDisplays;

        // Initialize upgrade displays
        InitializeUpgradeDisplays();
        UpdatePointsDisplay(CurrencyManager.Instance.GetCurrentPoints());

        Debug.Log("[StoreUIManager] Store UI initialized");
    }

    private void OnDestroy()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.OnPointsChanged -= UpdatePointsDisplay;
        if (UpgradeManager.Instance != null)
            UpgradeManager.OnUpgradesChanged -= RefreshUpgradeDisplays;
    }

    private void InitializeUpgradeDisplays()
    {
        if (UpgradeManager.Instance == null)
            return;

        var allUpgrades = UpgradeManager.Instance.GetAllUpgrades();

        foreach (var upgrade in allUpgrades.Values)
        {
            if (upgradeDisplayPrefab != null && upgradeContainer != null)
            {
                GameObject displayObj = Instantiate(upgradeDisplayPrefab, upgradeContainer);
                displayObj.SetActive(true); // Activate the instantiated display
                UpgradeDisplay display = displayObj.GetComponent<UpgradeDisplay>();

                if (display != null)
                {
                    display.Initialize(upgrade);
                    upgradeDisplays[upgrade.id] = display;
                }
            }
            else if (upgradeContainer != null)
            {
                // Create a simple display without prefab
                Debug.LogWarning("[StoreUIManager] Upgrade display prefab not assigned, creating programmatically");
                GameObject displayObj = CreateUpgradeDisplayFromCode(upgrade);
                if (displayObj != null)
                {
                    UpgradeDisplay display = displayObj.GetComponent<UpgradeDisplay>();
                    if (display != null)
                    {
                        display.Initialize(upgrade);
                        upgradeDisplays[upgrade.id] = display;
                    }
                }
            }
        }

        RefreshUpgradeDisplays();
    }

    private GameObject CreateUpgradeDisplayFromCode(Upgrade upgrade)
    {
        // Create main container
        GameObject displayObj = new GameObject($"UpgradeDisplay_{upgrade.id}");
        displayObj.transform.SetParent(upgradeContainer, false);

        RectTransform rectTransform = displayObj.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400, 150);

        Image bg = displayObj.AddComponent<Image>();
        bg.color = new Color(0.2f, 0.2f, 0.2f, 0.9f);

        VerticalLayoutGroup layout = displayObj.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(10, 10, 10, 10);
        layout.spacing = 5;
        layout.childControlHeight = false;
        layout.childControlWidth = true;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = true;

        UpgradeDisplay display = displayObj.AddComponent<UpgradeDisplay>();

        // Create child UI elements
        CreateTextElement(displayObj.transform, "UpgradeNameText", 20, Color.white);
        CreateTextElement(displayObj.transform, "UpgradeDescriptionText", 14, Color.gray);
        CreateTextElement(displayObj.transform, "UpgradeLevel", 16, Color.yellow);
        CreateTextElement(displayObj.transform, "CostText", 16, Color.green);

        // Create buy button
        GameObject buttonObj = new GameObject("BuyButton");
        buttonObj.transform.SetParent(displayObj.transform, false);
        RectTransform buttonRect = buttonObj.AddComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(0, 40);

        Image buttonImg = buttonObj.AddComponent<Image>();
        buttonImg.color = new Color(0.3f, 0.6f, 0.3f, 1f);

        Button button = buttonObj.AddComponent<Button>();

        // Add button text
        GameObject buttonTextObj = new GameObject("Text");
        buttonTextObj.transform.SetParent(buttonObj.transform, false);
        RectTransform buttonTextRect = buttonTextObj.AddComponent<RectTransform>();
        buttonTextRect.anchorMin = Vector2.zero;
        buttonTextRect.anchorMax = Vector2.one;
        buttonTextRect.sizeDelta = Vector2.zero;

        TextMeshProUGUI buttonText = buttonTextObj.AddComponent<TextMeshProUGUI>();
        buttonText.text = "BUY";
        buttonText.fontSize = 18;
        buttonText.alignment = TextAlignmentOptions.Center;
        buttonText.color = Color.white;

        // Create locked overlay (initially hidden)
        GameObject overlayObj = new GameObject("LockedOverlay");
        overlayObj.transform.SetParent(displayObj.transform, false);
        RectTransform overlayRect = overlayObj.AddComponent<RectTransform>();
        overlayRect.anchorMin = Vector2.zero;
        overlayRect.anchorMax = Vector2.one;
        overlayRect.sizeDelta = Vector2.zero;

        Image overlayImg = overlayObj.AddComponent<Image>();
        overlayImg.color = new Color(0, 0, 0, 0.7f);
        overlayObj.SetActive(false);

        Debug.Log($"[StoreUIManager] Created programmatic display for {upgrade.displayName}");
        return displayObj;
    }

    private GameObject CreateTextElement(Transform parent, string name, int fontSize, Color color)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent, false);

        RectTransform rectTransform = textObj.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(0, fontSize + 10);

        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.fontSize = fontSize;
        text.color = color;
        text.alignment = TextAlignmentOptions.Left;
        text.text = name;

        return textObj;
    }

    private void UpdatePointsDisplay(long newPoints)
    {
        if (playerPointsText != null)
        {
            playerPointsText.text = $"Points: {newPoints}";
        }
    }

    private void RefreshUpgradeDisplays()
    {
        foreach (var display in upgradeDisplays.Values)
        {
            if (display != null)
                display.Refresh();
        }
    }

    private void ReturnToGame()
    {
        Debug.Log("[StoreUIManager] Returning to game");

        // Save game before leaving
        if (DataManager.Instance != null)
        {
            DataManager.Instance.SaveGame();
        }

        // Load game scene
        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.LoadScene("GameScene");
        }
    }
}
