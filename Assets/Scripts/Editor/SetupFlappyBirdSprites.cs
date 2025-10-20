using UnityEngine;
using UnityEditor;

public class SetupFlappyBirdSprites : MonoBehaviour
{
    [MenuItem("Tools/Setup Flappy Bird Sprites")]
    static void SetupSprites()
    {
        // Get Unity's built-in sprite
        Sprite defaultSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
        Sprite squareSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
        
        // Find and setup Bird
        GameObject bird = GameObject.Find("Bird");
        if (bird != null)
        {
            SpriteRenderer sr = bird.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = defaultSprite;
                sr.color = new Color(1f, 0.9f, 0.2f, 1f); // Yellow
            }
        }
        
        // Find and setup pipes
        GameObject topPipe = GameObject.Find("TopPipe");
        if (topPipe != null)
        {
            SpriteRenderer sr = topPipe.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = squareSprite;
                sr.color = new Color(0.2f, 0.8f, 0.2f, 1f); // Green
            }
        }
        
        GameObject bottomPipe = GameObject.Find("BottomPipe");
        if (bottomPipe != null)
        {
            SpriteRenderer sr = bottomPipe.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = squareSprite;
                sr.color = new Color(0.2f, 0.8f, 0.2f, 1f); // Green
            }
        }
        
        // Setup ground
        GameObject ground1 = GameObject.Find("Ground1");
        if (ground1 != null)
        {
            SpriteRenderer sr = ground1.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = squareSprite;
                sr.color = new Color(0.6f, 0.4f, 0.2f, 1f); // Brown
            }
        }
        
        GameObject ground2 = GameObject.Find("Ground2");
        if (ground2 != null)
        {
            SpriteRenderer sr = ground2.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = squareSprite;
                sr.color = new Color(0.6f, 0.4f, 0.2f, 1f); // Brown
            }
        }
        
        Debug.Log("Flappy Bird sprites setup complete!");
        EditorUtility.SetDirty(bird);
    }
}
