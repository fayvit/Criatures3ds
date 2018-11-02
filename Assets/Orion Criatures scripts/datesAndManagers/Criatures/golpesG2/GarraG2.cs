using UnityEngine;

[System.Serializable]
public class GarraG2 : ImpactoBase
{
    public GarraG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.garra,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 2,
        potenciaMaxima = 8,
        potenciaMinima = 1,
        tempoDeReuso = 3.5f,
        TempoNoDano = 0.5f,
        velocidadeDeGolpe = 14f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.3f,//74
        tempoDeMoveMax = 0.55f,
        tempoDeDestroy = 1.2f
    }
        )
    {
        carac = new CaracteristicasDeImpacto()
        {
            noImpacto = NoImpacto.impactoComum.ToString(),
            nomeTrail = Trails.tresCubos.ToString(),
            parentearNoOsso = true
        };
    }
}
