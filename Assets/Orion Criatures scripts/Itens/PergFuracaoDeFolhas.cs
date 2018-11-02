using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergFuracaoDeFolhas : ItemDeAprenderGolpe
{
    public PergFuracaoDeFolhas(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergFuracaoDeFolhas)
    {
        valor = 144
    })
    {
        Estoque = estoque;
        TextoDaMensagemInicial = new string[2]
            {
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),MbItens.NomeEmLinguas(nomeIDitem.pergFuracaoDeFolhas)),
                BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpe),
            };
        Particula = DoJogo.particulaDaDefesaPergaminhoFora;

        golpeDoPergaminho = new nomesGolpes[1]
        {
            nomesGolpes.furacaoDeFolhas,
        };
    }
}

