using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergVentosCortantes : ItemDeAprenderGolpe
{
    public PergVentosCortantes(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergVentosCortantes)
    {
        valor = 144
    })
    {
        Estoque = estoque;
        TextoDaMensagemInicial = new string[2]
            {
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),MbItens.NomeEmLinguas(nomeIDitem.pergVentosCortantes)),
                BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpe),
            };
        Particula = DoJogo.particulaDoPoderPergaminhoFora;

        golpeDoPergaminho = new nomesGolpes[1]
        {
            nomesGolpes.ventosCortantes,
        };
    }
}


