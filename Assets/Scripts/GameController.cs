using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    public Slider sizeSlider; // Aray�zdeki boyut kayd�r�c�s�
    public float kal�nl�k = 0.1f;
    public CizimKaydedici cizimKaydedici;
    public new Camera camera;

    private void Awake()
    {
        if (File.Exists(cizimKaydedici.dosyaYolu))
        {
            cizimKaydedici.G�r�nt�y�Kaydet();
        }
        else
        {
            cizimKaydedici.ResmiYukle();
            camera.orthographicSize = 5.4f;
        }
        
        
    }
    void Start()
    {
        // Slider'�n de�er de�i�imini dinlemek i�in listener ekleyelim
        sizeSlider.onValueChanged.AddListener(delegate { OnSizeSliderValueChanged(); });
        // Ba�lang��ta kal�nl��� ayarlayal�m
        SetThickness(kal�nl�k);
       
        
    }

    // Slider de�eri de�i�ti�inde �a�r�lacak fonksiyon
    void OnSizeSliderValueChanged()
    {
        // Slider'�n de�erine g�re kal�nl��� ayarlayal�m
        SetThickness(sizeSlider.value);
    }

    // Kal�nl��� ayarlayan fonksiyon
    void SetThickness(float thickness)
    {
        kal�nl�k = thickness;
        // Burada kal�nl�k de�erini kullanarak �izim arac�n�z�n kal�nl���n� ayarlayabilirsiniz
        Debug.Log("Kal�nl�k: " + kal�nl�k);
    }
}
