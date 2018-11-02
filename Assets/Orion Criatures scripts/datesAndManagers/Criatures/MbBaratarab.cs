using UnityEngine;
using System.Collections.Generic;

public class MbBaratarab
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
        // Golpes aprendidos apenas com pergaminhos
        new GolpePersonagem()
            {
                Nome = nomesGolpes.ventania,
                NivelDoGolpe = -1,
                Colisor = new colisor("Arma__o/Bone_001/Bone/Bone_002"),
                TaxaDeUso = 0.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.ventosCortantes,
                NivelDoGolpe = -1,
                Colisor = new colisor("Arma__o/Bone_001/Bone/Bone_002"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 0.5f
            },
            /*
            
            fim dos golpes aprendidos apenas com pergaminhos
            
            */
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/Bone_001/Bone/Bone_002"),
                Nome = nomesGolpes.gosmaDeInseto,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.cabecada,
                NivelDoGolpe = 1,
                Colisor = new colisor("Arma__o/Bone_001/Bone/Bone_002",
                                          new Vector3(0,0,0),
                                          new Vector3(-0.283f,0.014f,-0.245f)),
            TaxaDeUso = 0.5f
            },new GolpePersonagem()
            {
                NivelDoGolpe = 2,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/Bone_001/Bone/Bone_002"),
                Nome = nomesGolpes.gosmaAcida,
                TaxaDeUso = 1.25f,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.multiplicar,
                NivelDoGolpe = 7,
                Colisor = new colisor(),
                TaxaDeUso = 1.5f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 8,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/Bone_001/Bone/Bone_002"),
                Nome = nomesGolpes.olharMal,
                TaxaDeUso = 1.25f,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.sobreVoo,
                NivelDoGolpe = 9,
                Colisor = new colisor("Arma__o/Bone_001/Bone/",
                                                      new Vector3(0,0,0),
                                                      new Vector3(-0.163f,0.017f,0.139f)),
                TaxaDeUso = 0.5f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Baratarab,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Inseto },
                    distanciaFundamentadora = 0.05f,
                    meusAtributos = {
                    PV = { Taxa = 0.21f},
                    PE = { Taxa = 0.21f},
                    Ataque = { Taxa = 0.24f},
                    Defesa = { Taxa = 0.17f},
                    Poder = { Taxa = 0.17f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Inseto)
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
                        alturaDoPulo = 1.8f,
                        tempoMaxPulo = 1,
                        velocidadeSubindo = 5,
                        velocidadeDescendo = 20,
                        velocidadeDuranteOPulo = 3.8f,
                        amortecimentoNaTransicaoDePulo = 1.2f
                    }
                }
            };
        }
    }
}