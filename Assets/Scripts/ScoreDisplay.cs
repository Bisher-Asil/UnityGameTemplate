using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text scoreText;
    private int lastScore = -1;
    
void Start()
    {
        scoreText = GetComponent<Text>();
        if (scoreText == null)
        {
            Debug.LogError("ScoreDisplay: Text component not found!");
            return;
        }
        
        // Set RectTransform anchors to center-top
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 1f);  // Center horizontally, top vertically
            rectTransform.anchorMax = new Vector2(0.5f, 1f);  // Center horizontally, top vertically
            rectTransform.offsetMin = new Vector2(-200, -80);  // Left offset, bottom offset
            rectTransform.offsetMax = new Vector2(200, -10);   // Right offset, top offset
            rectTransform.sizeDelta = new Vector2(400, 70);    // Width: 400, Height: 70
        }
        
        // Set initial text
        UpdateScoreDisplay();
    }
    
    void Update()
    {
        // Update score text if score has changed
        if (GameManager.instance != null)
        {
            int currentScore = GameManager.instance.GetScore();
            if (currentScore != lastScore)
            {
                UpdateScoreDisplay();
                lastScore = currentScore;
            }
        }
    }
    
    void UpdateScoreDisplay()
    {
        if (GameManager.instance != null && scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.instance.GetScore();
        }
    }
}
