using UnityEngine;
using System.Collections.Generic;

public class IruinG2
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Esqueleto/gomo1/cabeca"),
                Nome = nomesGolpes.gosmaDeInseto,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.chicoteDeCalda,
                NivelDoGolpe = 1,
                Colisor = new colisor("Esqueleto/gomo2/gomo3/rabo",
                                          new Vector3(0,0,0),
                                          new Vector3(-0.444f,0,0f),2),
            TaxaDeUso = 0.5f
            },new GolpePersonagem()
            {
                NivelDoGolpe = 2,
                ModPersonagem = 0,
                Colisor = new colisor("Esqueleto/gomo1/cabeca"),
                Nome = nomesGolpes.gosmaAcida,
                TaxaDeUso = 1.25f,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 7,
                Colisor = new colisor(),
                Nome = nomesGolpes.multiplicar,
                TaxaDeUso = 1.25f
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 8,
                ModPersonagem = 0,
                Colisor = new colisor("Esqueleto/gomo1/cabeca"),
                Nome = nomesGolpes.olharEnfraquecedor,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Iruin,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Inseto },
                    distanciaFundamentadora = 0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.19f},
                    PE = { Taxa = 0.2f},
                    Ataque = { Taxa = 0.17f},
                    Defesa = { Taxa = 0.22f},
                    Poder = { Taxa = 0.22f}
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