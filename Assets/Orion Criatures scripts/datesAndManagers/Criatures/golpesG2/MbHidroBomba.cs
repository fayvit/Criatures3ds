using UnityEngine;

[System.Serializable]
public class MbHidroBomba : ImpactoAereoBase
{


    public MbHidroBomba() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.hidroBomba,
        tipo = nomeTipos.Agua,
        carac = caracGolpe.colisaoComPow,
        custoPE = 3,
        potenciaCorrente = 7,
        potenciaMaxima = 14,
        potenciaMinima = 3,
        tempoDeReuso = 8.5f,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 20f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.55f,//74
        tempoDeMoveMax = 1.4f,
        tempoDeDestroy = 1.5f
    }
        )
    {
        carac = new CaracteristicasDeImpactoComSalto(
            NoImpacto.impactoDeAgua,
            Trails.hidroBomba,
            ToqueAoChao.aguaAoChao,
            PreparaSalto.preparaImpactoDeAguaAoChao,
            ImpactoAereoFinal.MaisAltoQueOAlvo
            );

    }


}