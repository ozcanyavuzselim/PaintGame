using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public CizimKaydedici cizimKaydedici;
    // Sayfa numaras�n� al�p oyun sahnesine ge�i� yapacak metot
    public void SahneyeGecis(int sayfaNo)
    {
        SceneManager.LoadScene(sayfaNo);
        cizimKaydedici.G�r�nt�y�Kaydet();
    }
}
