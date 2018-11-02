using UnityEngine;
using System.Collections;

[System.Serializable]
public class MbPergaminhoDePerfeicao : ItemConsumivelBase
{

    public MbPergaminhoDePerfeicao(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergaminhoDePerfeicao)
    {
        valor = 400
    }
        )
    {
        Estoque = estoque;
    }

    public override void IniciaUsoComCriature(GameObject dono)
    {

        IniciaUsoDesseItem(dono, ItemQuantitativo.PrecisaDePerfeicao(GameController.g.Manager.CriatureAtivo.MeuCriatureBase));
    }

    public override bool AtualizaUsoComCriature()
    {
        return AtualizaUsoDesseItem(DoJogo.perfeicao);
    }

    public override void IniciaUsoDeHeroi(GameObject dono)
    {
        GameController.g.HudM.ModoCriature(false);
        IniciaUsoDesseItem(dono, ItemQuantitativo.PrecisaDePerfeicao(GameController.g.Manager.CriatureAtivo.MeuCriatureBase));
    }

    public override bool AtualizaUsoDeHeroi()
    {
        return AtualizaUsoDesseItem(DoJogo.perfeicao);
    }

    protected override void EscolhiEmQuemUsar(int indice)
    {
        CriatureBase C = GameController.g.Manager.Dados.CriaturesAtivos[indice];
        Atributos A = C.CaracCriature.meusAtributos;

        EscolhiEmQuemUsar(indice,
            ItemQuantitativo.PrecisaDePerfeicao(C),true, 
            -1, A.PV.Corrente,
            A.PV.Maximo,
            nomeTipos.nulo);
    }

    public override void AcaoDoItemConsumivel(CriatureBase C)
    {
        C.EstadoPerfeito();
        //ItemQuantitativo.RecuperaPV(CriatureAlvoDoItem.MeuCriatureBase.CaracCriature.meusAtributos, valorDeRecuperacao);
    }


}
