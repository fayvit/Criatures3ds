using UnityEngine;

[System.Serializable]
public class ChicoteDeCaldaG2 : ImpactoBase
{
    public ChicoteDeCaldaG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.chicoteDeCalda,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 2,
        potenciaMaxima = 8,
        potenciaMinima = 1,
        tempoDeReuso = 3.5f,
        TempoNoDano = 0.65f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 53,
        tempoDeMoveMin = 0.5f,//74
        tempoDeMoveMax = 1.2f,
        tempoDeDestroy = 1.4f,
        velocidadeDeGolpe = 20
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
