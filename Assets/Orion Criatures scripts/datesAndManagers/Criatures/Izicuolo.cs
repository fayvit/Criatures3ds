using UnityEngine;
using System.Collections.Generic;

public class Izicuolo
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
            new GolpePersonagem()
            {
                NivelDoGolpe = -1,
                Nome = nomesGolpes.sabreDeBastao,
                Colisor = new colisor("Arma__o_001/Corpo/"),
                AcimaDoChao = 0.1f,
                DistanciaEmissora = 2
            },
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o_001/Corpo/braco_R/punho_R/punho_R_001",
                                         new Vector3(0,0,0),
                                         new Vector3(0.382f,-0.192f,0.509f)),
                Nome = nomesGolpes.bastao,
                TaxaDeUso = 0.5f,
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.gosmaDeInseto,
                NivelDoGolpe = 1,
                Colisor = new colisor("Arma__o_001/Corpo/",
                                                new Vector3(0,0,0.3f),
                                           new Vector3(-0.2f,0f,0.723f)),
                TaxaDeUso = 0.65f,
                AcimaDoChao = 0.1f
            },new GolpePersonagem()
            {
                Nome = nomesGolpes.gosmaAcida,
                NivelDoGolpe = 2,
                Colisor = new colisor("Arma__o_001/Corpo/",
                                   new Vector3(0.18f,0,0),
                                   new Vector3(-0.867f,-0.585f,-0.26f)),
                TaxaDeUso = 1f,
                AcimaDoChao = 0.1f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.multiplicar,
                NivelDoGolpe = 7,
                Colisor = new colisor(),
                TaxaDeUso = 1.25f
            },new GolpePersonagem()
            {
                Nome = nomesGolpes.olharMal,
                NivelDoGolpe = 8,
                Colisor = new colisor("Arma__o_001/Corpo/",
                                   new Vector3(0.18f,0,0),
                                   new Vector3(-0.867f,-0.585f,-0.26f)),
                TaxaDeUso = 1f,
                AcimaDoChao = 0.1f
            },
        };

    public static CriatureBase Criature
    {
        get
        {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Izicuolo,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Inseto },
                    distanciaFundamentadora = -0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.19f,},
                    PE = { Taxa = 0.21f},
                    Ataque = { Taxa = 0.19f},
                    Defesa = { Taxa = 0.19f},
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