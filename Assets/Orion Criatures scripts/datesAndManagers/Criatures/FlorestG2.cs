using UnityEngine;
using System.Collections.Generic;

public class FlorestG2
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
        #region comPergaminhos
        new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/corpo"),
                Nome = nomesGolpes.olharEnfraquecedor,
                AcimaDoChao = 0.5f,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
        new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/corpo"),
                Nome = nomesGolpes.anelDoOlhar,
                AcimaDoChao = 0.5f,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
        #endregion comPergaminhos

        new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/corpo"),
                Nome = nomesGolpes.laminaDeFolha,
                AcimaDoChao = 0.5f,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.garra,
                NivelDoGolpe = 1,
                Colisor = new colisor("Arma__o/corpo/quadrilD/pernaD1/pernaD2/peD/",
                                    new Vector3(0,0,0.3f),
                                    new Vector3(-0.33f,0.48f,-0.38f)),
                TaxaDeUso = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.furacaoDeFolhas,
                NivelDoGolpe = 2,
                Colisor = new colisor("Arma__o/corpo"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.tempestadeDeFolhas,
                NivelDoGolpe = 7,
                Colisor = new colisor("Arma__o/corpo",
                                                   new Vector3(0,0,0.3f),
                                                   new Vector3(0,0,0f)),
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Florest,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Planta },
                    distanciaFundamentadora = 0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.195f,},
                    PE = { Taxa = 0.205f},
                    Ataque = { Taxa = 0.21f},
                    Defesa = { Taxa = 0.21f},
                    Poder = { Taxa = 0.18f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Planta)
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
