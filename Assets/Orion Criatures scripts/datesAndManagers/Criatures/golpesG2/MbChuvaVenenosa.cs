using UnityEngine;

[System.Serializable]
public class MbChuvaVenenosa : ImpactoAereoBase
{

    
    public MbChuvaVenenosa() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.chuvaVenenosa,
        tipo = nomeTipos.Veneno,
        carac = caracGolpe.colisaoComPow,
        custoPE = 3,
        potenciaCorrente = 7,
        potenciaMaxima = 14,
        potenciaMinima = 3,
        tempoDeReuso = 8.5f,
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
            NoImpacto.impactoVenenoso,
            Trails.chuvaVenenosa,
            ToqueAoChao.poeiraAoVento,
            PreparaSalto.impulsoVenenoso,
            ImpactoAereoFinal.MaisAltoQueOAlvo,
            false
            )
            ;

    }

    public override void UpdateGolpe(GameObject G)
    {
        aImpacto.ImpactoAtivo(G, this, carac,0.35f);
    }

    public override void VerificaAplicaStatus(CriatureBase atacante, CreatureManager cDoAtacado)
    {
        VerificaAplicaStatusEnvenenado.VerificaAplicaStatus(atacante, cDoAtacado, this, 4);
    }


}
