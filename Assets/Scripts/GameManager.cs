using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private int score = 0;
    private bool isGameOver = false;
    
    void Awake()
    {
        // Singleton pattern - ensure only one GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public int GetScore()
    {
        return score;
    }
    
    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            Debug.Log("Score: " + score);
        }
    }
    
    public bool IsGameOver()
    {
        return isGameOver;
    }
    
    public void SetGameOver(bool gameOver)
    {
        isGameOver = gameOver;
    }
    
    public void ResetScore()
    {
        score = 0;
        isGameOver = false;
    }
}
