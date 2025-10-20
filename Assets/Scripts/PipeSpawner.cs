using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 3f;  // Time between pipe spawns in seconds
    public float pipeSpeed = 5f;  // Speed pipes move from right to left
    public float minYPosition = -2f;  // Minimum vertical position for pipe gap
    public float maxYPosition = 2f;   // Maximum vertical position for pipe gap
    public float spawnXPosition = 10f;  // X position where pipes spawn (off-screen right)
    
    private float spawnTimer = 0f;
    private bool isSpawning = true;
    
    void Update()
    {
        if (!isSpawning)
            return;
            
        // Increment timer
        spawnTimer += Time.deltaTime;
        
        // Spawn pipe when timer reaches spawnRate
        if (spawnTimer >= spawnRate)
        {
            SpawnPipe();
            spawnTimer = 0f;
        }
    }
    
    void SpawnPipe()
    {
        // Randomize the vertical position of the gap
        float randomYOffset = Random.Range(minYPosition, maxYPosition);
        
        // Instantiate the pipe at the spawn position
        GameObject newPipe = Instantiate(pipePrefab, new Vector3(spawnXPosition, randomYOffset, 0), Quaternion.identity);
        
        // Get the PipeMovement component and set its speed
        PipeMovement pipeMovement = newPipe.GetComponent<PipeMovement>();
        if (pipeMovement != null)
        {
            pipeMovement.speed = pipeSpeed;
        }
    }
    
    public void StopSpawning()
    {
        isSpawning = false;
    }
    
    public void ResumeSpawning()
    {
        isSpawning = true;
    }
}
