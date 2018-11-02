using UnityEngine;

[System.Serializable]
public class MbAvalanche : ImpactoAereoBase
{


    public MbAvalanche() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.avalanche,
        tipo = nomeTipos.Pedra,
        carac = caracGolpe.colisaoComPow,
        custoPE = 3,
        potenciaCorrente = 7,
        potenciaMaxima = 14,
        potenciaMinima = 3,
        tempoDeReuso = 8.5f,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 24f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.75f,//74
        tempoDeMoveMax = 1.6f,
        tempoDeDestroy = 1.7f
    }
        )
    {
        carac = new CaracteristicasDeImpactoComSalto(
            NoImpacto.impactoDePedra,
            Trails.avalanche,
            ToqueAoChao.impactoDePedraAoChao,
            PreparaSalto.preparaImpactoAoChao,
            ImpactoAereoFinal.MaisAltoQueOAlvo
            );

    }


}