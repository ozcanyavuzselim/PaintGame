using UnityEngine;

public class Kova : MonoBehaviour
{
    public Color paintrenk = Color.black; // Kovayla boyanacak renk

    public SpriteRenderer canvasRenderer; // Tuvalin SpriteRenderer bile�eni

    void Start()
    {
        
    }

    void Update()
    {
        // Fare sol t�kland���nda kova arac�n� kullan
        if (Input.GetMouseButtonDown(0))
        {
            UsePaintBucket();
        }
    }
    public void SetDrawColor(Color newColor)
    {
        paintrenk = newColor;
    }
    void UsePaintBucket()
    {
        // Fare konumunu al ve d�nya koordinatlar�na d�n��t�r
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Fare konumu tuvalin i�inde mi kontrol et
        if (canvasRenderer.bounds.Contains(mousePos))
        {
            // Tuvalin rengini de�i�tir
            ChangeCanvasColor();
        }
    }

    void ChangeCanvasColor()
    {
        // Tuvalin rengini de�i�tir
        canvasRenderer.color = paintrenk;
    }
}
