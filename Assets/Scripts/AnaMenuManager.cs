using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenuManager : MonoBehaviour
{
    

    // Sayfa numarasýný alýp oyun sahnesine geçiþ yapacak metot
    public void SahneyeGecis(int sayfaNo)
    {
        SceneManager.LoadScene(sayfaNo);
    }
    public void OyundanCik()
    {
        
        Application.Quit();
    }
}
