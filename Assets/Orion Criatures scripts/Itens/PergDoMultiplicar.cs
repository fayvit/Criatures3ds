using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergDoMultiplicar : ItemDeAprenderGolpe
{

    public PergDoMultiplicar(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergMultiplicar)
    {
        valor = 300
    })
    {
        Estoque = estoque;
        TextoDaMensagemInicial = new string[2]
            {
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),MbItens.NomeEmLinguas(nomeIDitem.pergMultiplicar)),
                BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpe),
            };
        Particula = DoJogo.particulaDaDefesaPergaminhoFora;

        golpeDoPergaminho = new nomesGolpes[1]
        {
            nomesGolpes.multiplicar,
        };
    }
}
