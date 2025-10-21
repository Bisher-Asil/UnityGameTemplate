using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Spawns enemy squares in the game world
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-8, -4);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(8, 4);
    [SerializeField] private float spawnDelay = 0.2f;

    private List<EnemySquare> activeSquares = new List<EnemySquare>();
    private float spawnTimer = 0f;
    private int squaresToSpawn = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        // Update active squares list
        activeSquares.RemoveAll(square => square == null);

        // Check if round should end (all squares destroyed)
        if (squaresToSpawn == 0 && activeSquares.Count == 0 && GameController.Instance.IsRoundActive())
        {
            GameController.Instance.EndRound();
        }

        // Spawn enemies with delay
        if (squaresToSpawn > 0)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnSquare();
                squaresToSpawn--;
                spawnTimer = spawnDelay;
            }
        }
    }

    public void SpawnRound(int squareCount)
    {
        squaresToSpawn = squareCount;
        spawnTimer = spawnDelay;
        Debug.Log($"[EnemySpawner] Starting to spawn {squareCount} squares");
    }

    private void SpawnSquare()
    {
        if (squarePrefab == null)
        {
            Debug.LogError("[EnemySpawner] Square prefab is not assigned!");
            return;
        }

        Vector2 randomPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject squareObj = Instantiate(squarePrefab, randomPos, Quaternion.identity);
        EnemySquare square = squareObj.GetComponent<EnemySquare>();

        if (square != null)
        {
            activeSquares.Add(square);
            square.OnDestroyed += () => activeSquares.Remove(square);
        }

        Debug.Log($"[EnemySpawner] Spawned square at {randomPos}");
    }

    public int GetActiveSquareCount()
    {
        return activeSquares.Count;
    }

    public List<EnemySquare> GetActiveSquares()
    {
        return new List<EnemySquare>(activeSquares);
    }
}
