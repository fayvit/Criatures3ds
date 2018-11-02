using UnityEngine;
using System.Collections;

[System.Serializable]
public class MbCascalho : ProjetilBase
{
    public MbCascalho() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.cascalho,
        tipo = nomeTipos.Pedra,
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
            noImpacto = NoImpacto.impactoDePedra,
            tipo = TipoDoProjetil.basico
        };
    }

}
