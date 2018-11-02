using UnityEngine;

[System.Serializable]
public class MbSobreSalto : ImpactoAereoBase
{


    public MbSobreSalto() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.sobreSalto,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 4,
        potenciaMaxima = 10,
        potenciaMinima = 2,
        tempoDeReuso = 6.5f,
        TempoNoDano = 0.5f,
        velocidadeDeGolpe = 28f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.75f,//74
        tempoDeMoveMax = 1.6f,
        tempoDeDestroy = 1.7f,
        colisorScale = 2
    }
        )
    {
        carac = new CaracteristicasDeImpactoComSalto(
            NoImpacto.impactoComum,
            Trails.umCuboETrail,
            ToqueAoChao.impactoAoChao,
            PreparaSalto.preparaImpactoAoChao,
            ImpactoAereoFinal.MaisAltoQueOAlvo
            );

    }


}