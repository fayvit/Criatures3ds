using UnityEngine;
using System.Collections;

[System.Serializable]
public class MbAgulhaVenenosa : ProjetilBase
{
    public MbAgulhaVenenosa() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.agulhaVenenosa,
        tipo = nomeTipos.Veneno,
        carac = caracGolpe.projetil,
        custoPE = 1,
        potenciaCorrente = 3,
        potenciaMaxima = 7,
        potenciaMinima = 1,
        tempoDeReuso = 5,
        tempoDeMoveMax = 1,
        tempoDeMoveMin = 0.3f,
        tempoDeDestroy = 2,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 10
    }
        )
    {
        carac = new CaracteristicasDeProjetil()
        {
            noImpacto = NoImpacto.impactoVenenoso,
            tipo = TipoDoProjetil.basico
        };
    }

    public override void VerificaAplicaStatus(CriatureBase atacante, CreatureManager cDoAtacado)
    {
        VerificaAplicaStatusEnvenenado.VerificaAplicaStatus(atacante, cDoAtacado, this, 2);
    }

}
