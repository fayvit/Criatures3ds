using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemAntiStatusBase : ItemConsumivelBase
{
    protected TipoStatus qualStatusRemover = TipoStatus.nulo;
    public ItemAntiStatusBase(ContainerDeCaracteristicasDeItem C) : base(C) { }

    bool VerificaPodeUsarStatus(CriatureBase C)
    {
        Atributos A = C.CaracCriature.meusAtributos;
        int temStatus = StatusTemporarioBase.ContemStatus(qualStatusRemover, C);
        bool vivo = A.PV.Corrente > 0;

        return vivo && temStatus > -1;
    }
    public override void IniciaUsoComCriature(GameObject dono)
    {
        
        IniciaUsoDesseItem(dono, VerificaPodeUsarStatus(GameController.g.Manager.CriatureAtivo.MeuCriatureBase));
    }

    public override bool AtualizaUsoComCriature()
    {
        return AtualizaUsoDesseItem(DoJogo.animaAntiStatus);
    }

    public override void IniciaUsoDeHeroi(GameObject dono)
    {
        IniciaUsoDesseItem(dono, VerificaPodeUsarStatus(GameController.g.Manager.CriatureAtivo.MeuCriatureBase));
    }

    public override bool AtualizaUsoDeHeroi()
    {
        return AtualizaUsoDesseItem(DoJogo.animaAntiStatus);
    }

    protected override void EscolhiEmQuemUsar(int indice)
    {
        CriatureBase C = GameController.g.Manager.Dados.CriaturesAtivos[indice];
        Atributos A = C.CaracCriature.meusAtributos;

        int temStatus = StatusTemporarioBase.ContemStatus(qualStatusRemover, C);
        bool vivo = A.PV.Corrente > 0;

        if (temStatus > -1 || !vivo)
        {
            EscolhiEmQuemUsar(indice,
                vivo, true);
        }
        else {
            MensDeUsoDeItem.MensDeNaoPrecisaDesseItem(C.NomeEmLinguas);
        }
    }


    public override void AcaoDoItemConsumivel(CriatureBase C)
    {
        List<CriatureBase> meusC = GameController.g.Manager.Dados.CriaturesAtivos;
        StatusTemporarioBase[] meusStatus = GameController.g.ContStatus.StatusDoHeroi.ToArray();
        StatusTemporarioBase sTb = null;

        for (int i = 0; i < meusStatus.Length; i++)
        {
            Debug.Log(meusC.IndexOf(meusStatus[i].OAfetado) +" :"+ meusC.IndexOf(C) +" : "+ meusStatus[i].Dados.Tipo +" : "+ qualStatusRemover);
            if (meusC.IndexOf(meusStatus[i].OAfetado) == meusC.IndexOf(C) && meusStatus[i].Dados.Tipo == qualStatusRemover)
                sTb = meusStatus[i];
        }

        if (sTb != null)
            sTb.RetiraComponenteStatus();
        else
            Debug.Log("Status foi alcançado como nulo");

    }
}
