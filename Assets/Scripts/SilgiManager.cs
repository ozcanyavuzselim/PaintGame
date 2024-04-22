using UnityEngine;

public class SilgiManager : MonoBehaviour
{
    public GameObject tuval; // �izim alan� GameObject'i
    public GameController gameController;
    public ParticleSystem partikul;

    private LineRenderer silgiLine; // �u anda silinen �izgi

    private Vector2 lastErasedPos; // Son silinmi� konum

    private Bounds �izimS�n�rlar�; // �izim alan�n�n s�n�rlar�

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
        // Kullan�c� sol fare tu�unu bas�l� tutarsa veya dokunursa silme i�lemi yap
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (�izimS�n�rlar�.Contains(mousePos)) // Fare konumu �izim alan� i�indeyse
            {
                if (silgiLine == null) // E�er �u anda bir �izgi silinmiyorsa
                {
                    StartErasing(mousePos);
                }
                else // E�er �u anda bir �izgi siliniyorsa, �izgiyi g�ncelle
                {
                    UpdateErasedLine(mousePos);

                }
            }
        }
        else // Kullan�c� fareyi b�rakt�ysa, �u anda silinen �izgiyi temizle
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
        silgiLine.startWidth = gameController.kal�nl�k;
        silgiLine.endWidth = gameController.kal�nl�k;
        silgiLine.material = new Material(Shader.Find("Sprites/Default"));
        silgiLine.startColor = Color.white; // Silgiyi beyaza ayarla
        silgiLine.endColor = Color.white;
        silgiLine.SetPosition(0, startPos);
        silgiLine.SetPosition(1, startPos);
        lastErasedPos = startPos;
        int currentLayerOrder = AracSecici.GetCurrentLayerOrder(); // Layer s�ras�n� al
        AracSecici.IncreaseLayerOrder(); // Layer s�ras�n� art�r
        silgiLine.sortingOrder = currentLayerOrder;

        // Par�ac�k Sistemi Olu�turma ve Y�nlendirme
        if (partikul != null)
        {
            Vector2 direction = (startPos - lastErasedPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            var shapeModule = partikul.shape;
            shapeModule.rotation = new Vector3(0, angle + 180, 0);

            partikul.transform.position = startPos;
            partikul.Play(); // Par�ac�k sistemi ba�lat
        }

    }

    void UpdateErasedLine(Vector2 newPos)
    {
        silgiLine.positionCount++;
        silgiLine.SetPosition(silgiLine.positionCount - 1, newPos);
        lastErasedPos = newPos;

        // Par�ac�k sistemi konumunu �izginin son noktas�na ayarla
        if (partikul != null)
        {
            // Par�ac�k sistemi rengini kalem rengi olarak ayarla
            var mainModule = partikul.main;
            mainModule.startColor = Color.white; 

            // Par�ac�k sistemi boyutunu kalem kal�nl���na g�re ayarla
            mainModule.startSize = gameController.kal�nl�k * 10;

            partikul.transform.position = newPos;
        }
    }
}
