using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MapaPorScript : MonoBehaviour
{
    public Texture2D img;
    [SerializeField] private  List<CorPorPrefab> coresPorPrefabs = new List<CorPorPrefab>();
    [SerializeField] private DimensoesCustomizaveis dimensoesCustomizaveis = new DimensoesCustomizaveis();

    [System.Serializable]
    private class DimensoesCustomizaveis
    {
        public int initialPixelHeight = 0;
        public int finalPixelHeight = 0;
        public int initialPixelWidth = 0;
        public int finalPixelWidth = 0;
        public Vector3 tamanhoDoBloco = new Vector3(10,10,10);
    }

    public bool vai;
    public bool procurarCores;
    private Transform grandePai;

    [System.Serializable]
    private class CorPorPrefab
    {
        public Color cor;
        public GameObject G;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (vai)
        {
            if (dimensoesCustomizaveis.finalPixelWidth == 0)
                dimensoesCustomizaveis.finalPixelWidth = img.width;

            if (dimensoesCustomizaveis.finalPixelHeight == 0)
                dimensoesCustomizaveis.finalPixelHeight = img.height;

            if (dimensoesCustomizaveis.initialPixelHeight > dimensoesCustomizaveis.finalPixelHeight 
                ||
                dimensoesCustomizaveis.finalPixelHeight > img.height 
                ||
                dimensoesCustomizaveis.initialPixelWidth > dimensoesCustomizaveis.finalPixelWidth 
                ||
                dimensoesCustomizaveis.finalPixelWidth > img.width)
            {
                Debug.LogError("As dimensões utilizadas são incompativeis");
            }
            else
            {
                grandePai = new GameObject().transform;
                grandePai.name = "grandePai";
                for (int i = dimensoesCustomizaveis.initialPixelWidth; i < dimensoesCustomizaveis.finalPixelWidth; i++)
                    for (int j = dimensoesCustomizaveis.initialPixelHeight; j < dimensoesCustomizaveis.finalPixelHeight; j++)
                    {
                        ColoqueBLoco(img.GetPixel(i, j), i, j);
                    }
            }
            vai = false;
        }

        if (procurarCores)
        {
            for (int i = 0; i < img.width; i++)
                for (int j = 0; j < img.height; j++)
                {
                    bool coloca = true;
                    for (int k = 0; k < coresPorPrefabs.Count; k++)
                    {

                        if (coresPorPrefabs[k].cor == img.GetPixel(i, j))
                            coloca = false;
                    }

                    if (coloca)
                        coresPorPrefabs.Add(new CorPorPrefab() { cor = img.GetPixel(i, j) });
                }
            procurarCores = false;
        }
    }

    void ColoqueBLoco(Color C, int x, int y)
    {
        GameObject G = RetornaObjectDaCor(C);
        
        if (G != null)
            Instantiate(G, new Vector3(x *dimensoesCustomizaveis.tamanhoDoBloco.x , 0, y*dimensoesCustomizaveis.tamanhoDoBloco.z), Quaternion.identity,grandePai);
    }

    GameObject RetornaObjectDaCor(Color C)
    {
        GameObject G = null;
        for (int i = 0; i < coresPorPrefabs.Count; i++)
        {
            if (coresPorPrefabs[i].cor == C)
                G = coresPorPrefabs[i].G;
        }

        return G;
    }
}
