using UnityEngine;

/// <summary>
/// Sets up the StoreScene with all necessary components including camera
/// </summary>
public class StoreSceneSetup : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("[StoreSceneSetup] Setting up StoreScene");

        // Ensure camera exists
        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogWarning("[StoreSceneSetup] Main camera not found, creating one");
            GameObject cameraObj = new GameObject("Main Camera");
            camera = cameraObj.AddComponent<Camera>();
            cameraObj.AddComponent<AudioListener>();
        }

        // Configure camera for store UI
        camera.orthographic = true;
        camera.orthographicSize = 10f;
        camera.backgroundColor = Color.black;
        camera.transform.position = new Vector3(0, 0, -10);

        Debug.Log("[StoreSceneSetup] StoreScene setup complete");
    }
}
