using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PergaminhoDeLaurense : ItemModificadorDeAtributoIntrinsico
{

    public PergaminhoDeLaurense(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergaminhoDeLaurense)
    {
        valor = 1500
    }
        )
    {
        Estoque = estoque;
        TextoDaMensagemInicial = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.usarPergaminhoDeLaurense).ToArray();
    }

    /*
    public void InicioComum()
    {

        GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.usarPergaminhoDeLaurense), 25);
        GameController.g.HudM.Menu_Basico.IniciarHud(OpcaoEscolhida, NomesDosCriaturesAtivos());
        ActionManager.ModificarAcao(GameController.g.transform,()=> {
            OpcaoEscolhida(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
        });
        Estado = EstadoDeUsoDeItem.selecaoDeItem;

    }*/

    

    protected override void AplicaEfeito(CriatureBase C)
    {
        AtributoInstrinseco A = C.CaracCriature.meusAtributos.Ataque;

        A = ContaDeSubida(A);

        C.CaracCriature.meusAtributos.Ataque = A;

        EntraNoModoFinalizacao(C);
    }
}
