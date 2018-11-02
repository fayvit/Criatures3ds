using UnityEngine;
using System.Collections;

[System.Serializable]
public class PsicoseG2 : ProjetilBase
{

    public PsicoseG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.psicose,
        tipo = nomeTipos.Psiquico,
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
        velocidadeDeGolpe = 18
    }
        )
    {
        carac = new CaracteristicasDeProjetil()
        {
            noImpacto = NoImpacto.impactoComum,
            tipo = TipoDoProjetil.direcional
        };
    }

}
