using UnityEngine;
using System.Collections.Generic;

public class MbCracler
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
        #region comPergaminhos]

         new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                Nome = nomesGolpes.olharEnfraquecedor,
                Colisor = new colisor("container/Arma__o/Bone/Bone_R/Bone_R_001"),
                DistanciaEmissora = 1.5f,
                AcimaDoChao = 0.25f
            },
        new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                Nome = nomesGolpes.sabreDeBastao,
                Colisor = new colisor("container/Arma__o/Bone/Bone_R/Bone_R_001"),
                DistanciaEmissora = 1.5f,
                AcimaDoChao = 0.25f
            },
        #endregion comPErgaminhos
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("container/Arma__o/Bone/Bone_R/Bone_R_001"),
                Nome = nomesGolpes.agulhaVenenosa,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f,
                TempoDeInstancia = 0.25f,
                AcimaDoChao = -0.1f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.bastao,
                NivelDoGolpe = 1,
                Colisor = new colisor("container/Arma__o/Bone/Bone_001",
		                                            new Vector3(0,0,0),
		                                            new Vector3(0,0,0)),
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.ondaVenenosa,
                NivelDoGolpe = 2,
                Colisor = new colisor("container/Arma__o/Bone/Bone_R/Bone_R_001"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.chuvaVenenosa,
                NivelDoGolpe = 7,
                Colisor = new colisor("",new Vector3(0,0.62f,0.21f),Vector3.zero),
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            CriatureBase X = new CriatureBase()
            {
                NomeID = nomesCriatures.Cracler,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Veneno },
                    distanciaFundamentadora = 0.1f,
                    meusAtributos = {
                    PV = { Taxa = 0.22f,},
                    PE = { Taxa = 0.17f},
                    Ataque = { Taxa = 0.22f},
                    Defesa = { Taxa = 0.19f},
                    Poder = { Taxa = 0.2f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Veneno)
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
            X.CaracCriature.contraTipos[9].Mod = 0.1f;//Veneno
            X.CaracCriature.contraTipos[7].Mod = 2f;//Eletrico

            return X;
        }
    }
}
