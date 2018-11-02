using UnityEngine;
using System.Collections.Generic;

public class MbEscorpion
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
        #region GolpesAprendidosComPergaminhos
        //Golpes aprendidos com pergaminhos        
        new GolpePersonagem()
            {
                Nome = nomesGolpes.multiplicar,
                NivelDoGolpe = -1,
                Colisor = new colisor(),
                TaxaDeUso = 1.5f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/Bone/Bone_003/Bone_004/Bone_005/Bone_006/rabo"),
                Nome = nomesGolpes.olharEnfraquecedor,
                TaxaDeUso = 1,
                AcimaDoChao = -0.75f
            },
        #endregion GolpesAprendidosComPergaminhos
        /*
            fim dos golpes aprendidos com eprgaminhos
        */
        new GolpePersonagem()
            {
                NivelDoGolpe = 8,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/Bone/Bone_003/Bone_004/Bone_005/Bone_006/rabo"),
                Nome = nomesGolpes.olharMal,
                TaxaDeUso = 1,
                AcimaDoChao = -0.75f
            },new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/Bone/Bone_003/Bone_004/Bone_005/Bone_006/rabo"),
                Nome = nomesGolpes.agulhaVenenosa,
                TaxaDeUso = 1,
                AcimaDoChao = -0.75f           
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.chicoteDeCalda,
                NivelDoGolpe = 1,
                Colisor = new colisor("Arma__o/Bone/Bone_003/Bone_004/Bone_005/Bone_006/rabo",2),
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.ondaVenenosa,
                NivelDoGolpe = 2,
                Colisor = new colisor("Arma__o/Bone/Bone_003/Bone_004/Bone_005/Bone_006/rabo"),
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.chuvaVenenosa,
                NivelDoGolpe = 7,
                Colisor = new colisor("",new Vector3(0,0.316f,0.437f),Vector3.zero),
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            CriatureBase X = new CriatureBase()
            {
                NomeID = nomesCriatures.Escorpion,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 8,
                distanciaCameraLuta = 3.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Veneno },
                    distanciaFundamentadora = 0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.21f,},
                    PE = { Taxa = 0.17f},
                    Ataque = { Taxa = 0.2f},
                    Defesa = { Taxa = 0.23f},
                    Poder = { Taxa = 0.19f}
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
                    velocidadeCorrendo = 10,
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

            return X;
        }
    }
}
