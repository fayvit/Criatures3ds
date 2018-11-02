using UnityEngine;
using System.Collections.Generic;

public class MbDogMour
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {

            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Esqueleto/Bone/Bone_001/Bone_002/Bone_003/Bone_004/Bone_005"),
                Nome = nomesGolpes.bombaDeGas,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.garra,
                NivelDoGolpe = 1,
                Colisor = new colisor("Esqueleto/Bone/Bone_001/Bone_002/Bone_002_R/Bone_002_R_001/Bone_002_R_002",
                                         new Vector3(0.18f,0,0),
                                         new Vector3(-0.525f,-0.057f,-0.015f)),
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.rajadaDeGas,
                NivelDoGolpe = 2,
                Colisor = new colisor("Esqueleto/Bone/Bone_001/Bone_002/Bone_003/Bone_004/Bone_005"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.cortinaDeFumaca,
                NivelDoGolpe = 7,
                Colisor = new colisor(),
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.DogMour,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Gas },
                    distanciaFundamentadora = 0.1f,
                    meusAtributos = {
                    PV = { Taxa = 0.18f,},
                    PE = { Taxa = 0.22f},
                    Ataque = { Taxa = 0.17f},
                    Defesa = { Taxa = 0.18f},
                    Poder = { Taxa = 0.25f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Gas)
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

