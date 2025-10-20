using UnityEngine;

[ExecuteInEditMode]
public class SpriteSetup : MonoBehaviour
{
    private static Sprite squareSprite;
    private static Sprite circleSprite;
    
    void Start()
    {
        SetupAllSprites();
        // Listen for new objects being instantiated
        StartCoroutine(CheckForNewPipes());
    }
    
    void OnEnable()
    {
        SetupAllSprites();
    }
    
    public void SetupAllSprites()
    {
        // Create reusable sprites if they don't exist
        if (squareSprite == null)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();
            squareSprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
        }
        
        if (circleSprite == null)
        {
            Texture2D texture = new Texture2D(64, 64);
            Color[] pixels = new Color[64 * 64];
            
            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    float dx = (x - 32) / 32f;
                    float dy = (y - 32) / 32f;
                    float dist = Mathf.Sqrt(dx * dx + dy * dy);
                    pixels[y * 64 + x] = dist < 1f ? Color.white : Color.clear;
                }
            }
            
            texture.SetPixels(pixels);
            texture.Apply();
            circleSprite = Sprite.Create(texture, new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f), 64);
        }
        
        // Setup Bird
        GameObject bird = GameObject.Find("Bird");
        if (bird != null)
        {
            SpriteRenderer sr = bird.GetComponent<SpriteRenderer>();
            if (sr != null && sr.sprite == null)
            {
                sr.sprite = circleSprite;
                sr.color = new Color(1f, 0.9f, 0.2f, 1f); // Yellow
            }
        }
        
        // Setup ground
        SetupSquareSprite("Ground1", new Color(0.6f, 0.4f, 0.2f, 1f));
        SetupSquareSprite("Ground2", new Color(0.6f, 0.4f, 0.2f, 1f));
        
        // Setup any existing pipes
        SetupAllPipes();
        
        Debug.Log("Sprites setup complete!");
    }
    
    System.Collections.IEnumerator CheckForNewPipes()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            SetupAllPipes();
        }
    }
    
    void SetupAllPipes()
    {
        // Find all pipe objects
        GameObject[] topPipes = GameObject.FindGameObjectsWithTag("Untagged");
        foreach (GameObject obj in topPipes)
        {
            if (obj.name.Contains("TopPipe"))
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null && sr.sprite == null)
                {
                    sr.sprite = squareSprite;
                    sr.color = new Color(0.2f, 0.8f, 0.2f, 1f);
                }
            }
            else if (obj.name.Contains("BottomPipe"))
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null && sr.sprite == null)
                {
                    sr.sprite = squareSprite;
                    sr.color = new Color(0.2f, 0.8f, 0.2f, 1f);
                }
            }
        }
    }
    
    void SetupSquareSprite(string objectName, Color color)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null && sr.sprite == null)
            {
                sr.sprite = squareSprite;
                sr.color = color;
            }
        }
    }
}
