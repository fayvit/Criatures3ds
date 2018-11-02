using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergOlharEnfraquecedor : ItemDeAprenderGolpe
{

    public PergOlharEnfraquecedor(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergOlharEnfraquecedor)
    {
        valor = 330
    })
    {
        Estoque = estoque;
        TextoDaMensagemInicial = new string[2]
            {
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),MbItens.NomeEmLinguas(nomeIDitem.pergOlharEnfraquecedor)),
                BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpe),
            };
        Particula = DoJogo.particulaDaDefesaPergaminhoFora;

        golpeDoPergaminho = new nomesGolpes[1]
        {
            nomesGolpes.olharEnfraquecedor,
        };
    }
}
