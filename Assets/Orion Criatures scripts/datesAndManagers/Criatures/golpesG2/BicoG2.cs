using UnityEngine;

[System.Serializable]
public class BicoG2 : ImpactoBase
{

    public BicoG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.bico,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 2,
        potenciaMaxima = 8,
        potenciaMinima = 1,
        tempoDeReuso = 3.5f,
        TempoNoDano = 0.5f,
        velocidadeDeGolpe = 25f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 33,
        tempoDeMoveMin = 0.3f,//74
        tempoDeMoveMax = 0.8f,
        tempoDeDestroy = 1f,
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

    /*
    public override void IniciaGolpe(GameObject G)
    {
        aImpacto.ReiniciaAtualizadorDeImpactos();
        AnimadorCriature.AnimaAtaque(G, Nome.ToString());
    }

    public override void UpdateGolpe(GameObject G)
    {
        aImpacto.ImpatoAtivo(G, this, carac);
    }*/
}
