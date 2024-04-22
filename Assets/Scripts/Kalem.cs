using UnityEngine;

public class Kalem : MonoBehaviour
{
    

    public Color kalemColor = Color.black; // Kalemle çizilen rengi belirler
    public GameObject tuval; // Çizim alaný GameObject'i

    public GameController gameController;
    private LineRenderer Line; // Þu anda çizilen çizgi

    private Bounds çizimSýnýrlarý; // Çizim alanýnýn sýnýrlarý
    private Vector2 lastPos;

    public ParticleSystem partikül; // Kullanýcý tarafýndan atanacak parçacýk sistemi

    void Start()
    {
        // Çizim alanýnýn sýnýrlarýný al
        if (tuval != null)
        {
            çizimSýnýrlarý = tuval.GetComponent<Renderer>().bounds;
        }
        else
        {
            Debug.LogError("Çizim alaný GameObject'i atanmamýþ!");
        }
    }

    void Update()
    {
        // Kullanýcý sol fare tuþunu basýlý tutarsa veya dokunursa çizim iþlemi yap
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (çizimSýnýrlarý.Contains(mousePos)) // Fare konumu çizim alaný içindeyse
            {
                if (Line == null) // Eðer þu anda bir çizgi yoksa
                {
                    CreateNewLine(mousePos);
                }
                else // Eðer þu anda bir çizgi varsa, çizgiyi güncelle
                {
                    UpdateLine(mousePos);
                }
            }
        }
        else // Kullanýcý fareyi býraktýysa, þu anda çizilen çizgiyi temizle
        {
            Line = null;
            partikül.Stop();
        }
    }

    public void SetDrawColor(Color newColor)
    {
        kalemColor = newColor;
    }

    void CreateNewLine(Vector2 startPos)
    {
        GameObject lineObject = new GameObject("Line");
        Line = lineObject.AddComponent<LineRenderer>();
        Line.positionCount = 2;
        Line.startWidth = gameController.kalýnlýk;
        Line.endWidth = gameController.kalýnlýk;
        Line.material = new Material(Shader.Find("Sprites/Default"));
        Line.startColor = kalemColor;
        Line.endColor = kalemColor;
        Line.SetPosition(0, startPos);
        Line.SetPosition(1, startPos);
        lastPos = startPos;
        int currentLayerOrder = AracSecici.GetCurrentLayerOrder(); // Layer sýrasýný al
        AracSecici.IncreaseLayerOrder(); // Layer sýrasýný artýr
        Line.sortingOrder = currentLayerOrder;

        // Parçacýk Sistemi Oluþturma ve Yönlendirme
        if (partikül != null)
        {
            Vector2 direction = (startPos - lastPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            var shapeModule = partikül.shape;
            shapeModule.rotation = new Vector3(0, angle + 180, 0);

            partikül.transform.position = startPos;
            partikül.Play(); // Parçacýk sistemi baþlat
        }
    }

    void UpdateLine(Vector2 newPos)
    {
        Line.positionCount++;
        Line.SetPosition(Line.positionCount - 1, newPos);

        // Parçacýk sistemi konumunu çizginin son noktasýna ayarla
        if (partikül != null)
        {
            // Parçacýk sistemi rengini kalem rengi olarak ayarla
            var mainModule = partikül.main;
            mainModule.startColor = kalemColor;

            // Parçacýk sistemi boyutunu kalem kalýnlýðýna göre ayarla
            mainModule.startSize = gameController.kalýnlýk * 10;

            partikül.transform.position = newPos;
        }

    }
}
