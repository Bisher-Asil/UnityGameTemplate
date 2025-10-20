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
            Flap();
        }
    }
    
public void Flap()
    {
        // Reset vertical velocity and apply upward impulse
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, flapForce);
    }
}
