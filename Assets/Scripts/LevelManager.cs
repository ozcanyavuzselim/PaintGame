using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public CizimKaydedici cizimKaydedici;
    // Sayfa numarasýný alýp oyun sahnesine geçiþ yapacak metot
    public void SahneyeGecis(int sayfaNo)
    {
        SceneManager.LoadScene(sayfaNo);
        cizimKaydedici.GörüntüyüKaydet();
    }
}
