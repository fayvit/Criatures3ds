using UnityEngine;

[System.Serializable]
public class MbTempestadeEletrica : ImpactoAereoBase
{


    public MbTempestadeEletrica() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.tempestadeEletrica,
        tipo = nomeTipos.Eletrico,
        carac = caracGolpe.colisaoComPow,
        custoPE = 3,
        potenciaCorrente = 7,
        potenciaMaxima = 14,
        potenciaMinima = 3,
        tempoDeReuso = 8.5f,
        TempoNoDano = 0.5f,
        velocidadeDeGolpe = 28f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.55f,//74
        tempoDeMoveMax = 1.4f,
        tempoDeDestroy = 1.6f
    }
        )
    {
        carac = new CaracteristicasDeImpactoComSalto(
            NoImpacto.impactoEletrico,
            Trails.tempestadeEletrica,
            ToqueAoChao.eletricidadeAoChao,
            PreparaSalto.eletricidadeAoChao,
            ImpactoAereoFinal.MaisAltoQueOAlvo,
            false
            )
            ;

    }


}
