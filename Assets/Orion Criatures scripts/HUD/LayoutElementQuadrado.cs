using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LayoutElementQuadrado
{
    List<LayoutElement> esseLay = new List<LayoutElement>();
    RectTransform rtDoPai;
    VerticalLayoutGroup layVG;
    HorizontalLayoutGroup layHG;

    int tamanhoGuardado = 0;

    public enum FocoDoAjustamento
    {
        altura,
        largura
    }

    public float Espaco
    {
        get {
            float espaco = 0;
            if (layHG)
                espaco = layHG.spacing;

            if (layVG)
                espaco = layVG.spacing;

            return espaco;
        }

        set {
            if (layHG)
                layHG.spacing = value;

            if (layVG)
                layVG.spacing = value;
        }
    }

    public LayoutElementQuadrado(GameObject G)
    {
        esseLay.AddRange( G.GetComponentsInChildren<LayoutElement>());
        rtDoPai = G.transform.parent.GetComponent<RectTransform>();
        layVG = G.transform.GetComponent<VerticalLayoutGroup>();
        layHG = G.transform.GetComponent<HorizontalLayoutGroup>();
        tamanhoGuardado = esseLay.Count;
        //Debug.Log(layVG + " : " + layHG);
    }

    public int TamanhoDoVetorDeLayouts
    {
        get { return tamanhoGuardado; }
        set { tamanhoGuardado = value; }
    }

    public void AjustamentoDeLadoFixo(FocoDoAjustamento foco)
    {
        float largura = rtDoPai.rect.width;
        float altura = rtDoPai.rect.height;
        float tamanho = 0.85f * largura;

        

        if (FocoDoAjustamento.altura == foco)
            tamanho = 0.8f * altura;

        //Debug.Log(largura + " : " + altura+" : "+tamanho);

        if (esseLay.Count!=tamanhoGuardado)
        {
            esseLay = new List<LayoutElement>();
            esseLay.AddRange(rtDoPai.transform.GetComponentsInChildren<LayoutElement>());
        }
            

        for (int i = 0; i < tamanhoGuardado; i++)
        {
            //Debug.Log(tamanhoGuardado);
            if (esseLay[i] != null)
            {
                esseLay[i].preferredWidth = tamanho;
                esseLay[i].preferredHeight = tamanho;
            }
        }
    }

    public void AjustaTamanhoQuadrado(FocoDoAjustamento foco,int numeroDeElementos,int distanciaSeparadora)
    {
        float largura = rtDoPai.rect.width;
        float altura = rtDoPai.rect.height;

        float focado = 0;
        float secundario = 0;

        if (foco == FocoDoAjustamento.largura)
        {
            focado = largura;
            secundario = altura;
        }
        else if (foco == FocoDoAjustamento.altura)
        {
            focado = altura;
            secundario = largura;
        }

        //Debug.Log(altura + " : " + largura);

        int i = 0;
        if (focado / numeroDeElementos - (numeroDeElementos-1)*distanciaSeparadora > secundario)
        {


            for (i = 0; i < esseLay.Count; i++)
            {
                esseLay[i].preferredWidth = 0.89f*secundario;
                esseLay[i].preferredHeight = 0.89f * secundario;
                
            }

            int spacing = (int)((focado/numeroDeElementos-secundario)/numeroDeElementos);

            if (layHG)
                layHG.spacing = spacing;

            if (layVG)
                layVG.spacing = spacing;

            
        }
        else
        {
            for (i = 0; i < esseLay.Count; i++)
            {
                esseLay[i].preferredWidth = focado / numeroDeElementos - (numeroDeElementos - 1) * distanciaSeparadora;
                esseLay[i].preferredHeight = focado / numeroDeElementos - (numeroDeElementos - 1) * distanciaSeparadora;
            }
        }
    }
}