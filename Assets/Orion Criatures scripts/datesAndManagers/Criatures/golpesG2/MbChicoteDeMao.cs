using UnityEngine;

[System.Serializable]
public class MbChicoteDeMao : ImpactoAereoBase
{


    public MbChicoteDeMao() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.chicoteDeMao,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 2,
        potenciaMaxima = 8,
        potenciaMinima = 1,
        tempoDeReuso = 3.5f,
        TempoNoDano = 0.5f,
        velocidadeDeGolpe = 28f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.75f,//74
        tempoDeMoveMax = 1.6f,
        tempoDeDestroy = 1.8f
    }
        )
    {
        carac = new CaracteristicasDeImpactoComSalto(
            NoImpacto.impactoComum,
            Trails.doisCubos,
            ToqueAoChao.impactoAoChao,
            PreparaSalto.preparaImpactoAoChao,
            ImpactoAereoFinal.MaisAltoQueOAlvo
            )
            ;

    }


}
