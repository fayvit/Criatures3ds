using UnityEngine;
using System.Collections.Generic;

public class MbAladegg
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
                Nome = nomesGolpes.ventania,
                NivelDoGolpe = 1,
                Colisor = new colisor("esqueleto/corpo",
                                   new Vector3(0.18f,0,0),
                                   new Vector3(-0.867f,-0.585f,-0.26f)),
                TaxaDeUso = 1f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("esqueleto/corpo/coxaD/pernaD/peD",
                                          new Vector3(0,0,0),
                                          new Vector3(-0.11f,-0,0.244f)),
                Nome = nomesGolpes.chute,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.ventosCortantes,
                NivelDoGolpe = 2,
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
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.olharEnfraquecedor,
                NivelDoGolpe = 8,
                Colisor = new colisor("esqueleto/corpo"),
                DistanciaEmissora = 1f,
                AcimaDoChao = 0.15f,
                TaxaDeUso = 1.25f
            },
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Aladegg,
                alturaCamera = 4,
                distanciaCamera = 5.5f,
                alturaCameraLuta = 6,
                distanciaCameraLuta = 4.5f,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Voador },
                    distanciaFundamentadora = 0.05f,
                    meusAtributos = {
                    PV = { Taxa = 0.18f,},
                    PE = { Taxa = 0.21f},
                    Ataque = { Taxa = 0.21f},
                    Defesa = { Taxa = 0.18f},
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
                    velocidadeAndando = 5f,
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

