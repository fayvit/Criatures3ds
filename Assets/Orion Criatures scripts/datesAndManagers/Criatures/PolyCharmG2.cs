using UnityEngine;
using System.Collections.Generic;

public class PolyCharmG2
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
        #region comPergainhos
        new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o_001/coluna1/coluna2/coluna3/pescoco/cabeca"),
                Nome = nomesGolpes.olharEnfraquecedor,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
        new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o_001/coluna1/coluna2/coluna3/pescoco/cabeca"),
                Nome = nomesGolpes.olharMal,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
        #endregion comPergaminhos

        new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o_001/coluna1/coluna2/coluna3/pescoco/cabeca"),
                Nome = nomesGolpes.bolaDeFogo,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.garra,
                NivelDoGolpe = 1,
                Colisor = new colisor("Arma__o_001/coluna1/pernaD/peD/",
                                   new Vector3(0.18f,0,0),
                                   new Vector3(-0.867f,-0.585f,-0.26f)),
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.rajadaDeFogo,
                NivelDoGolpe = 2,
                Colisor = new colisor("Arma__o_001/coluna1/coluna2/coluna3/pescoco/cabeca"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.tosteAtaque,
                NivelDoGolpe = 7,
                Colisor = new colisor("Arma__o_001/coluna1",
                                                   new Vector3(0f,0,0),
                                                   new Vector3(0,0,0)),
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.PolyCharm,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Fogo },
                    distanciaFundamentadora = 0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.21f,},
                    PE = { Taxa = 0.2f},
                    Ataque = { Taxa = 0.21f},
                    Defesa = { Taxa = 0.18f},
                    Poder = { Taxa = 0.2f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Fogo)
                },
                GerenteDeGolpes = new GerenciadorDeGolpes()
                {
                    listaDeGolpes = listaDosGolpes
                },
                Mov = new CaracteristicasDeMovimentacao()
                {
                    velocidadeAndando = 5,
                    caracPulo = new CaracteristicasDePulo()
                    {
                        alturaDoPulo = 2f,
                        tempoMaxPulo = 1,
                        velocidadeSubindo = 5,
                        velocidadeDescendo = 20,
                        velocidadeDuranteOPulo = 4,
                        amortecimentoNaTransicaoDePulo = 1.2f
                    }
                }
            };
        }
    }
}

