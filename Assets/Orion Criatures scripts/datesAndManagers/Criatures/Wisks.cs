using UnityEngine;
using System.Collections.Generic;

public class Wisks
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("esqueleto/corpo_001/coluna/anteBraco_R/braco_R/mao_R",
                                        new Vector3(0,0,0),
                                        new Vector3(-0.26f,-0,0)),
                Nome = nomesGolpes.tapa,
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.cabecada,
                ModPersonagem = 1.5f,
                NivelDoGolpe = 1,
                Colisor = new colisor("esqueleto/corpo_001/coluna/pescoco/cabeca",
                                            new Vector3(0,0f,0),
                                            new Vector3(-0.644f,-0.495f,0.048f)),
                TaxaDeUso = 0.5f
            },new GolpePersonagem()
            {
                NivelDoGolpe = 2,
                ModPersonagem = 0,
                Colisor = new colisor("esqueleto/corpo_001/",
                                              new Vector3(0,0,1.4f),
                                              new Vector3(-0.365f,0.113f,-0.325f)),
                Nome = nomesGolpes.sobreSalto,
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 6,
                ModPersonagem = 0.2f,
                Colisor = new colisor("esqueleto"),
                Nome = nomesGolpes.dentada,
                TaxaDeUso = 0.5f,
                DistanciaEmissora = 0.5f
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
                NivelDoGolpe = 7,
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
                NomeID = nomesCriatures.Wisks,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Normal },
                    distanciaFundamentadora = 0.02f,
                    meusAtributos = {
                    PV = { Taxa = 0.19f,},
                    PE = { Taxa = 0.22f},
                    Ataque = { Taxa = 0.17f},
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
                },alturaCamera = 3,
                distanciaCamera = 7,
                alturaCameraLuta = 5,
                distanciaCameraLuta = 5
            };
        }
    }
}