using UnityEngine;

[System.Serializable]
public class MbEspada : ImpactoAereoBase
{


    public MbEspada() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.espada,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 3,
        potenciaMaxima = 9,
        potenciaMinima = 1,
        tempoDeReuso = 3.5f,
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
            NoImpacto.impactoComum,
            Trails.doisCubos,
            ToqueAoChao.impactoAoChao,
            PreparaSalto.preparaImpactoAoChao,
            ImpactoAereoFinal.MaisAltoQueOAlvo
            )
            ;

    }


}
