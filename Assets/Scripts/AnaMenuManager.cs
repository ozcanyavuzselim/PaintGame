using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenuManager : MonoBehaviour
{
    

    // Sayfa numaras�n� al�p oyun sahnesine ge�i� yapacak metot
    public void SahneyeGecis(int sayfaNo)
    {
        SceneManager.LoadScene(sayfaNo);
    }
    public void OyundanCik()
    {
        
        Application.Quit();
    }
}
