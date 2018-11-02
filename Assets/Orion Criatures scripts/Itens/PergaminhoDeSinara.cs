using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergaminhoDeSinara : ItemModificadorDeAtributoConsumivel
{

    public PergaminhoDeSinara(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergSinara)
    {
        valor = 1500
    }
        )
    {
        Estoque = estoque;
        TextoDaMensagemInicial = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.usarPergaminhoDeSinara).ToArray();
        Particula = DoJogo.particulaDoPVpergaminho;
    }

    protected override void AplicaEfeito(CriatureBase C)
    {
        AtributoConsumivel A = C.CaracCriature.meusAtributos.PV;

        A = ContaDeSubida(A);

        C.CaracCriature.meusAtributos.PV = A;

        EntraNoModoFinalizacao(C);
    }
}
