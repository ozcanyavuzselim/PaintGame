using UnityEngine;

public class Sprey : MonoBehaviour
{
    public GameObject damgaPrefab; // Damga olarak kullan�lacak prefab
    public Color damgaColor = Color.black; // Damga rengi

    public GameController gameController;

    public AudioSource spreyses;
    void Update()
    {
        // Kullan�c� sol fare tu�una bast���nda damgay� kullan�m
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
        // Fare konumunu al ve d�nya koordinatlar�na d�n��t�r�r
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Damga prefab� varsa damgay� olu�turur
        if (damgaPrefab != null)
        {
            spreyses.Play();
            GameObject currentStamp = Instantiate(damgaPrefab, mousePos, Quaternion.identity);
            SpriteRenderer stampRenderer = currentStamp.GetComponent<SpriteRenderer>();
            if (stampRenderer != null)
            {
                int currentLayerOrder = AracSecici.GetCurrentLayerOrder(); // Layer s�ras�n� al�r
                AracSecici.IncreaseLayerOrder(); // Layer s�ras�n� art�r�r
                stampRenderer.sortingOrder = currentLayerOrder; // sortingOrder de�erini g�nceller

                // Damga boyutunu ayarlama
                SetStampSize(currentStamp.transform, gameController.kal�nl�k);

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
