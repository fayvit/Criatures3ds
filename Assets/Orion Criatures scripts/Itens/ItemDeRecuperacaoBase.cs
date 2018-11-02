using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemDeRecuperacaoBase : ItemConsumivelBase
{
    protected int valorDeRecuperacao = 40;
    public ItemDeRecuperacaoBase(ContainerDeCaracteristicasDeItem C) : base(C){ }

    public override void IniciaUsoComCriature(GameObject dono)
    {
        
        IniciaUsoDesseItem(dono, ItemQuantitativo.UsaItemDeRecuperacao(GameController.g.Manager.CriatureAtivo.MeuCriatureBase));
    }

    public override bool AtualizaUsoComCriature()
    {
        return AtualizaUsoDesseItem(DoJogo.acaoDeCura1);
    }

    public override void IniciaUsoDeHeroi(GameObject dono)
    {
        IniciaUsoDesseItem(dono, ItemQuantitativo.UsaItemDeRecuperacao(GameController.g.Manager.CriatureAtivo.MeuCriatureBase));
    }

    public override bool AtualizaUsoDeHeroi()
    {
        return AtualizaUsoDesseItem(DoJogo.acaoDeCura1);
    }

    protected override void EscolhiEmQuemUsar(int indice)
    {
        CriatureBase C = GameController.g.Manager.Dados.CriaturesAtivos[indice];
        Atributos A = C.CaracCriature.meusAtributos;

        EscolhiEmQuemUsar(indice,
            ItemQuantitativo.UsaItemDeRecuperacao(C),true, 
            valorDeRecuperacao, A.PV.Corrente,
            A.PV.Maximo,
            nomeTipos.nulo);
    }

    public override void AcaoDoItemConsumivel(CriatureBase C)
    {
        ItemQuantitativo.RecuperaPV(C.CaracCriature.meusAtributos, valorDeRecuperacao);
        if (!GameController.g.estaEmLuta)
            GameController.g.Salvador.SalvarAgora();
    }
}
