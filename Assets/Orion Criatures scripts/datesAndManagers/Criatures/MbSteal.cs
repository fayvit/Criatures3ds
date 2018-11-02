using UnityEngine;
using System.Collections.Generic;

public class MbSteal
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {

            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("metarig/hips/chest"),
                Nome = nomesGolpes.eletricidade,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.25f,
                AcimaDoChao = 0.1f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.cabecada,
                NivelDoGolpe = 1,
                Colisor = new colisor("metarig/hips/chest/neck/head",
                                          new Vector3(0.97f,0.12f,0.65f),
                                          new Vector3(-0.306f,-0.156f,-0.129f)),
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.eletricidadeConcentrada,
                NivelDoGolpe = 2,
                Colisor = new colisor("metarig/hips/chest"),
                DistanciaEmissora = 1f,
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.tempestadeEletrica,
                NivelDoGolpe = 7,
                Colisor = new colisor("metarig/hips"),
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Steal,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Eletrico },
                    distanciaFundamentadora = 0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.195f,},
                    PE = { Taxa = 0.205f},
                    Ataque = { Taxa = 0.18f},
                    Defesa = { Taxa = 0.21f},
                    Poder = { Taxa = 0.21f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Eletrico)
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
