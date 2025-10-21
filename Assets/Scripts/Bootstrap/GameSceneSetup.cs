using UnityEngine;

/// <summary>
/// Sets up the GameScene with all necessary components including camera
/// </summary>
public class GameSceneSetup : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("[GameSceneSetup] Setting up GameScene");

        // Ensure camera exists
        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogWarning("[GameSceneSetup] Main camera not found, creating one");
            GameObject cameraObj = new GameObject("Main Camera");
            camera = cameraObj.AddComponent<Camera>();
            cameraObj.AddComponent<AudioListener>();
        }

        // Configure camera
        camera.orthographic = true;
        camera.orthographicSize = 10f;
        camera.backgroundColor = Color.black;
        camera.transform.position = new Vector3(0, 0, -10);

        Debug.Log("[GameSceneSetup] GameScene setup complete");
    }
}
