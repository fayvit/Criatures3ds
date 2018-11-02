using UnityEngine;
using System.Collections.Generic;

public class MbOnarac
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
            new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                Nome = nomesGolpes.sabreDeEspada,
                Colisor = new colisor("esqueleto/corpo/"),
                DistanciaEmissora = 2,
                AcimaDoChao = 0.25f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("esqueleto/corpo/anteBracoD/bracoD/maoD",
                                         new Vector3(0,0,0),
                                         new Vector3(-0.335f,-0.202f,0.147f)),
                Nome = nomesGolpes.espada,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.chute,
                NivelDoGolpe = 1,
                Colisor = new colisor("esqueleto/corpo/coxaD/pernaD/calcanharD",
                                          new Vector3(0,0,0),
                                          new Vector3(-0.25f,0.034f,-0.339f)),
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.sobreSalto,
                NivelDoGolpe = 2,
                Colisor = new colisor("esqueleto/corpo/",
                                              new Vector3(0,0,1.4f),
                                              new Vector3(0f,0.113f,-0.292f)),
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.anelDoOlhar,
                NivelDoGolpe = 7,
                Colisor = new colisor("esqueleto/corpo/"),
                DistanciaEmissora = 0.5f,
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Florest,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Normal },
                    distanciaFundamentadora = 0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.195f,},
                    PE = { Taxa = 0.205f},
                    Ataque = { Taxa = 0.21f},
                    Defesa = { Taxa = 0.21f},
                    Poder = { Taxa = 0.18f}
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
