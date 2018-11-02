using UnityEngine;
using System.Collections;

[System.Serializable]
public class MbOndaVenenosa : ProjetilBase
{

    public MbOndaVenenosa() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.ondaVenenosa,
        tipo = nomeTipos.Veneno,
        carac = caracGolpe.projetil,
        custoPE = 2,
        potenciaCorrente = 4,
        potenciaMaxima = 10,
        potenciaMinima = 2,
        tempoDeReuso = 7.5f,
        tempoDeMoveMax = 1,
        tempoDeMoveMin = 0.3f,
        tempoDeDestroy = 2,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 18
    }
        )
    {
        carac = new CaracteristicasDeProjetil()
        {
            noImpacto = NoImpacto.impactoVenenoso,
            tipo = TipoDoProjetil.rigido
        };
    }

    public override void VerificaAplicaStatus(CriatureBase atacante, CreatureManager cDoAtacado)
    {
        VerificaAplicaStatusEnvenenado.VerificaAplicaStatus(atacante, cDoAtacado, this, 3);
    }

}
