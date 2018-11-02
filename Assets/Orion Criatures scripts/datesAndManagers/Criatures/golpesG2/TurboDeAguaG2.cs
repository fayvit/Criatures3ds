using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurboDeAguaG2 : ProjetilBase
{

    public TurboDeAguaG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.turboDeAgua,
        tipo = nomeTipos.Agua,
        carac = caracGolpe.projetil,
        custoPE = 2,
        potenciaCorrente = 4,
        potenciaMaxima = 10,
        potenciaMinima = 2,
        tempoDeReuso = 7.5f,
        tempoDeMoveMax = 1.25f,
        tempoDeMoveMin = 0f,
        tempoDeDestroy = 2,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 15
    }
        )
    {
        carac = new CaracteristicasDeProjetil()
        {
            noImpacto = NoImpacto.impactoDeAgua,
            tipo = TipoDoProjetil.rigido
        };
    }

}
