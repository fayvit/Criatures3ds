using UnityEngine;

[System.Serializable]
public class MbTespestadeDeFolhas : ImpactoAereoBase
{


    public MbTespestadeDeFolhas() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.tempestadeDeFolhas,
        tipo = nomeTipos.Planta,
        carac = caracGolpe.colisaoComPow,
        custoPE = 3,
        potenciaCorrente = 7,
        potenciaMaxima = 14,
        potenciaMinima = 3,
        tempoDeReuso = 8.5f,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 30f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.65f,//74
        tempoDeMoveMax = 1.3f,
        tempoDeDestroy = 1.45f
    }
        )
    {
        carac = new CaracteristicasDeImpactoComSalto(
            NoImpacto.impactoDeFolhas,
            Trails.tempestadeDeFolhas,
            ToqueAoChao.poeiraAoVento,
            PreparaSalto.impactoBaixoDeFolhas,
            ImpactoAereoFinal.AvanceEPareAbaixo,
            false
            )
            ;

    }


}
