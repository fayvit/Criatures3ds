using UnityEngine;
using System.Collections;

[System.Serializable]
public class ElementosDoJogo
{
    public GameObject[] doJogo;
    public Material[] materiais;
    public Sprite uiDefault;
    public Sprite uiDestaque;

    public GameObject retorna(DoJogo doJogo)
    {
        return retorna(doJogo.ToString(), "doJogo");
    }

    public GameObject retorna(Huds doJogo)
    {
        return retorna(doJogo.ToString(), "doJogo");
    }

    public GameObject retorna(nomesCriatures nome)
    {
        return retorna(nome.ToString(), "criature");
    }

    public GameObject retorna(string ele, string oq = "doJogo")
    {

        GameObject retorno = null;

        switch (oq)
        {
            case "doJogo":
                retorno = (GameObject)Resources.Load("actives/" + ele);
            break;
            case "criature":
            case "Criature":
                retorno = criature(ele);
            break;
            case "colisor":
                retorno = retornaColisor(ele);
            break;
        }


        return retorno;
    }

    public GameObject criature(string ele)
    {
        return (GameObject)Resources.Load("criatures/" + ele);
    }

    public GameObject retornaColisor(string ele)
    {

        return (GameObject)Resources.Load("colisores/" + ele);
    }

    public Texture2D RetornaMini(nomesCriatures nome)
    {
        return (Texture2D)Resources.Load("miniCriatures/" + nome);//retornaMini(nome.ToString(),"criature");
    }

    public Texture2D RetornaMini(nomeIDitem nome)
    {
        return (Texture2D)Resources.Load("miniItens/" + nome);//return retornaMini(nome.ToString(), "itens");
    }
    public Texture2D RetornaMini(nomesGolpes nome)
    {
        return (Texture2D)Resources.Load("miniGolpes/" + nome);//return retornaMini(nome.ToString(), "golpe");
    }
    
    public Texture2D RetornaMini(TipoStatus nome)
    {
        return (Texture2D)Resources.Load("miniStatus/" + nome);//return retornaMini(nome.ToString(), "golpe");
    }
}

/// <summary>
/// Enumerador para gameObjects que são instanciados durante o jogo
/// </summary>
public enum DoJogo
{
    rajadaDeAgua,
    particulasDerrotado,
    AlvoFoco,
    impactoDeAgua,
    GiraCubo,
    particulaLuvaDeGuarde,
    raioDeLuvaDeGuarde,
    acaoDeCura1,
    particulasCoisasBoasUI,
    particulasUpeiDeNivel,
    encontro,
    raioEletrico,
    animaPE,
    poeiraAoVento,
    particulaDasPedraPuzzle,
    perfeicao,
    particulaDaFuga,
    curaDeArmagedom,
    particulaDaDefesaPergaminhoFora,
    particulaDoAtaquePergaminhoFora,
    particulaDoPoderPergaminhoFora,
    particulaDoPVpergaminho,
    particulaDoPEpergaminho,
    caindoNaArmadilhaChao,
    pegueiCristal,
    particulasEnvenenado,
    animaAntiStatus,
    particulasAmedrontado,
    particulasFraco
}

/// <summary>
/// Enumerador para tipos de HUDs instanciadas durante o jogo
/// </summary>
public enum Huds
{
    HUD_Vida,
    HUD_Golpes,
    HUD_Criatures,
    HUD_Itens
}
/// <summary>
/// Enumerador para miniaturas que são chamadas durante o jogo
/// </summary>
public enum RetornaMiniTipos
{
    golpe,
    criature,
    itens,
    status
}
