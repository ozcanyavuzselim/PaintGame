using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    public Slider sizeSlider; // Arayüzdeki boyut kaydýrýcýsý
    public float kalýnlýk = 0.1f;
    public CizimKaydedici cizimKaydedici;
    public new Camera camera;

    private void Awake()
    {
        if (File.Exists(cizimKaydedici.dosyaYolu))
        {
            cizimKaydedici.GörüntüyüKaydet();
        }
        else
        {
            cizimKaydedici.ResmiYukle();
            camera.orthographicSize = 5.4f;
        }
        
        
    }
    void Start()
    {
        // Slider'ýn deðer deðiþimini dinlemek için listener ekleyelim
        sizeSlider.onValueChanged.AddListener(delegate { OnSizeSliderValueChanged(); });
        // Baþlangýçta kalýnlýðý ayarlayalým
        SetThickness(kalýnlýk);
       
        
    }

    // Slider deðeri deðiþtiðinde çaðrýlacak fonksiyon
    void OnSizeSliderValueChanged()
    {
        // Slider'ýn deðerine göre kalýnlýðý ayarlayalým
        SetThickness(sizeSlider.value);
    }

    // Kalýnlýðý ayarlayan fonksiyon
    void SetThickness(float thickness)
    {
        kalýnlýk = thickness;
        // Burada kalýnlýk deðerini kullanarak çizim aracýnýzýn kalýnlýðýný ayarlayabilirsiniz
        Debug.Log("Kalýnlýk: " + kalýnlýk);
    }
}
