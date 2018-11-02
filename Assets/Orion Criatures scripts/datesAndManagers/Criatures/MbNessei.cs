using UnityEngine;
using System.Collections.Generic;

public class MbNessei
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
        new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                ModPersonagem = 0.1f,
                Colisor = new colisor("esqueleto/centro/c1/c2/c3/cabeca/bocaB"),
                Nome = nomesGolpes.olharMal,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f,
                TempoDeInstancia = 0.25f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0.1f,
                Colisor = new colisor("esqueleto/centro/c1/c2/c3/cabeca/bocaB"),
                Nome = nomesGolpes.rajadaDeAgua,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f,
                TempoDeInstancia = 0.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.chicoteDeCalda,
                NivelDoGolpe = 1,
                ModPersonagem = 0.2f,
                Colisor = new colisor("esqueleto/centroReverso/r1/r2/r3/rabo",
                                                  new Vector3(0,0f,0),
                                                  new Vector3(-0.093f,0.135f,-0.37f)),
                TaxaDeUso = 0.5f
            },new GolpePersonagem()
            {
                NivelDoGolpe = 2,
                ModPersonagem = 0.3f,
                Colisor = new colisor("esqueleto/centro/c1/c2/c3/cabeca/bocaB"),
                Nome = nomesGolpes.turboDeAgua,
                TaxaDeUso = 1.25f,
                DistanciaEmissora = 0.5f,
                TempoDeInstancia = 0.15f
            },new GolpePersonagem()
            {
                NivelDoGolpe = 7,
                ModPersonagem = 0.3f,
                Colisor = new colisor("esqueleto/centroReverso",
                                                            new Vector3(0,0f,0),
                                                            new Vector3(0,0,-0.66f)),
                Nome = nomesGolpes.hidroBomba,
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Nessei,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Agua },
                    distanciaFundamentadora = 0.11f,
                    meusAtributos = {
                    PV = { Taxa = 0.19f,},
                    PE = { Taxa = 0.21f},
                    Ataque = { Taxa = 0.26f},
                    Defesa = { Taxa = 0.17f},
                    Poder = { Taxa = 0.17f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Agua)
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
                },
                distanciaCameraLuta = 7f,
                alturaCameraLuta = 16
            };
        }
    }
}