using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float resetPositionX = -12f;
    [SerializeField] private float startPositionX = 15f;
    
    void Update()
    {
        // Move background to the left
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        
        // Reset position when off screen
        if (transform.position.x < resetPositionX)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = startPositionX;
            transform.position = newPosition;
        }
    }
}
