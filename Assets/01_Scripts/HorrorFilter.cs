using UnityEngine;

public class HorrorFilter : MonoBehaviour
{
    
    public Color filterColor = new Color(0.1f, 0.2f, 0.3f, 0.3f);

    private Texture2D texture;

    void Start()
    {
        
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
    }

    void OnGUI()
    {
        
        GUI.color = filterColor;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
    }
}
