using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Temporary editor script to set up the StoreScene
/// </summary>
public class SetupStoreScene
{
    [MenuItem("Tools/Setup Store Scene")]
    public static void Setup()
    {
        // Open StoreScene
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/StoreScene.unity", OpenSceneMode.Single);
        
        // Create Main Camera if it doesn't exist
        var camera = GameObject.FindObjectOfType<Camera>();
        if (camera == null)
        {
            var cameraGO = new GameObject("Main Camera");
            camera = cameraGO.AddComponent<Camera>();
            cameraGO.AddComponent<AudioListener>();
            
            camera.transform.position = new Vector3(0, 0, -10);
            camera.orthographic = true;
            camera.orthographicSize = 5;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = Color.black;
            
            Debug.Log("Created Main Camera in StoreScene");
        }
        
        // Create StoreSceneSetup if it doesn't exist
        var setup = GameObject.FindObjectOfType<StoreSceneSetup>();
        if (setup == null)
        {
            var setupGO = new GameObject("StoreSceneBootstrap");
            setup = setupGO.AddComponent<StoreSceneSetup>();
            Debug.Log("Created StoreSceneSetup in StoreScene");
        }
        
        // Create managers if they don't exist
        if (GameObject.Find("StoreManagers") == null)
        {
            var managersGO = new GameObject("StoreManagers");
            managersGO.AddComponent<CurrencyManager>();
            managersGO.AddComponent<UpgradeManager>();
            managersGO.AddComponent<DataManager>();
            managersGO.AddComponent<SceneTransitionManager>();
            Debug.Log("Created StoreManagers in StoreScene");
        }
        
        // Create StoreUIManager if it doesn't exist
        var storeUI = GameObject.FindObjectOfType<StoreUIManager>();
        if (storeUI == null)
        {
            var storeUIGO = new GameObject("StoreUIManager");
            storeUI = storeUIGO.AddComponent<StoreUIManager>();
            Debug.Log("Created StoreUIManager in StoreScene");
        }
        
        // Create AudioVisual systems if they don't exist
        if (GameObject.Find("AudioVisualSystems") == null)
        {
            var avGO = new GameObject("AudioVisualSystems");
            avGO.AddComponent<GameAudioManager>();
            Debug.Log("Created AudioVisualSystems in StoreScene");
        }
        
        // Save the scene
        EditorSceneManager.SaveScene(scene);
        Debug.Log("StoreScene setup complete!");
    }
}
