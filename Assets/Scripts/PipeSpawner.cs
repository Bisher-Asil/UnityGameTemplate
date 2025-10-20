using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnXPosition = 10f;
    
    [Header("Pipe Settings")]
    [SerializeField] private float minHeight = -2f;
    [SerializeField] private float maxHeight = 2f;
    
    private float timer = 0f;
    private bool isSpawning = true;
    
    void Update()
    {
        if (!isSpawning) return;
        
        timer += Time.deltaTime;
        
        if (timer >= spawnRate)
        {
            SpawnPipe();
            timer = 0f;
        }
    }
    
    void SpawnPipe()
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnXPosition, randomHeight, 0f);
        
        Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
    }
    
    public void StopSpawning()
    {
        isSpawning = false;
    }
    
    public void StartSpawning()
    {
        isSpawning = true;
        timer = 0f;
    }
}
