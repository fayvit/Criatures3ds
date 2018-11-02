using UnityEngine;
using System.Collections.Generic;

public class Batler
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
            new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                Nome = nomesGolpes.sabreDeAsa,
                Colisor = new colisor("esqueleto/corpo",
                                   new Vector3(0.18f,0,0),
                                   new Vector3(-0.867f,-0.585f,-0.26f)),
                DistanciaEmissora = 1.5f,
                AcimaDoChao = 0.25f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("esqueleto/corpo/coxaD/pernaD/peD",
                                          new Vector3(0,0,0),
                                          new Vector3(-0.11f,-0,0.244f)),
                Nome = nomesGolpes.garra,
                TaxaDeUso = 0.5f,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.dentada,
                NivelDoGolpe = 1,
                Colisor = new colisor("esqueleto/corpo/",
                                                new Vector3(0,0,0.3f),
                                           new Vector3(-0.2f,0f,0.723f)),
                TaxaDeUso = 0.65f
            },new GolpePersonagem()
            {
                Nome = nomesGolpes.ventania,
                NivelDoGolpe = 2,
                Colisor = new colisor("esqueleto/corpo",
                                   new Vector3(0.18f,0,0),
                                   new Vector3(-0.867f,-0.585f,-0.26f)),
                TaxaDeUso = 1f
            },new GolpePersonagem()
            {
                Nome = nomesGolpes.ventosCortantes,
                NivelDoGolpe = 3,
                Colisor = new colisor("esqueleto/corpo"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.sobreVoo,
                NivelDoGolpe = 7,
                Colisor = new colisor("esqueleto/corpo/coxaD/pernaD/peD",
                                                   new Vector3(0,0,0),
                                                   new Vector3(-0.11f,-0,0.244f)),
                TaxaDeUso = 1.25f
            },new GolpePersonagem()
            {
                Nome = nomesGolpes.olharMal,
                NivelDoGolpe = 8,
                Colisor = new colisor("esqueleto/corpo"),
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
                NomeID = nomesCriatures.Batler,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Voador },
                    distanciaFundamentadora = -0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.19f,},
                    PE = { Taxa = 0.21f},
                    Ataque = { Taxa = 0.19f},
                    Defesa = { Taxa = 0.19f},
                    Poder = { Taxa = 0.22f}
                },
                    contraTipos = tipos.AplicaContraTipos(nomeTipos.Voador)
                },
                GerenteDeGolpes = new GerenciadorDeGolpes()
                {
                    listaDeGolpes = listaDosGolpes
                },
                Mov = new CaracteristicasDeMovimentacao()
                {
                    velocidadeAndando = 7,
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