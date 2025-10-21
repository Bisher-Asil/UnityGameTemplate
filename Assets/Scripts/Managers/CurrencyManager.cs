using UnityEngine;
using System;

/// <summary>
/// Manages player currency (points) and currency events
/// </summary>
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    private long currentPoints = 0;

    // Events
    public static event Action<long> OnPointsChanged;
    public static event Action<long> OnPointsAdded;
    public static event Action<long> OnPointsSpent;

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

    public long GetCurrentPoints()
    {
        return currentPoints;
    }

    public void AddPoints(long amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("[CurrencyManager] Attempted to add negative points");
            return;
        }

        currentPoints += amount;
        OnPointsChanged?.Invoke(currentPoints);
        OnPointsAdded?.Invoke(amount);

        Debug.Log($"[CurrencyManager] Added {amount} points. Total: {currentPoints}");
    }

    public bool TrySpendPoints(long amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("[CurrencyManager] Attempted to spend negative points");
            return false;
        }

        if (currentPoints < amount)
        {
            Debug.Log($"[CurrencyManager] Insufficient points. Required: {amount}, Available: {currentPoints}");
            return false;
        }

        currentPoints -= amount;
        OnPointsChanged?.Invoke(currentPoints);
        OnPointsSpent?.Invoke(amount);

        Debug.Log($"[CurrencyManager] Spent {amount} points. Remaining: {currentPoints}");
        return true;
    }

    public void SetPoints(long amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("[CurrencyManager] Attempted to set negative points");
            return;
        }

        currentPoints = amount;
        OnPointsChanged?.Invoke(currentPoints);

        Debug.Log($"[CurrencyManager] Points set to {currentPoints}");
    }
}
