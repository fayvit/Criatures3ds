using UnityEngine;
using System.Collections;

[System.Serializable]
public class MbAnelDoOlhar : ProjetilBase
{

    public MbAnelDoOlhar() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.anelDoOlhar,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.projetil,
        custoPE = 2,
        potenciaCorrente = 8,
        potenciaMaxima = 16,
        potenciaMinima = 4,
        tempoDeReuso = 8.5f,
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
            noImpacto = NoImpacto.impactoComum,
            tipo = TipoDoProjetil.rigido
        };
    }

}
