using UnityEngine;
using System.Collections;

[System.Serializable]
public class PergDeSabre : ItemDeAprenderGolpe
{
    protected string nomeBasico = "Sabre";

    public PergDeSabre(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergSabre)
    {
        valor = 0,
        consumivel = false
    })
    {
        Estoque = estoque;
        TextoDaMensagemInicial = new string[2]
            {
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.emQuem),MbItens.NomeEmLinguas(nomeIDitem.pergSabre)),
                BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpe),
            };
        Particula = DoJogo.particulaDoPoderPergaminhoFora;
        nomeBasico = "Sabre";

        golpeDoPergaminho = new nomesGolpes[4]
        {
            nomesGolpes.sabreDeAsa,
            nomesGolpes.sabreDeBastao,
            nomesGolpes.sabreDeEspada,
            nomesGolpes.sabreDeNadadeira
        };
    }

    protected override string NomeBasico
    {
        get { return nomeBasico; }
    }

    protected override nomesGolpes GolpePorAprender(CriatureBase C)
    {
        nomesGolpes golpePorAprender = nomesGolpes.nulo;
        bool foi = false;
        for (int i = 0; i < golpeDoPergaminho.Length; i++)
        {
            if (!foi)
                golpePorAprender = C.GerenteDeGolpes.ProcuraGolpeNaLista(C.NomeID, golpeDoPergaminho[i]).Nome;

            if (golpePorAprender != nomesGolpes.nulo)
                foi = true;
        }

        return golpePorAprender;
    }

    

    

    /*
    protected override void EscolhiEmQuemUsar(int indice)
    {
        CharacterManager manager = GameController.g.Manager;
        CriatureBase C = manager.Dados.CriaturesAtivos[indice];
        //if (vaiUsar && tipoCerto)
        {
            RetirarUmItem(manager, this, 1, FluxoDeRetorno.menuHeroi);

            AcaoDoItemConsumivel(C);
            //ItemQuantitativo.AplicacaoDoItemComMenu(manager, C, valor, VerificaTemMaisParaUsar);

        }
    }*/
}
