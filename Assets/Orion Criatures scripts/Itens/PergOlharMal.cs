using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergOlharMal : ItemDeAprenderGolpe
{

    public PergOlharMal(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergOlharMal)
    {
        valor = 333
    })
    {
        Estoque = estoque;
        TextoDaMensagemInicial = new string[2]
            {
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),MbItens.NomeEmLinguas(nomeIDitem.pergOlharMal)),
                BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpe),
            };
        Particula = DoJogo.particulaDoAtaquePergaminhoFora;

        golpeDoPergaminho = new nomesGolpes[1]
        {
            nomesGolpes.olharMal,
        };
    }
}
