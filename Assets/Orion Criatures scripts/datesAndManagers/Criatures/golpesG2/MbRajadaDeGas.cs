using UnityEngine;
using System.Collections;

[System.Serializable]
public class MbRajadaDeGas : ProjetilBase
{

    public MbRajadaDeGas() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.rajadaDeGas,
        tipo = nomeTipos.Gas,
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
            noImpacto = NoImpacto.impactoDeGas,
            tipo = TipoDoProjetil.rigido
        };
    }

}
