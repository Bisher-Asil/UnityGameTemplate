using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyBirdGameManager : MonoBehaviour
{
    public static FlappyBirdGameManager Instance { get; private set; }
    
    [Header("UI References")]
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMPro.TextMeshProUGUI finalScoreText;
    
    [Header("Game Objects")]
    [SerializeField] private PipeSpawner pipeSpawner;
    
    private int score = 0;
    private bool isGameOver = false;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        score = 0;
        isGameOver = false;
        UpdateScoreUI();
        
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
    
    public void AddScore()
    {
        if (isGameOver) return;
        
        score++;
        UpdateScoreUI();
    }
    
    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }
    
    public void GameOver()
    {
        if (isGameOver) return;
        
        isGameOver = true;
        
        if (pipeSpawner != null)
            pipeSpawner.StopSpawning();
        
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        
        if (finalScoreText != null)
            finalScoreText.text = "Score: " + score;
        
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
