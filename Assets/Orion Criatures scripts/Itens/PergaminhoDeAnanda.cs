using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergaminhoDeAnanda : ItemModificadorDeAtributoIntrinsico
{

    public PergaminhoDeAnanda(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergaminhoDeAnanda)
    {
        valor = 1500
    }
        )
    {
        Estoque = estoque;
        TextoDaMensagemInicial = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.usarPergaminhoDeAnanda).ToArray();
        Particula = DoJogo.particulaDoPoderPergaminhoFora;
    }

    protected override void AplicaEfeito(CriatureBase C)
    {
        AtributoInstrinseco A = C.CaracCriature.meusAtributos.Poder;

        A = ContaDeSubida(A);

        C.CaracCriature.meusAtributos.Poder = A;

        EntraNoModoFinalizacao(C);
    }

}
