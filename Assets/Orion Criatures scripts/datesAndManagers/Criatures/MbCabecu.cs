using UnityEngine;
using System.Collections.Generic;

public class MbCabecu
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {

            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/base/corpo"),
                Nome = nomesGolpes.rajadaDeTerra,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.3f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.chute,
                NivelDoGolpe = 1,
                Colisor = new colisor("Arma__o/base/pernaD/peD1/peD2",
                                           new Vector3(0,0f,0),
                                           new Vector3(-0.156f,0.096f,-0.212f)),
                TaxaDeUso = 1f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.vingancaDaTerra,
                NivelDoGolpe = 2,
                Colisor = new colisor("Arma__o/base/corpo"),
                TaxaDeUso = 1.25f,
                DistanciaEmissora = 0.25f
                
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.cortinaDeTerra,
                NivelDoGolpe = 7,
                Colisor = new colisor(),
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.olharMal,
                NivelDoGolpe = 8,
                Colisor = new colisor("Arma__o/base/corpo"),
                TaxaDeUso = 1.25f

            },
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Cabecu,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Terra },
                    distanciaFundamentadora = 0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.21f,},
                    PE = { Taxa = 0.21f},
                    Ataque = { Taxa = 0.18f},
                    Defesa = { Taxa = 0.18f},
                    Poder = { Taxa = 0.22f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Terra)
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
                        alturaDoPulo = 2.2f,
                        tempoMaxPulo = 1,
                        velocidadeSubindo = 5.5f,
                        velocidadeDescendo = 15,
                        velocidadeDuranteOPulo = 5,
                        amortecimentoNaTransicaoDePulo = 1.2f
                    }
                }
            };
        }
    }
}

