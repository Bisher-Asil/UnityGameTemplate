using UnityEngine;

/// <summary>
/// Custom mouse cursor system that tracks player input and supports upgrading size
/// </summary>
public class MouseCursor : MonoBehaviour
{
    public static MouseCursor Instance { get; private set; }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private new CircleCollider2D collider;  // 'new' keyword to hide inherited member
    [SerializeField] private float baseSize = 0.5f;
    [SerializeField] private float sizeUpgradeMultiplier = 0.2f;

    private float currentSize;
    private Camera mainCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (collider == null)
            collider = GetComponent<CircleCollider2D>();

        mainCamera = Camera.main;

        // Hide default cursor
        Cursor.visible = false;

        // Ensure sprite renderer is visible
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
            // Set a visible color if no sprite is assigned
            if (spriteRenderer.sprite == null)
            {
                Debug.LogWarning("[MouseCursor] No sprite assigned! Cursor will be invisible until sprite is set.");
                // Set cyan color to make it visible even without sprite (for debugging)
                spriteRenderer.color = Color.cyan;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }

        // Initialize size
        currentSize = baseSize;
        UpdateCursorSize();

        // Subscribe to upgrade events
        UpgradeManager.OnUpgradePurchased += OnUpgradePurchased;
    }

    private void OnDestroy()
    {
        UpgradeManager.OnUpgradePurchased -= OnUpgradePurchased;
    }

    private void Update()
    {
        // Update cursor position to follow mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
    }

    private void OnUpgradePurchased(string upgradeId)
    {
        if (upgradeId == "size")
        {
            UpdateCursorSize();
        }
    }

    public void UpdateCursorSize()
    {
        int sizeLevel = UpgradeManager.Instance.GetUpgradeLevel("size");
        currentSize = baseSize + (sizeLevel * sizeUpgradeMultiplier);

        // Update visual size
        transform.localScale = Vector3.one * currentSize;

        // Update collider size
        if (collider != null)
        {
            collider.radius = currentSize / 2f;
        }

        Debug.Log($"[MouseCursor] Size updated to {currentSize} (level {sizeLevel})");
    }

    public float GetCurrentSize()
    {
        return currentSize;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        gameObject.SetActive(true);
    }
}
