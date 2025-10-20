using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float destroyXPosition = -12f;
    
    void Update()
    {
        // Move pipe to the left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        
        // Destroy pipe when off screen
        if (transform.position.x < destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}
