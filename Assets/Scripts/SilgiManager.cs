using UnityEngine;

public class SilgiManager : MonoBehaviour
{
    public GameObject tuval; // Çizim alaný GameObject'i
    public GameController gameController;
    public ParticleSystem partikul;

    private LineRenderer silgiLine; // Þu anda silinen çizgi

    private Vector2 lastErasedPos; // Son silinmiþ konum

    private Bounds çizimSýnýrlarý; // Çizim alanýnýn sýnýrlarý

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
        // Kullanýcý sol fare tuþunu basýlý tutarsa veya dokunursa silme iþlemi yap
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (çizimSýnýrlarý.Contains(mousePos)) // Fare konumu çizim alaný içindeyse
            {
                if (silgiLine == null) // Eðer þu anda bir çizgi silinmiyorsa
                {
                    StartErasing(mousePos);
                }
                else // Eðer þu anda bir çizgi siliniyorsa, çizgiyi güncelle
                {
                    UpdateErasedLine(mousePos);

                }
            }
        }
        else // Kullanýcý fareyi býraktýysa, þu anda silinen çizgiyi temizle
        {
            silgiLine = null;
            partikul.Stop();
        }
    }

    void StartErasing(Vector2 startPos)
    {
        GameObject lineObject = new GameObject("ErasedLine");
        silgiLine = lineObject.AddComponent<LineRenderer>();
        silgiLine.positionCount = 2;
        silgiLine.startWidth = gameController.kalýnlýk;
        silgiLine.endWidth = gameController.kalýnlýk;
        silgiLine.material = new Material(Shader.Find("Sprites/Default"));
        silgiLine.startColor = Color.white; // Silgiyi beyaza ayarla
        silgiLine.endColor = Color.white;
        silgiLine.SetPosition(0, startPos);
        silgiLine.SetPosition(1, startPos);
        lastErasedPos = startPos;
        int currentLayerOrder = AracSecici.GetCurrentLayerOrder(); // Layer sýrasýný al
        AracSecici.IncreaseLayerOrder(); // Layer sýrasýný artýr
        silgiLine.sortingOrder = currentLayerOrder;

        // Parçacýk Sistemi Oluþturma ve Yönlendirme
        if (partikul != null)
        {
            Vector2 direction = (startPos - lastErasedPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            var shapeModule = partikul.shape;
            shapeModule.rotation = new Vector3(0, angle + 180, 0);

            partikul.transform.position = startPos;
            partikul.Play(); // Parçacýk sistemi baþlat
        }

    }

    void UpdateErasedLine(Vector2 newPos)
    {
        silgiLine.positionCount++;
        silgiLine.SetPosition(silgiLine.positionCount - 1, newPos);
        lastErasedPos = newPos;

        // Parçacýk sistemi konumunu çizginin son noktasýna ayarla
        if (partikul != null)
        {
            // Parçacýk sistemi rengini kalem rengi olarak ayarla
            var mainModule = partikul.main;
            mainModule.startColor = Color.white; 

            // Parçacýk sistemi boyutunu kalem kalýnlýðýna göre ayarla
            mainModule.startSize = gameController.kalýnlýk * 10;

            partikul.transform.position = newPos;
        }
    }
}
