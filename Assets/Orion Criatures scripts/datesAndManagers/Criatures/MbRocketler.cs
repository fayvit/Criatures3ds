using UnityEngine;
using System.Collections.Generic;

public class MbRocketler
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {

            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Esqueleto/hips/spine/chest/upper_arm_R/forearm_R/hand_R",
                                    new Vector3(0,0,0),
                                    new Vector3(-0.621f,-0,0.244f)),
                Nome = nomesGolpes.cascalho,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f,
                TempoDeInstancia = 0.05f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.cabecada,
                NivelDoGolpe = 1,
                Colisor = new colisor("Esqueleto/hips/spine/chest/neck/head",
                                                  new Vector3(-2.28f,0.96f,-1.41f),
                                                  new Vector3(-0.296f,0.401f,0.508f)),
                TaxaDeUso = 1f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.pedregulho,
                NivelDoGolpe = 2,
                Colisor = new colisor("Esqueleto/hips/spine/chest/upper_arm_R/forearm_R/hand_R"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.avalanche,
                NivelDoGolpe = 7,
                Colisor = new colisor("Esqueleto/hips/spine/chest/neck/head",new Vector3(-0.72f,0,0.52f),Vector3.zero),
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.olharEnfraquecedor,
                NivelDoGolpe = 8,
                Colisor = new colisor("Esqueleto/hips/spine/chest/upper_arm_R/forearm_R/hand_R"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.olharMal,
                NivelDoGolpe = 16,
                Colisor = new colisor("Esqueleto/hips/spine/chest/upper_arm_R/forearm_R/hand_R"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Rocketler,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Pedra },
                    distanciaFundamentadora = 0.1f,
                    meusAtributos = {
                    PV = { Taxa = 0.18f,},
                    PE = { Taxa = 0.21f},
                    Ataque = { Taxa = 0.21f},
                    Defesa = { Taxa = 0.22f},
                    Poder = { Taxa = 0.18f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Pedra)
                },
                GerenteDeGolpes = new GerenciadorDeGolpes()
                {
                    listaDeGolpes = listaDosGolpes
                },
                Mov = new CaracteristicasDeMovimentacao()
                {
                    velocidadeAndando = 4.9f,
                    caracPulo = new CaracteristicasDePulo()
                    {
                        alturaDoPulo = 2.8f,
                        tempoMaxPulo = 1,
                        velocidadeSubindo = 9.5f,
                        velocidadeDescendo = 15,
                        velocidadeDuranteOPulo = 7,
                        amortecimentoNaTransicaoDePulo = 1.2f
                    }
                }
            };
        }
    }
}

