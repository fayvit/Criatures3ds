using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemDeEnergiaBase : ItemConsumivelBase
{
    protected nomeTipos recuperaDoTipo = nomeTipos.nulo;
    protected int valorDeRecuperacao = 40;

    public ItemDeEnergiaBase(ContainerDeCaracteristicasDeItem C) : base(C) { }

    public override void IniciaUsoComCriature(GameObject dono)
    {
        CriatureBase C = GameController.g.Manager.CriatureAtivo.MeuCriatureBase;
        IniciaUsoDesseItem(dono, 
            ItemQuantitativo.UsaItemDeEnergia(C), C.CaracCriature.TemOTipo(recuperaDoTipo),recuperaDoTipo);
    }

    public override bool AtualizaUsoComCriature()
    {
        return AtualizaUsoDesseItem(DoJogo.animaPE);
    }

    public override void IniciaUsoDeHeroi(GameObject dono)
    {        
        CriatureBase C = GameController.g.Manager.CriatureAtivo.MeuCriatureBase;
        IniciaUsoDesseItem(dono,
            ItemQuantitativo.UsaItemDeEnergia(C), C.CaracCriature.TemOTipo(recuperaDoTipo),recuperaDoTipo);
    }

    public override bool AtualizaUsoDeHeroi()
    {
        return AtualizaUsoDesseItem(DoJogo.animaPE);
    }

    protected override void EscolhiEmQuemUsar(int indice)
    {
        CriatureBase C = GameController.g.Manager.Dados.CriaturesAtivos[indice];
        Atributos A = C.CaracCriature.meusAtributos;

        EscolhiEmQuemUsar(indice,
            ItemQuantitativo.UsaItemDeEnergia(C),
            C.CaracCriature.TemOTipo(recuperaDoTipo),
            valorDeRecuperacao, A.PE.Corrente,
            A.PE.Maximo,
            recuperaDoTipo
            );
    }

    public override void AcaoDoItemConsumivel(CriatureBase C)
    {
        ItemQuantitativo.RecuperaPE(C.CaracCriature.meusAtributos, valorDeRecuperacao);
    }
}
