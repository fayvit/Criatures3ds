using UnityEngine;
using System.Collections.Generic;

public class MbBabaucu
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {

            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("esqueleto/corpo/preBracoD/bracoD1/bracoD2/bracoD3/bracoD4",
                                          new Vector3(0,0,0f),
                                          new Vector3(-0.2f,0.53f,-0.13f)),
                Nome = nomesGolpes.chicoteDeMao,
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.dentada,
                ModPersonagem = 1.5f,
                NivelDoGolpe = 1,
                Colisor = new colisor("esqueleto/corpo/",
                                                new Vector3(0,0,0.3f),
                                           new Vector3(-0.2f,0f,0.723f)),
                TaxaDeUso = 0.5f
            },new GolpePersonagem()
            {
                NivelDoGolpe = 2,
                ModPersonagem = 0.5f,
                Colisor = new colisor("esqueleto/corpo/",
                                              new Vector3(0,0,1.4f),
                                              new Vector3(0.181f,0f,0.075f)),
                Nome = nomesGolpes.sobreSalto,
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 7,
                Colisor = new colisor("esqueleto"),
                Nome = nomesGolpes.anelDoOlhar,
                TaxaDeUso = 0.5f,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 8,
                Colisor = new colisor("esqueleto"),
                Nome = nomesGolpes.olharMal,
                TaxaDeUso = 0.5f,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 16,
                Colisor = new colisor("esqueleto"),
                Nome = nomesGolpes.olharEnfraquecedor,
                TaxaDeUso = 0.5f,
                DistanciaEmissora = 0.5f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Babaucu,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Normal },
                    distanciaFundamentadora = 0.02f,
                    meusAtributos = {
                    PV = { Taxa = 0.19f,},
                    PE = { Taxa = 0.17f},
                    Ataque = { Taxa = 0.22f},
                    Defesa = { Taxa = 0.2f},
                    Poder = { Taxa = 0.22f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Normal)
                },
                GerenteDeGolpes = new GerenciadorDeGolpes()
                {
                    listaDeGolpes = listaDosGolpes
                },
                Mov = new CaracteristicasDeMovimentacao()
                {
                    velocidadeAndando = 4.5f,
                    caracPulo = new CaracteristicasDePulo()
                    {
                        alturaDoPulo = 3f,
                        tempoMaxPulo = 1,
                        velocidadeSubindo = 8,
                        velocidadeDescendo = 20,
                        velocidadeDuranteOPulo = 5,
                        amortecimentoNaTransicaoDePulo = 1.2f
                    }
                }
            };
        }
    }
}