using UnityEngine;
using System.Collections;

[System.Serializable]
public class MbRajadaDeTerra : ProjetilBase
{
    public MbRajadaDeTerra() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.rajadaDeTerra,
        tipo = nomeTipos.Terra,
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
            noImpacto = NoImpacto.impactoDeTerra,
            tipo = TipoDoProjetil.basico
        };
    }

}
