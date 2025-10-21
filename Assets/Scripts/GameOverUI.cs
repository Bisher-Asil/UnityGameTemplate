using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private GameObject gameOverPanel;
    private Text finalScoreText;
    private Button restartButton;
    private bool hasShown = false;
    
void Start()
    {
        // Auto-find the GameOverPanel, FinalScoreText, and RestartButton
        Transform canvasTransform = GetComponent<Canvas>().transform;
        
        gameOverPanel = canvasTransform.Find("GameOverPanel").gameObject;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            finalScoreText = gameOverPanel.transform.Find("FinalScoreText").GetComponent<Text>();
            restartButton = gameOverPanel.transform.Find("RestartButton").GetComponent<Button>();
        }
        
        // Set up restart button listener
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
        
        Debug.Log("GameOverUI initialized. Panel: " + (gameOverPanel != null) + ", Score: " + (finalScoreText != null) + ", Button: " + (restartButton != null));
    }
    
void Update()
    {
        // Check if game is over and show panel
        if (GameManager.instance != null && GameManager.instance.IsGameOver())
        {
            if (gameOverPanel != null && !gameOverPanel.activeInHierarchy)
            {
                ShowGameOverUI();
            }
        }
    }
    
void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);
        
        // Update final score
        if (finalScoreText != null && GameManager.instance != null)
        {
            finalScoreText.text = "Final Score: " + GameManager.instance.GetScore();
        }
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Resume time if it was paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
