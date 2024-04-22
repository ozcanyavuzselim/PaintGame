using UnityEngine;

public class Kalem : MonoBehaviour
{
    

    public Color kalemColor = Color.black; // Kalemle �izilen rengi belirler
    public GameObject tuval; // �izim alan� GameObject'i

    public GameController gameController;
    private LineRenderer Line; // �u anda �izilen �izgi

    private Bounds �izimS�n�rlar�; // �izim alan�n�n s�n�rlar�
    private Vector2 lastPos;

    public ParticleSystem partik�l; // Kullan�c� taraf�ndan atanacak par�ac�k sistemi

    void Start()
    {
        // �izim alan�n�n s�n�rlar�n� al
        if (tuval != null)
        {
            �izimS�n�rlar� = tuval.GetComponent<Renderer>().bounds;
        }
        else
        {
            Debug.LogError("�izim alan� GameObject'i atanmam��!");
        }
    }

    void Update()
    {
        // Kullan�c� sol fare tu�unu bas�l� tutarsa veya dokunursa �izim i�lemi yap
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (�izimS�n�rlar�.Contains(mousePos)) // Fare konumu �izim alan� i�indeyse
            {
                if (Line == null) // E�er �u anda bir �izgi yoksa
                {
                    CreateNewLine(mousePos);
                }
                else // E�er �u anda bir �izgi varsa, �izgiyi g�ncelle
                {
                    UpdateLine(mousePos);
                }
            }
        }
        else // Kullan�c� fareyi b�rakt�ysa, �u anda �izilen �izgiyi temizle
        {
            Line = null;
            partik�l.Stop();
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
        Line.startWidth = gameController.kal�nl�k;
        Line.endWidth = gameController.kal�nl�k;
        Line.material = new Material(Shader.Find("Sprites/Default"));
        Line.startColor = kalemColor;
        Line.endColor = kalemColor;
        Line.SetPosition(0, startPos);
        Line.SetPosition(1, startPos);
        lastPos = startPos;
        int currentLayerOrder = AracSecici.GetCurrentLayerOrder(); // Layer s�ras�n� al
        AracSecici.IncreaseLayerOrder(); // Layer s�ras�n� art�r
        Line.sortingOrder = currentLayerOrder;

        // Par�ac�k Sistemi Olu�turma ve Y�nlendirme
        if (partik�l != null)
        {
            Vector2 direction = (startPos - lastPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            var shapeModule = partik�l.shape;
            shapeModule.rotation = new Vector3(0, angle + 180, 0);

            partik�l.transform.position = startPos;
            partik�l.Play(); // Par�ac�k sistemi ba�lat
        }
    }

    void UpdateLine(Vector2 newPos)
    {
        Line.positionCount++;
        Line.SetPosition(Line.positionCount - 1, newPos);

        // Par�ac�k sistemi konumunu �izginin son noktas�na ayarla
        if (partik�l != null)
        {
            // Par�ac�k sistemi rengini kalem rengi olarak ayarla
            var mainModule = partik�l.main;
            mainModule.startColor = kalemColor;

            // Par�ac�k sistemi boyutunu kalem kal�nl���na g�re ayarla
            mainModule.startSize = gameController.kal�nl�k * 10;

            partik�l.transform.position = newPos;
        }

    }
}
