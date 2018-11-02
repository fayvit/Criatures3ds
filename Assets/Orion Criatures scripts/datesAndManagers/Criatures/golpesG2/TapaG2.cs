using UnityEngine;

[System.Serializable]
public class TapaG2 : ImpactoBase
{
    public TapaG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.tapa,
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
        tempoDeMoveMin = 0.74f,//74
        tempoDeMoveMax = 1.2f,
        tempoDeDestroy = 1.4f,
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
