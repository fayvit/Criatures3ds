using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DadosDoPersonagem
{
    [SerializeField] private List<CriatureBase> criaturesAtivos = new List<CriatureBase>();
    [SerializeField] private List<CriatureBase> criaturesArmagedados = new List<CriatureBase>();
    [SerializeField] private int cristais = 0;

    private List<MbItens> itens = new List<MbItens>();
    private IndiceDeArmagedoms ultimoArmagedom = IndiceDeArmagedoms.planicieDeInfinity_b;
    private float tempoDoUltimoUsoDeItem = 0;
    private int criatureSai = 0;

    public int itemSai = 0;    
    public int maxCarregaveis = 5;

    public List<CriatureBase> CriaturesAtivos
    {
        get { return criaturesAtivos; }
        set { criaturesAtivos = value; }
    }

    public List<CriatureBase> CriaturesArmagedados
    {
        get { return criaturesArmagedados; }
        set { criaturesArmagedados = value; }
    }

    public List<MbItens> Itens
    {
        get { return itens; }
        set { itens = value; }
    }

    public int CriatureSai
    {
        get { return criatureSai; }
        set { criatureSai = value; }
    }

    public IndiceDeArmagedoms UltimoArmagedom
    {
        get { return ultimoArmagedom; }
        set { ultimoArmagedom = value; }
    }

    public int Cristais
    {
        get { return cristais; }
        set { cristais = value; }
    }

    public float TempoDoUltimoUsoDeItem
    {
        get { return tempoDoUltimoUsoDeItem; }
        set { tempoDoUltimoUsoDeItem = value; }
    }

    public void InicializadorDosDados()
    {
        
        CriaturesAtivos = new List<CriatureBase>() {
            new CriatureBase(nomesCriatures.Iruin,7),/*
            new CriatureBase(nomesCriatures.Cabecu,10),
            new CriatureBase(nomesCriatures.Babaucu,2),
            new CriatureBase(nomesCriatures.Florest,7),
            new CriatureBase(nomesCriatures.PolyCharm,7),*/
            
            

        };

        //CriaturesAtivos[0].CaracCriature.meusAtributos.PV.Corrente = 1;

        CriaturesArmagedados = new List<CriatureBase>() {
            new CriatureBase(nomesCriatures.Onarac,1),
            new CriatureBase(nomesCriatures.Babaucu,3),
            new CriatureBase(nomesCriatures.Wisks,2),
            new CriatureBase(nomesCriatures.Serpente,3)
        };


        //CriaturesAtivos[1].CaracCriature.meusAtributos.PV.Corrente = 0;
        //CriaturesAtivos[2].CaracCriature.meusAtributos.PV.Corrente = 2;


        
        Itens = new List<MbItens>()
        {
            PegaUmItem.Retorna(nomeIDitem.pergaminhoDePerfeicao,14),
            PegaUmItem.Retorna(nomeIDitem.maca,16),
            PegaUmItem.Retorna(nomeIDitem.pergArmagedom,2),
            PegaUmItem.Retorna(nomeIDitem.pergFuracaoDeFolhas,5),
            PegaUmItem.Retorna(nomeIDitem.pergaminhoDeFuga,10),
            PegaUmItem.Retorna(nomeIDitem.regador,10),
            PegaUmItem.Retorna(nomeIDitem.inseticida,2),
            PegaUmItem.Retorna(nomeIDitem.ventilador,2),
            PegaUmItem.Retorna(nomeIDitem.pergSinara,2),
            PegaUmItem.Retorna(nomeIDitem.pergAlana,1)
        };
        /*
        itens.Add(new item(nomeIDitem.maca) { estoque = 20 });
        itens.Add(new item(nomeIDitem.cartaLuva) { estoque = 3 });
        itens.Add(new item(nomeIDitem.pergArmagedom) { estoque = 7 });
        itens.Add(new item(nomeIDitem.pergSabre) { estoque = 5 });
        itens.Add(new item(nomeIDitem.pergSaida) { estoque = 5 });
        itens.Add(new item(nomeIDitem.pergGosmaDeInseto) { estoque = 8 });
        itens.Add(new item(nomeIDitem.pergGosmaAcida) { estoque = 8 });
        itens.Add(new item(nomeIDitem.pergMultiplicar) { estoque = 7 });
        itens.Add(new item(nomeIDitem.estatuaMisteriosa) { estoque = 1 });
        */


    }

    public bool TemCriatureVivo()
    {
        bool retorno = false;
        for (int i = 0; i < CriaturesAtivos.Count; i++)
        {
            if (CriaturesAtivos[i].CaracCriature.meusAtributos.PV.Corrente > 0)
                retorno = true;
        }

        return retorno;
    }

    public void TodosCriaturesPerfeitos()
    {
        for (int i = 0; i < CriaturesAtivos.Count; i++)
        {
            CriatureBase.EnergiaEVidaCheia(CriaturesAtivos[i]);
            int num = GameController.g.ContStatus.StatusDoHeroi.Count - 1;
            for (int j = num; j >= 0; j--)
                GameController.g.ContStatus.StatusDoHeroi[j].RetiraComponenteStatus();
        }
    }

    public int TemItem(nomeIDitem nome)
    {
        int tanto = 0;
        for (int i = 0; i < Itens.Count; i++)
        {
            if (Itens[i].ID == nome)
                tanto += Itens[i].Estoque;
        }

        return tanto;
    }

    public void AdicionaItem(nomeIDitem nomeItem, int quantidade)
    {
        if (nomeItem != nomeIDitem.cristais)
        {
            for (int i = 0; i < quantidade; i++)
            {
                AdicionaItem(nomeItem);
            }
        }
        else
        {
            cristais += quantidade;
        }
    }

    public void AdicionaItem(nomeIDitem nomeItem)
    {
        MbItens I = PegaUmItem.Retorna(nomeItem);
        bool foi = false;
        if (I.Acumulavel > 1)
        {

            int ondeTem = -1;
            for (int i = 0; i < Itens.Count; i++)
            {
                if (Itens[i].ID == I.ID)
                {
                    if (Itens[i].Estoque < Itens[i].Acumulavel)
                    {
                        if (!foi)
                        {
                            ondeTem = i;
                            foi = true;
                        }
                    }
                }
            }

            if (foi)
            {
                Itens[ondeTem].Estoque++;
            }
            else
            {
                Itens.Add(PegaUmItem.Retorna(nomeItem));
            }
        }
        else
        {
            Itens.Add(PegaUmItem.Retorna(nomeItem));
        }
    }

    public void ZeraUltimoUso()
    {
        for (int i = 0; i < criaturesAtivos.Count; i++)
        {
            for (int j = 0; j < criaturesAtivos[i].GerenteDeGolpes.meusGolpes.Count; j++)
            {
                criaturesAtivos[i].GerenteDeGolpes.meusGolpes[j].UltimoUso = Time.time- criaturesAtivos[i].GerenteDeGolpes.meusGolpes[j].TempoDeReuso;
            }
        }

        for (int i = 0; i < criaturesArmagedados.Count; i++)
        {
            for (int j = 0; j < criaturesArmagedados[i].GerenteDeGolpes.meusGolpes.Count; j++)
            {
                criaturesArmagedados[i].GerenteDeGolpes.meusGolpes[j].UltimoUso 
                    = Time.time - criaturesArmagedados[i].GerenteDeGolpes.meusGolpes[j].TempoDeReuso;
            }
        }

        TempoDoUltimoUsoDeItem = Time.time - MbItens.INTERVALO_DO_USO_DE_ITEM; ;

    }
}

[System.Serializable]
public struct UltimoArmagedomVisitado
{
    private float[] V;

    public UltimoArmagedomVisitado(Vector3 pos, NomesCenas cena)
    {
        V = new float[3] { pos[0], pos[1], pos[2] };
        NomeDaCena = cena;
    }

    public NomesCenas NomeDaCena { get; private set; }

    public Vector3 posHeroi
    {
        private set { }
        get
        {
            Vector3 V2 = new Vector3(V[0], V[1], V[2]);
            return V2;
        }
    }
}