using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CizimKaydedici : MonoBehaviour
{
    public Camera kamera;
    public string dosyaYolu;
    public SpriteRenderer spriteRenderer;

    public void G�r�nt�y�Kaydet()
    {

        // RenderTexture olu�tur ve kameran�n g�r�nt�s�n� bu RenderTexture'a render et
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        kamera.targetTexture = renderTexture;
        kamera.Render();

        // RenderTexture'dan g�r�nt�y� Texture2D'ye kopyala
        Texture2D resimDokusu = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        resimDokusu.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        resimDokusu.Apply();
        RenderTexture.active = null;

        // Kameran�n hedefini s�f�rla
        kamera.targetTexture = null;

        // Texture2D'yi PNG format�nda kaydet
        byte[] resimVerileri = resimDokusu.EncodeToPNG();
        string dosyaYolu = Path.Combine(Application.persistentDataPath, "kaydedilmis_resim.png");
        File.WriteAllBytes(dosyaYolu, resimVerileri);

        Debug.Log("Resim kaydedildi: " + dosyaYolu);

    }

    public void ResmiYukle()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer referans� null. L�tfen ge�erli bir SpriteRenderer atay�n.");
            return;
        }

        dosyaYolu = Path.Combine(Application.persistentDataPath, "kaydedilmis_resim.png");
        if (!File.Exists(dosyaYolu))
        {
            Debug.Log("Resim dosyas� bulunamad�: " + dosyaYolu);
            return;
        }

        byte[] resimVerileri = File.ReadAllBytes(dosyaYolu);

        Texture2D resimDokusu = new Texture2D(2, 2); // Ge�ici boyut
        if (!resimDokusu.LoadImage(resimVerileri))
        {
            Debug.LogError("Resim y�klenirken bir hata olu�tu.");
            return;
        }
        Debug.Log("Resim y�klenmdi ");
        // Texture2D'yi Sprite'a d�n��t�r
        Rect rect = new Rect(0, 0, resimDokusu.width, resimDokusu.height); // Resmin boyutlar�na g�re Rect olu�tur
        Sprite sprite = Sprite.Create(resimDokusu, rect, Vector2.one * 0.5f);
        Vector3 scale = new Vector3(1f, 1f, 1f); // Scale de�erlerini 1.1 olarak ayarla
        spriteRenderer.transform.localScale = scale;
        // Sprite'� SpriteRenderer'a ata
        spriteRenderer.sprite = sprite;
    }
}
