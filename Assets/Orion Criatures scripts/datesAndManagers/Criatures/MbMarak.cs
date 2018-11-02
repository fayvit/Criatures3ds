using UnityEngine;
using System.Collections.Generic;

public class MbMarak
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {

            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/corpo3/corpo2/corpo1/pescoco/cabeca/",
                                          new Vector3(0,0,3f),
                                          new Vector3(-0.6522f,-0.0274f,-0.659f)),
                Nome = nomesGolpes.chifre,
                TaxaDeUso = 1,
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.garra,
                NivelDoGolpe = 1,
                Colisor = new colisor("Arma__o/corpo3_R/",
                                         new Vector3(0,0,0.3f),
                                         new Vector3(-0.331f,-0.344f,-0.192f)),
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.sobreSalto,
                NivelDoGolpe = 2,
                Colisor = new colisor("Arma__o/corpo3/",
                                         new Vector3(0,-0.46f,1.4f),
                                         new Vector3(-0.365f,0.113f,-0.325f)),
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 7,
                Colisor = new colisor("Arma__o/corpo3/corpo2/corpo1/pescoco/cabeca/"),
                Nome = nomesGolpes.anelDoOlhar,
                TaxaDeUso = 0.5f,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 7,
                Colisor = new colisor("Arma__o/corpo3/corpo2/corpo1/pescoco/cabeca/"),
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
                NomeID = nomesCriatures.Marak,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Normal },
                    distanciaFundamentadora = 0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.21f,},
                    PE = { Taxa = 0.2f},
                    Ataque = { Taxa = 0.21f},
                    Defesa = { Taxa = 0.18f},
                    Poder = { Taxa = 0.2f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Normal)
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

