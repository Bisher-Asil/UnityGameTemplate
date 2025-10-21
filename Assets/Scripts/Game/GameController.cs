using UnityEngine;
using System;

/// <summary>
/// Controls the overall game flow and round progression
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private int currentRound = 1;
    [SerializeField] private int squaresToSpawnPerRound = 5;
    [SerializeField] private float timeBetweenRounds = 2f;

    private bool isRoundActive = false;
    private float roundTimer = 0f;

    // Events
    public static event Action<int> OnRoundStarted;
    public static event Action<int> OnRoundEnded;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartRound();
    }

    private void Update()
    {
        if (!isRoundActive)
        {
            roundTimer += Time.deltaTime;
            if (roundTimer >= timeBetweenRounds)
            {
                StartRound();
            }
        }
    }

    public void StartRound()
    {
        isRoundActive = true;
        roundTimer = 0f;
        OnRoundStarted?.Invoke(currentRound);

        if (EnemySpawner.Instance != null)
        {
            EnemySpawner.Instance.SpawnRound(squaresToSpawnPerRound);
        }

        Debug.Log($"[GameController] Round {currentRound} started");
    }

    public void EndRound()
    {
        isRoundActive = false;
        roundTimer = 0f;
        OnRoundEnded?.Invoke(currentRound);

        currentRound++;
        squaresToSpawnPerRound = Mathf.CeilToInt(squaresToSpawnPerRound * 1.1f); // 10% difficulty increase

        Debug.Log($"[GameController] Round {currentRound - 1} ended. Next: {squaresToSpawnPerRound} squares");
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    public bool IsRoundActive()
    {
        return isRoundActive;
    }

    public int GetSquaresToSpawn()
    {
        return squaresToSpawnPerRound;
    }
}
