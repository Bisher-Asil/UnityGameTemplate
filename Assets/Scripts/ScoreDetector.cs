using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    private bool hasScored = false;
    
void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bird passed through this trigger
        if (collision.CompareTag("Bird") && !hasScored)
        {
            hasScored = true;
            GameManager.instance.AddScore(1);
            
            // Play score sound
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayScoreSound();
            }
        }
    }
}
