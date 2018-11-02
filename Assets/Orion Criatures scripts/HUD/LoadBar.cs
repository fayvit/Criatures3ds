using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadBar : MonoBehaviour
{
    [SerializeField]private RectTransform bar;

    private Image img;
    private float posOriginalMaxDaAncora;
    private float posOriginalMinDaAncora;

    //[Range(0,1)]public float teste = 1;
    // Use this for initialization
    void Awake()
    {
        posOriginalMaxDaAncora = bar.anchorMax.x;
        posOriginalMinDaAncora = bar.anchorMin.x;
    }
    void Start()
    {
        GlobalController.g.FadeV.LimparFade();
        img = bar.GetComponent<Image>();
        //GetComponentInChildren<Text>().text = Application.persistentDataPath;
    }
    // Update is called once per frame
    public void ValorParaBarra(float x)
    {
        gameObject.SetActive(true);
        PercentagemDeBarraNoY(bar,x);
    }

    void PercentagemDeBarraNoY(RectTransform barra, float percentagem)
    {
        if (img.color.a == 0 && percentagem > 0)
            img.color = new Color(img.color.r,img.color.g,img.color.b,1);

        barra.anchorMax = new Vector2(
            (posOriginalMaxDaAncora - posOriginalMinDaAncora) * percentagem + posOriginalMinDaAncora,
            barra.anchorMax.y
            );
    }
}
