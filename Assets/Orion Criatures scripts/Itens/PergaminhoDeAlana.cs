using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergaminhoDeAlana : ItemModificadorDeAtributoConsumivel
{

    public PergaminhoDeAlana(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergAlana)
    {
        valor = 1500
    }
        )
    {
        Estoque = estoque;
        TextoDaMensagemInicial = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.usarPergaminhoDeAlana).ToArray();
        Particula = DoJogo.particulaDoPEpergaminho;
    }

    protected override void AplicaEfeito(CriatureBase C)
    {
        AtributoConsumivel A = C.CaracCriature.meusAtributos.PE;

        A = ContaDeSubida(A);

        C.CaracCriature.meusAtributos.PE = A;

        EntraNoModoFinalizacao(C);
    }
}
