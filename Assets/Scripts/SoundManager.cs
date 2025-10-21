using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    [SerializeField] private AudioClip flapSound;
    [SerializeField] private AudioClip scoreSound;
    
    private AudioSource audioSource;
    
    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    public void PlayFlapSound()
    {
        if (flapSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(flapSound);
        }
    }
    
    public void PlayScoreSound()
    {
        if (scoreSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(scoreSound);
        }
    }
}
