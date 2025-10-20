using UnityEngine;
using UnityEditor;

public class AssignPipeSprites : MonoBehaviour
{
    [MenuItem("Tools/Assign Pipe Sprites")]
    static void AssignSprites()
    {
        // Load the prefab
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PipePrefab.prefab");
        
        if (prefab == null)
        {
            Debug.LogError("PipePrefab not found!");
            return;
        }
        
        // Get built-in sprite
        Sprite sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
        
        // Get the prefab's children
        Transform[] children = prefab.GetComponentsInChildren<Transform>(true);
        
        foreach (Transform child in children)
        {
            if (child.name == "TopPipe" || child.name == "BottomPipe")
            {
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = sprite;
                    sr.color = new Color(0.2f, 0.8f, 0.2f, 1f);
                    EditorUtility.SetDirty(child.gameObject);
                }
            }
        }
        
        EditorUtility.SetDirty(prefab);
        AssetDatabase.SaveAssets();
        
        Debug.Log("Pipe sprites assigned!");
    }
}
