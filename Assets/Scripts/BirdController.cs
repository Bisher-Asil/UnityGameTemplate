using UnityEngine;

public class BirdController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float flapForce = 5f;
    [SerializeField] private float rotationSpeed = 3f;
    
    [Header("Rotation")]
    [SerializeField] private float maxRotation = 30f;
    [SerializeField] private float minRotation = -90f;
    
    private Rigidbody2D rb;
    private bool isDead = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (isDead) return;
        
        // Flap on spacebar or mouse click
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }
        
        // Rotate bird based on velocity
        RotateBird();
    }
    
    void Flap()
    {
        rb.linearVelocity = Vector2.up * flapForce;
    }
    
    void RotateBird()
    {
        float rotation = Mathf.Lerp(maxRotation, minRotation, -rb.linearVelocity.y / 10f);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreZone"))
        {
            FlappyBirdGameManager.Instance?.AddScore();
        }
    }
    
    void Die()
    {
        if (isDead) return;
        
        isDead = true;
        FlappyBirdGameManager.Instance?.GameOver();
    }
}
