using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemNaoUsavel : MbItens
{
    public ItemNaoUsavel(ContainerDeCaracteristicasDeItem C) : base(C){ }

    public override void IniciaUsoDeMenu(GameObject dono)
    {
        Estado = EstadoDeUsoDeItem.emEspera;
    }

    public override void IniciaUsoDeHeroi(GameObject dono)
    {
        IniciaUsoComCriature(dono);
    }
    public override void IniciaUsoComCriature(GameObject dono)
    {
        GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.mensLuta)[0], 30, 7);
    }

    public override bool AtualizaUsoComCriature()
    {
        return false;
    }

    public override bool AtualizaUsoDeHeroi()
    {
        return false;
    }
    public override bool AtualizaUsoDeMenu()
    {
        return false;
    }
}
