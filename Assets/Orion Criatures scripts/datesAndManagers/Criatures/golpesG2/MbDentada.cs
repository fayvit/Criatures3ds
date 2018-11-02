using UnityEngine;

[System.Serializable]
public class MbDentada : ImpactoAereoBase
{


    public MbDentada() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.dentada,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 2,
        potenciaMaxima = 8,
        potenciaMinima = 1,
        tempoDeReuso = 3.5f,
        TempoNoDano = 0.5f,
        velocidadeDeGolpe = 18f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 66,
        tempoDeMoveMin = 0.75f,//74
        tempoDeMoveMax = 1.4f,
        tempoDeDestroy = 1.6f
    }
        )
    {
        carac = new CaracteristicasDeImpactoComSalto(
            NoImpacto.impactoComum,
            Trails.dentada,
            ToqueAoChao.impactoAoChao,
            PreparaSalto.impactoBaixo,
            ImpactoAereoFinal.AvanceEPareAbaixo
            );

    }


}
