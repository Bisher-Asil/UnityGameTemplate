using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float flapForce = 5f;
    [SerializeField] private float downwardForce = 2f;
    [SerializeField] private float maxFallSpeed = 10f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // Input detection
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }
        
        // Apply downward force for smoother falling
        if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity += Vector2.down * downwardForce * Time.deltaTime;
        }
        
        // Clamp fall speed
        if (rb.linearVelocity.y < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
        }
        
        // Death by going off-screen (too high or too low)
        if (transform.position.y > 10 || transform.position.y < -10)
        {
            Die();
        }
    }
    
public void Flap()
    {
        // Play flap sound
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayFlapSound();
        }
        
        rb.linearVelocity = new Vector2(0, flapForce);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Detect collision with pipes or ground
        bool isPipe = collision.gameObject.CompareTag("Pipe");
        bool isGround = collision.gameObject.CompareTag("Ground");
        
        // Also check parent if child doesn't have tag
        if (!isPipe && collision.gameObject.transform.parent != null)
        {
            isPipe = collision.gameObject.transform.parent.CompareTag("Pipe");
        }
        
        if (isPipe || isGround)
        {
            Die();
        }
    }
    
    void Die()
    {
        GameManager.instance.SetGameOver(true);
        PipeSpawner pipeSpawner = FindObjectOfType<PipeSpawner>();
        if (pipeSpawner != null)
        {
            pipeSpawner.StopSpawning();
        }
        Debug.Log("Game Over! Final Score: " + GameManager.instance.GetScore());
    }
}
