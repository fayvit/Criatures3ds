using UnityEngine;

[System.Serializable]
public class MbChute : ImpactoBase
{
    public MbChute() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.chute,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 2,
        potenciaMaxima = 8,
        potenciaMinima = 1,
        tempoDeReuso = 3.5f,
        TempoNoDano = 0.5f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.5f,//74
        tempoDeMoveMax = 0.95f,
        tempoDeDestroy = 1.1f,
        velocidadeDeGolpe = 28
    }
        )
    {
        carac = new CaracteristicasDeImpacto()
        {
            noImpacto = NoImpacto.impactoComum.ToString(),
            nomeTrail = Trails.umCuboETrail.ToString(),
            parentearNoOsso = true
        };
    }
}
