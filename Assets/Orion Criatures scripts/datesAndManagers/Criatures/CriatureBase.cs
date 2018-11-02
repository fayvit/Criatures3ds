using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class CriatureBase : ICloneable
{
    [SerializeField]    private nomesCriatures nome;
    [SerializeField]    private CaracteristicasDeCriature caracCriature;
    [SerializeField]    private GerenciadorDeGolpes gerenteG;
    [SerializeField]    private CaracteristicasDeMovimentacao mov;
    [SerializeField]    private List<DatesForTemporaryStatus> statusTemporarios = new List<DatesForTemporaryStatus>();

    public float alturaCamera = 4f;
    public float distanciaCamera = 5.5f;
    public float alturaCameraLuta = 10f;
    public float distanciaCameraLuta = 4.5f;

    /*
    Esse primeiro construtor é utilizado no banco de dados 
    de Criatures Base contido na classe personagemG2
    */
    public CriatureBase() { }
    public CriatureBase(nomesCriatures X, int nivel = 1)
    {
        CriatureBase Y = personagemG2.RetornaUmCriature(X); //(CriatureBase)(personagemG2.Criatures[X].Clone());
        caracCriature = Y.CaracCriature;
        gerenteG = Y.GerenteDeGolpes;
        mov = Y.Mov;
        alturaCamera = Y.alturaCamera;
        distanciaCamera = Y.distanciaCamera;
        alturaCameraLuta = Y.alturaCameraLuta;
        distanciaCameraLuta = Y.distanciaCameraLuta;
        nome = X;

        if (nivel > 1)
            caracCriature.incrementaNivel(nivel);

        gerenteG.meusGolpes = new List<GolpeBase>();
        gerenteG.meusGolpes.AddRange(GolpesAtivos(nivel, gerenteG.listaDeGolpes.ToArray()));


        VerificaSomaDeTaxas();
    }

    void VerificaSomaDeTaxas()
    {
        Atributos a = caracCriature.meusAtributos;
        float comoAssim = a.PV.Taxa + a.PE.Taxa + a.Ataque.Taxa + a.Defesa.Taxa + a.Poder.Taxa;
        if (comoAssim != 1)
        {
            Debug.Log("O criature " + nome.ToString() + " não tem a soma das taxas igual a 1: " + comoAssim);
        }
    }

    public GolpeBase[] GolpesAtivos(int nivel, GolpePersonagem[] listaGolpes)
    {
        List<GolpePersonagem> L = new List<GolpePersonagem>();
        int i = 0;
        //int N = -1;
        while (i < listaGolpes.Length)
        {
            if (listaGolpes[i].NivelDoGolpe <= nivel && listaGolpes[i].NivelDoGolpe > -1)
            {
                if (L.Count < 4)
                    L.Add(listaGolpes[i]);
                else
                {
                    L[0] = L[1];
                    L[1] = L[2];
                    L[2] = L[3];
                    L[3] = listaGolpes[i];
                }
            }
            i++;
        }

        GolpeBase[] Y = new GolpeBase[L.Count];
        for (i = 0; i < L.Count; i++)
        {
            Y[i] = PegaUmGolpeG2.RetornaGolpe(L[i].Nome);

        }
        return Y;
    }

    public static void EnergiaEVidaCheia(CriatureBase C)
    {
        Atributos A = C.CaracCriature.meusAtributos;
        A.PV.Corrente = A.PV.Maximo;
        A.PE.Corrente = A.PE.Maximo;
    }

    public void EstadoPerfeito()
    {
        EnergiaEVidaCheia(this);
        int num = statusTemporarios.Count-1;
        for (int i = num; i >=0; i--)
        {
            int num2 = GameController.g.ContStatus.StatusDoHeroi.Count-1;
            for (int j = num2; j >= 0; j--)
                if (statusTemporarios.IndexOf(GameController.g.ContStatus.StatusDoHeroi[j].Dados) == i)
                    GameController.g.ContStatus.StatusDoHeroi[j].RetiraComponenteStatus();
        }
    }

    /*
    public bool SouOMesmoQue(CriatureBase C)
    {
        bool retorno = false;

        if (C.NomeID == nome
            &&
            C.CaracCriature.mNivel.Nivel==caracCriature.mNivel.Nivel
            &&
            C.CaracCriature.mNivel.XP == caracCriature.mNivel.XP
            &&
            C.CaracCriature.meusAtributos.PV.Corrente == caracCriature.meusAtributos.PV.Corrente
            &&
            C.CaracCriature.meusAtributos.PV.Maximo == caracCriature.meusAtributos.PV.Maximo
            &&
            C.CaracCriature.meusAtributos.PE.Corrente == caracCriature.meusAtributos.PE.Corrente
            &&
            C.CaracCriature.meusAtributos.PE.Maximo == caracCriature.meusAtributos.PE.Maximo
            )
        retorno = true;
        return retorno;
    }*/

    public object Clone()
    {
        CriatureBase retorno =
        new CriatureBase()
        {
            NomeID = this.NomeID,
            alturaCamera = this.alturaCamera,
            distanciaCamera = this.distanciaCamera,
            alturaCameraLuta = this.alturaCameraLuta,
            distanciaCameraLuta = this.distanciaCameraLuta,
            CaracCriature = new CaracteristicasDeCriature()
            {
                meusTipos = (nomeTipos[])this.CaracCriature.meusTipos.Clone(),
                distanciaFundamentadora = this.CaracCriature.distanciaFundamentadora,
                meusAtributos = {
                    PV = { Taxa = caracCriature.meusAtributos.PV.Taxa},
                    PE = { Taxa = caracCriature.meusAtributos.PE.Taxa},
                    Ataque = { Taxa = caracCriature.meusAtributos.Ataque.Taxa},
                    Defesa = { Taxa = caracCriature.meusAtributos.Defesa.Taxa},
                    Poder = { Taxa = caracCriature.meusAtributos.Poder.Taxa }
                },
                contraTipos = caracCriature.contraTipos
            },
            GerenteDeGolpes = new GerenciadorDeGolpes()
            {
                listaDeGolpes = gerenteG.listaDeGolpes,
            },
            Mov = new CaracteristicasDeMovimentacao()
            {
                velocidadeAndando = mov.velocidadeAndando,
                caracPulo = new CaracteristicasDePulo()
                {
                    alturaDoPulo = mov.caracPulo.alturaDoPulo,
                    tempoMaxPulo = mov.caracPulo.tempoMaxPulo,
                    velocidadeSubindo = mov.caracPulo.velocidadeSubindo,
                    velocidadeDescendo = mov.caracPulo.velocidadeDescendo,
                    velocidadeDuranteOPulo = mov.caracPulo.velocidadeDuranteOPulo,
                    amortecimentoNaTransicaoDePulo = mov.caracPulo.amortecimentoNaTransicaoDePulo
                }
            }
        };
        return retorno;
    }

    public CaracteristicasDeCriature CaracCriature
    {
        get { return caracCriature; }
        set { caracCriature = value; }
    }

    public GerenciadorDeGolpes GerenteDeGolpes
    {
        get { return gerenteG; }
        set { gerenteG = value; }
    }

    public CaracteristicasDeMovimentacao Mov
    {
        get { return mov; }
        set { mov = value; }
    }

    public nomesCriatures NomeID
    {
        get { return nome; }
        set { nome = value; }
    }

    public List<DatesForTemporaryStatus> StatusTemporarios
    {
        get { return statusTemporarios; }
        set { statusTemporarios = value; }
    }

    public GerenciadorDeExperiencia G_XP
    {
        get { return (GerenciadorDeExperiencia)caracCriature.mNivel; }
    }

    public string NomeEmLinguas
    {
        get { return NomeID.ToString();}
    }
}
