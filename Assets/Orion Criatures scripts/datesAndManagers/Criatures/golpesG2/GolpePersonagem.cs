using UnityEngine;
using System;

[Serializable]
public class GolpePersonagem
{
    [SerializeField]private nomesGolpes _nome = nomesGolpes.nulo;
    [SerializeField]private colisor _colisor = new colisor();

    [SerializeField]private int nivelDoGolpe = 1;
    [SerializeField]private float modPersonagem = 0;
    [SerializeField]private float acimaDoChao = 0;
    [SerializeField]private float distanciaEmissora = 0;
    [SerializeField]private float tempoDeInstancia = 0;
    [SerializeField]private float taxaDeUso = 1;

    public static GolpePersonagem RetornaGolpePersonagem(GameObject G,nomesGolpes nomeDoGolpe)
    {
        CriatureBase criatureBase = G.GetComponent<CreatureManager>().MeuCriatureBase;
        GerenciadorDeGolpes gg = criatureBase.GerenteDeGolpes;
        GolpePersonagem gP = gg.ProcuraGolpeNaLista(criatureBase.NomeID, nomeDoGolpe);
        return gP;   
    }   
    
    public float AcimaDoChao
    {
        get
        {
            return acimaDoChao;
        }

        set
        {
            acimaDoChao = value;
        }
    }

    public colisor Colisor
    {
        get
        {
            return _colisor;
        }

        set
        {
            _colisor = value;
        }
    }

    public float DistanciaEmissora
    {
        get
        {
            return distanciaEmissora;
        }

        set
        {
            distanciaEmissora = value;
        }
    }

    public nomesGolpes Nome
    {
        get
        {
            return _nome;
        }

        set
        {
            _nome = value;
        }
    }

    public float TempoDeInstancia
    {
        get
        {
            return tempoDeInstancia;
        }

        set
        {
            tempoDeInstancia = value;
        }
    }

    public float TaxaDeUso
    {
        get
        {
            return taxaDeUso;
        }

        set
        {
            taxaDeUso = value;
        }
    }

    public int NivelDoGolpe
    {
        get
        {
            return nivelDoGolpe;
        }

        set
        {
            nivelDoGolpe = value;
        }
    }

    public float ModPersonagem
    {
        get
        {
            return modPersonagem;
        }

        set
        {
            modPersonagem = value;
        }
    }
}
