using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Ensures an EventSystem exists in the scene for UI interaction
/// Automatically creates one if missing
/// </summary>
public class EnsureEventSystem : MonoBehaviour
{
    private void Awake()
    {
        // Check if EventSystem already exists
        if (EventSystem.current == null)
        {
            Debug.Log("[EnsureEventSystem] No EventSystem found, creating one...");
            
            // Create EventSystem GameObject
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<EventSystem>();
            eventSystemObj.AddComponent<StandaloneInputModule>();
            
            Debug.Log("[EnsureEventSystem] EventSystem created successfully");
        }
        else
        {
            Debug.Log("[EnsureEventSystem] EventSystem already exists");
        }
    }
}
