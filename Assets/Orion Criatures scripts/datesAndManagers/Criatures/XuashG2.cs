using UnityEngine;
using System.Collections.Generic;

public class XuashG2
{
    static List<GolpePersonagem> listaDosGolpes = new List<GolpePersonagem>()
        {
            new GolpePersonagem()
            {
                NivelDoGolpe = 1,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/Tronco/pescoco/Cabeca/BocaD"),
                Nome = nomesGolpes.rajadaDeAgua,
                TaxaDeUso = 1,
                DistanciaEmissora = 0.5f
            },
            new GolpePersonagem()
            {
                Nome = nomesGolpes.tapa,
                NivelDoGolpe = 1,
                Colisor = new colisor("Arma__o/Tronco/ombroD/BracoD1/BracoD2/punhoD",
                                    new Vector3(0,0,0),
                                    new Vector3(-0.26f,-0,0)),
                TaxaDeUso = 0.5f
            },new GolpePersonagem()
            {
                NivelDoGolpe = 2,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/Tronco/pescoco/Cabeca/BocaD"),
                Nome = nomesGolpes.turboDeAgua,
                TaxaDeUso = 1.25f,
                DistanciaEmissora = 0.5f
            },new GolpePersonagem()
            {
                NivelDoGolpe = 7,
                ModPersonagem = 0,
                Colisor = new colisor("Arma__o/Tronco",
                                                  new Vector3(0,0,0),
                                                  new Vector3(-0.26f,-0,0)),
                Nome = nomesGolpes.hidroBomba,
                TaxaDeUso = 1.25f
            }
        };

    public static CriatureBase Criature
    {
        get {
            return new CriatureBase()
            {
                NomeID = nomesCriatures.Xuash,
                CaracCriature = new CaracteristicasDeCriature()
                {
                    meusTipos = new nomeTipos[1] { nomeTipos.Agua },
                    distanciaFundamentadora = -0.01f,
                    meusAtributos = {
                    PV = { Taxa = 0.19f,},
                    PE = { Taxa = 0.21f},
                    Ataque = { Taxa = 0.19f},
                    Defesa = { Taxa = 0.22f},
                    Poder = { Taxa = 0.19f}
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
                }
            };
        }
    }
}