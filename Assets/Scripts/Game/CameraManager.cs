using UnityEngine;

/// <summary>
/// Manages camera settings and follows gameplay
/// </summary>
public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float orthographicSize = 10f;
    [SerializeField] private Color backgroundColor = Color.black;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = GetComponent<Camera>();

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera != null)
        {
            SetupCamera();
        }
        else
        {
            Debug.LogError("[CameraManager] Main camera not found!");
        }
    }

    private void SetupCamera()
    {
        // Configure camera for 2D gameplay
        mainCamera.orthographic = true;
        mainCamera.orthographicSize = orthographicSize;
        mainCamera.backgroundColor = backgroundColor;

        // Position camera at origin looking at z=-1
        transform.position = new Vector3(0, 0, -10);
        transform.rotation = Quaternion.identity;

        Debug.Log($"[CameraManager] Camera configured: Orthographic size {orthographicSize}");
    }

    public void SetOrthographicSize(float size)
    {
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = size;
            orthographicSize = size;
        }
    }

    public float GetOrthographicSize()
    {
        return orthographicSize;
    }
}
