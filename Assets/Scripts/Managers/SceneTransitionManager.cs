using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Handles scene loading and transitions
/// </summary>
public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Load a scene with fade transition
    /// </summary>
    public void LoadScene(string sceneName)
    {
        Debug.Log($"[SceneTransitionManager] Loading scene: {sceneName}");
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Fade to black
        yield return Fade(0, 1, fadeDuration);

        // Load scene
        Debug.Log($"[SceneTransitionManager] Actually loading scene now: {sceneName}");
        
        // Check if scene exists in build settings
        bool sceneExists = false;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (name == sceneName)
            {
                sceneExists = true;
                break;
            }
        }

        if (!sceneExists)
        {
            Debug.LogError($"[SceneTransitionManager] Scene '{sceneName}' not found in build settings! Add it to File > Build Settings.");
            yield break;
        }

        SceneManager.LoadScene(sceneName);

        // Fade from black
        yield return Fade(1, 0, fadeDuration);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        if (fadeCanvasGroup == null)
            yield break;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
