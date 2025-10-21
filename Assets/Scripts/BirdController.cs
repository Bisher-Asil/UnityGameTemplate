using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float flapForce = 6f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
void Update()
    {
        // Input detection for Space key or mouse click
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (!GameManager.instance.IsGameOver())
            {
                Flap();
            }
        }
        
        // Check if bird went out of bounds (top/bottom)
        if (transform.position.y > 10f || transform.position.y < -10f)
        {
            Die();
        }
    }
    
public void Flap()
    {
        // Reset vertical velocity and apply upward impulse
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, flapForce);
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
