using UnityEngine;

public class Kova : MonoBehaviour
{
    public Color paintrenk = Color.black; // Kovayla boyanacak renk

    public SpriteRenderer canvasRenderer; // Tuvalin SpriteRenderer bileþeni

    void Start()
    {
        
    }

    void Update()
    {
        // Fare sol týklandýðýnda kova aracýný kullan
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
        // Fare konumunu al ve dünya koordinatlarýna dönüþtür
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Fare konumu tuvalin içinde mi kontrol et
        if (canvasRenderer.bounds.Contains(mousePos))
        {
            // Tuvalin rengini deðiþtir
            ChangeCanvasColor();
        }
    }

    void ChangeCanvasColor()
    {
        // Tuvalin rengini deðiþtir
        canvasRenderer.color = paintrenk;
    }
}
