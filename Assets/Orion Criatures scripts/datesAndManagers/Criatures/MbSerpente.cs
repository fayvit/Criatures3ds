using UnityEngine;
using System.Collections.Generic;

public class MbSerpente
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
        #region comPergaminhos
        new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                ModPersonagem = 0,
                Colisor = new colisor("esqueleto/centroReverso/r1/r2/r3/rabo"),
                Nome = nomesGolpes.olharMal,
                AcimaDoChao = 0.45f,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.75f
            },
        #endregion comPErgaminhos
        new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("esqueleto/centroReverso/r1/r2/r3/rabo"),
                Nome = nomesGolpes.laminaDeFolha,
                AcimaDoChao = 0.45f,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.75f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.chicoteDeCalda,
                NivelDoGolpe = 1,
                Colisor = new colisor("esqueleto/centroReverso/r1/r2/r3/rabo",
                                    new Vector3(0,0f,0),
                                    new Vector3(-0.093f,0.135f,-0.37f)),
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.furacaoDeFolhas,
                NivelDoGolpe = 2,
                Colisor = new colisor("esqueleto/centroReverso/r1/r2/r3/rabo"),
                DistanciaEmissora = 2.75f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f,
                TempoDeInstancia = 0.1f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.tempestadeDeFolhas,
                NivelDoGolpe = 7,
                Colisor = new colisor("esqueleto/centroReverso",
                                                            new Vector3(0,0f,0),
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
                NomeID = nomesCriatures.Serpente,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[2] { nomeTipos.Planta , nomeTipos.Normal },
                    distanciaFundamentadora = 0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.19f,},
                    PE = { Taxa = 0.21f},
                    Ataque = { Taxa = 0.26f},
                    Defesa = { Taxa = 0.17f},
                    Poder = { Taxa = 0.17f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Normal)
                },
                GerenteDeGolpes = new GerenciadorDeGolpes()
                {
                    listaDeGolpes = listaDosGolpes
                },
                Mov = new CaracteristicasDeMovimentacao()
                {
                    velocidadeAndando = 5.5f,
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
