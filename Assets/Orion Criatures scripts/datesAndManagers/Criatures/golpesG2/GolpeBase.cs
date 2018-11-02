using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GolpeBase : IGolpeBase
{
    [SerializeField] private ContainerDeCaracteristicasDeGolpe container;
    [System.NonSerialized]private Vector3 dirDeREpulsao = Vector3.zero;

    public GolpeBase(ContainerDeCaracteristicasDeGolpe container)
    {
        this.container = container;
    }

    public float ColisorScale
    {
        get { return container.colisorScale; }
    }

    public caracGolpe Caracteristica
    {
        get
        {
            return container.carac;
        }
    }

    public int CustoPE
    {
        get
        {
            return container.custoPE;
        }
    }

    public int ModCorrente
    {
        get
        {
            return container.modCorrente;
        }

        set
        {
            container.modCorrente = value;
        }
    }

    public float ModTempoDeReuso
    {
        get
        {
            return container.modTempoDeReuso;
        }

        set
        {
            container.modTempoDeReuso = value;
        }
    }

    public nomesGolpes Nome
    {
        get
        {
            return container.nome;
        }
    }

    public int PotenciaCorrente
    {
        get
        {
            int retorno = container.potenciaCorrente+container.modCorrente;

            if (retorno > container.potenciaMaxima)
                retorno = container.potenciaMaxima;
            else if (retorno < container.potenciaMinima)
                retorno = container.potenciaMinima;

            return retorno;
        }
    }

    public int DanoMaximo {
        get { return container.danoMaximo; }
    }

    public float DistanciaDeRepulsao
    {
        get
        {
            return container.distanciaDeRepulsao;
        }
    }

    public float VelocidadeDeRepulsao
    {
        get { return container.velocidadeDeRepulsao; }
    }

    public float VelocidadeDeGolpe {
        get { return container.velocidadeDeGolpe; }
    }

    public float TempoDeDestroy
    {
        get
        {
            return container.tempoDeDestroy;
        }
    }

    public float TempoDeMoveMax
    {
        get
        {
            return container.tempoDeMoveMax;
        }
    }

    public float TempoDeMoveMin
    {
        get
        {
            return container.tempoDeMoveMin;
        }
    }

    public float TempoDeReuso
    {
        get
        {
            return container.tempoDeReuso;
        }
    }

    public float TempoNoDano
    {
        get
        {
            return container.TempoNoDano;
        }
    }

    public nomeTipos Tipo
    {
        get
        {
            return container.tipo;
        }
    }

    public float UltimoUso
    {
        get
        {
            return container.ultimoUso;
        }

        set
        {
            container.ultimoUso = value;
        }
    }

    public Vector3 DirDeREpulsao
    {
        get
        {
            return dirDeREpulsao;
        }

        set
        {
            dirDeREpulsao = value;
        }
    }

    public bool PodeNoAr
    {
        get
        {
            return container.podeNoAr;
        }
    }

    public virtual void IniciaGolpe(GameObject G)
    {
        throw new NotImplementedException();
    }

    public virtual void UpdateGolpe(GameObject G)
    {
        throw new NotImplementedException();
    }

    public virtual void FinalizaEspecificoDoGolpe()
    {

    }

    public virtual void VerificaAplicaStatus(CriatureBase atacante, CreatureManager atacado)
    {

    }

    public string NomeEmLinguas()
    {
        string[] arr = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.nomesDosGolpes).ToArray();
        if (arr.Length > (int)Nome)
            return arr[(int)Nome];
        else
        {
            Debug.LogError("O array de nomes de golpes não contem um nome para o ID= "+Nome);
            return Nome.ToString();// BancoDeTextos.falacoes[heroi.lingua]["listaDeGolpes"][(int)Nome];
        }
    }

    public static string NomeEmLinguas(nomesGolpes nome)
    {
        string[] arr = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.nomesDosGolpes).ToArray();
        if (arr.Length > (int)nome)
            return arr[(int)nome];
        else
        {
            Debug.LogError("O array de nomes de golpes não contem um nome para o ID= " + nome);
            return nome.ToString();// BancoDeTextos.falacoes[heroi.lingua]["listaDeGolpes"][(int)Nome];
        }
    }
}
