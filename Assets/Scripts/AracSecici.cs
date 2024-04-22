using UnityEngine;
using UnityEngine.UI;

public class AracSecici : MonoBehaviour
{
    public Kalem kalem;
    public Kova kova;
    public Sprey sprey;
    public SilgiManager silgi;
    private static int currentLayerOrder = 0;

    public Button kalemButton;
    public Button kovaBucketButton;
    public Button spreyButton;
    public Button silgiButton;


    public AudioSource kalemses;
    public AudioSource spreyses;
    void Start()
    {
        // Butonlara týklama olaylarýný ekle
        kalemButton.onClick.AddListener(SelectPenTool);
        kovaBucketButton.onClick.AddListener(SelectPaintBucketTool);
        spreyButton.onClick.AddListener(SelectStampTool);
        silgiButton.onClick.AddListener(SelectEraserTool);
    }
   
    public static int GetCurrentLayerOrder()
    {
        return currentLayerOrder;
    }

    public static void IncreaseLayerOrder()
    {
        currentLayerOrder++;
    }
    void SelectPenTool()
    {
        // Kalemi seç
        kalem.enabled = true;
        kova.enabled = false;
        sprey.enabled = false;
        silgi.enabled = false;
        kalemses.Play();
    }

    void SelectPaintBucketTool()
    {
        // Kovayý seç
        kalem.enabled = false;
        kova.enabled = true;
        sprey.enabled = false;
        silgi.enabled = false;
    }

    void SelectStampTool()
    {
        // spreyi seç
        kalem.enabled = false;
        kova.enabled = false;
        sprey.enabled = true;
        silgi.enabled = false;
        spreyses.Play();
    }
    void SelectEraserTool()
    {
        // Silgi seç
        kalem.enabled = false;
        kova.enabled = false;
        sprey.enabled = false;
        silgi.enabled = true;
    }
}
