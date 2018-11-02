using UnityEngine;
using System.Collections;

[System.Serializable]
public class GosmaAcidaG2 : ProjetilBase
{
    public GosmaAcidaG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.gosmaAcida,
        tipo = nomeTipos.Inseto,
        carac = caracGolpe.projetil,
        custoPE = 2,
        potenciaCorrente = 4,
        potenciaMaxima = 10,
        potenciaMinima = 2,
        tempoDeReuso = 7.5f,
        tempoDeMoveMax = 1,
        tempoDeMoveMin = 0.15f,
        tempoDeDestroy = 2,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 10
    }
        )
    {
        carac = new CaracteristicasDeProjetil()
        {
            noImpacto = NoImpacto.impactoDeGosmaAcida,
            tipo = TipoDoProjetil.basico
        };
    }

}
