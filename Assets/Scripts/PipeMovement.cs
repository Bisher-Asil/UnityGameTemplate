using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 5f;  // Speed to move left
    public float destroyPositionX = -12f;  // X position where pipe is destroyed
    
    void Update()
    {
        // Move pipe to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
        // Destroy pipe if it goes off-screen (left side)
        if (transform.position.x < destroyPositionX)
        {
            Destroy(gameObject);
        }
    }
}
