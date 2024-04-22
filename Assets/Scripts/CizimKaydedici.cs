using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CizimKaydedici : MonoBehaviour
{
    public Camera kamera;
    public string dosyaYolu;
    public SpriteRenderer spriteRenderer;

    public void GörüntüyüKaydet()
    {

        // RenderTexture oluþtur ve kameranýn görüntüsünü bu RenderTexture'a render et
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        kamera.targetTexture = renderTexture;
        kamera.Render();

        // RenderTexture'dan görüntüyü Texture2D'ye kopyala
        Texture2D resimDokusu = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        resimDokusu.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        resimDokusu.Apply();
        RenderTexture.active = null;

        // Kameranýn hedefini sýfýrla
        kamera.targetTexture = null;

        // Texture2D'yi PNG formatýnda kaydet
        byte[] resimVerileri = resimDokusu.EncodeToPNG();
        string dosyaYolu = Path.Combine(Application.persistentDataPath, "kaydedilmis_resim.png");
        File.WriteAllBytes(dosyaYolu, resimVerileri);

        Debug.Log("Resim kaydedildi: " + dosyaYolu);

    }

    public void ResmiYukle()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer referansý null. Lütfen geçerli bir SpriteRenderer atayýn.");
            return;
        }

        dosyaYolu = Path.Combine(Application.persistentDataPath, "kaydedilmis_resim.png");
        if (!File.Exists(dosyaYolu))
        {
            Debug.Log("Resim dosyasý bulunamadý: " + dosyaYolu);
            return;
        }

        byte[] resimVerileri = File.ReadAllBytes(dosyaYolu);

        Texture2D resimDokusu = new Texture2D(2, 2); // Geçici boyut
        if (!resimDokusu.LoadImage(resimVerileri))
        {
            Debug.LogError("Resim yüklenirken bir hata oluþtu.");
            return;
        }
        Debug.Log("Resim yüklenmdi ");
        // Texture2D'yi Sprite'a dönüþtür
        Rect rect = new Rect(0, 0, resimDokusu.width, resimDokusu.height); // Resmin boyutlarýna göre Rect oluþtur
        Sprite sprite = Sprite.Create(resimDokusu, rect, Vector2.one * 0.5f);
        Vector3 scale = new Vector3(1f, 1f, 1f); // Scale deðerlerini 1.1 olarak ayarla
        spriteRenderer.transform.localScale = scale;
        // Sprite'ý SpriteRenderer'a ata
        spriteRenderer.sprite = sprite;
    }
}
