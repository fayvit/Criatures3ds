using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemConsumivelBase : MbItens
{
    [System.NonSerialized]protected CreatureManager CriatureAlvoDoItem;
    private const float TEMPO_DE_ANIMA_CURA_1 = 1.5f;

    public ItemConsumivelBase(ContainerDeCaracteristicasDeItem C) : base(C) { }

    public override void IniciaUsoDeMenu(GameObject dono)
    {
        GameController.g.HudM.P_EscolheUsoDeItens.AtivarParaItem(EscolhiEmQuemUsar);
        GameController.g.HudM.Painel.AtivarNovaMens(string.Format(
            BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),
            MbItens.NomeEmLinguas(ID)
            ), 26);
    }

    void VerificaTemMaisParaUsar()
    {
        GameController g = GameController.g;
        if (g.Manager.Dados.TemItem(ID) <= 0)
            g.HudM.P_EscolheUsoDeItens.VoltarDosItens();
    }

    protected virtual void EscolhiEmQuemUsar(int indice)
    {
        throw new System.NotImplementedException();
    }

    protected void EscolhiEmQuemUsar(
        int indice,
        bool vaiUsar,
        bool tipoCerto, 
        int valor = 0,
        int corrente = 0,
        int maximo= 0,
        nomeTipos recuperaDoTipo = nomeTipos.nulo)
    {
        CharacterManager manager = GameController.g.Manager;
        CriatureBase C = manager.Dados.CriaturesAtivos[indice];

        if (vaiUsar && tipoCerto)
        {
            if(Consumivel)
                RetirarUmItem(manager, this, 1, FluxoDeRetorno.menuHeroi);
            
            AcaoDoItemConsumivel(C);
            ItemQuantitativo.AplicacaoDoItemComMenu(manager, C, valor,VerificaTemMaisParaUsar);

        }
        else if (!tipoCerto)
        {
            MensDeUsoDeItem.MensNaoTemOTipo(recuperaDoTipo.ToString());
        }

        else if (corrente <= 0)
        {
            MensDeUsoDeItem.MensDeMorto(C.NomeEmLinguas);
        }
        else if (corrente >= maximo)
        {
            MensDeUsoDeItem.MensDeNaoPrecisaDesseItem(C.NomeEmLinguas);
        }
    }

    public override bool AtualizaUsoDeMenu()
    {
        return false;
    }

    protected void IniciaUsoDesseItem(GameObject dono,bool podeUsar,bool temTipo = true,nomeTipos nomeDoTipo = nomeTipos.nulo)
    {
        Manager = GameController.g.Manager;
        CriatureAlvoDoItem = Manager.CriatureAtivo;
        if (podeUsar && temTipo && RetirarUmItem(Manager, this, 1))
        {
            GameController.g.HudM.ModoCriature(false);
            InicializacaoComum(dono, Manager.CriatureAtivo.transform);
            Estado = EstadoDeUsoDeItem.animandoBraco;
        }
        else
        {
            Estado = EstadoDeUsoDeItem.finalizaUsaItem;
            if (!temTipo)
            {
                GameController.g.HudM.Painel.AtivarNovaMens(string.Format(
                   BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[3], nomeDoTipo), 30, 5);
            }
            else if (!podeUsar)
            {
                GameController.g.HudM.Painel.AtivarNovaMens(string.Format(
                BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.mensLuta)[2],
                    CriatureAlvoDoItem.MeuCriatureBase.NomeEmLinguas), 30, 5);
            }
        }
    }

    protected bool AtualizaUsoDesseItem(DoJogo particula)
    {
        switch (Estado)
        {
            case EstadoDeUsoDeItem.animandoBraco:
                if (!AnimaB.AnimaTroca(true))
                {
                    Estado = EstadoDeUsoDeItem.aplicandoItem;
                    Manager.Mov.Animador.ResetaTroca();
                    AuxiliarDeInstancia.InstancieEDestrua(particula, CriatureAlvoDoItem.transform.position, 1);
                    AcaoDoItemConsumivel(CriatureAlvoDoItem.MeuCriatureBase);
                    GameController.g.HudM.AtualizeImagemDeAtivos();
                    GameController.g.HudM.AtualizaDadosDaHudVida(false);
                }
            break;
            case EstadoDeUsoDeItem.aplicandoItem:
                TempoDecorrido += Time.deltaTime;
                if (TempoDecorrido > TEMPO_DE_ANIMA_CURA_1)
                {
                    
                    //GameController.g.HudM.AtualizaHudHeroi(CriatureAlvoDoItem.MeuCriatureBase);
                    Estado = EstadoDeUsoDeItem.finalizaUsaItem;
                    return false;
                }
            break;
            case EstadoDeUsoDeItem.finalizaUsaItem:
                return false;
            //break;
            case EstadoDeUsoDeItem.nulo:
                Debug.Log("alcançou estado nulo para " + ID.ToString());
            break;
        }
        return true;
    }

    public virtual void AcaoDoItemConsumivel(CriatureBase C)
    {
        throw new System.NotImplementedException();
    }
}
