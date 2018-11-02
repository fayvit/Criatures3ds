using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergGosmaAcida : ItemDeAprenderGolpe
{
    public PergGosmaAcida(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergGosmaAcida)
    {
        valor = 144
    })
    {
        Estoque = estoque;
        TextoDaMensagemInicial = new string[2]
            {
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),MbItens.NomeEmLinguas(nomeIDitem.pergGosmaAcida)),
                BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpe),
            };
        Particula = DoJogo.particulaDaDefesaPergaminhoFora;

        golpeDoPergaminho = new nomesGolpes[1]
        {
            nomesGolpes.gosmaAcida,
        };
    }
}



