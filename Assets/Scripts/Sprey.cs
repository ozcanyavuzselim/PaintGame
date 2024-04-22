using UnityEngine;

public class Sprey : MonoBehaviour
{
    public GameObject damgaPrefab; // Damga olarak kullanýlacak prefab
    public Color damgaColor = Color.black; // Damga rengi

    public GameController gameController;

    public AudioSource spreyses;
    void Update()
    {
        // Kullanýcý sol fare tuþuna bastýðýnda damgayý kullaným
        if (Input.GetMouseButtonDown(0))
        {
            UseStamp();
        }
    }
    public void SetDrawColor(Color newColor)
    {
        damgaColor = newColor;
    }
    void UseStamp()
    {
        // Fare konumunu al ve dünya koordinatlarýna dönüþtürür
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Damga prefabý varsa damgayý oluþturur
        if (damgaPrefab != null)
        {
            spreyses.Play();
            GameObject currentStamp = Instantiate(damgaPrefab, mousePos, Quaternion.identity);
            SpriteRenderer stampRenderer = currentStamp.GetComponent<SpriteRenderer>();
            if (stampRenderer != null)
            {
                int currentLayerOrder = AracSecici.GetCurrentLayerOrder(); // Layer sýrasýný alýr
                AracSecici.IncreaseLayerOrder(); // Layer sýrasýný artýrýr
                stampRenderer.sortingOrder = currentLayerOrder; // sortingOrder deðerini günceller

                // Damga boyutunu ayarlama
                SetStampSize(currentStamp.transform, gameController.kalýnlýk);

                // Damga rengini ayarlama
                SetStampColor(stampRenderer, damgaColor);
            }
        }
    }

    void SetStampSize(Transform stampTransform, float size)
    {
        // Damga boyutunu ayarlama
        if (stampTransform != null)
        {
            stampTransform.localScale = new Vector3(size, size, 1.0f);
        }
    }

    void SetStampColor(SpriteRenderer stampRenderer, Color color)
    {
        // Damga rengini ayarlama
        if (stampRenderer != null)
        {
            stampRenderer.color = color;
        }
    }
}
