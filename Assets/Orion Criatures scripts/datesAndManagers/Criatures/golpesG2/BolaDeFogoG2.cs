using UnityEngine;
using System.Collections;

[System.Serializable]
public class BolaDeFogoG2 : ProjetilBase
{
    public BolaDeFogoG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.bolaDeFogo,
        tipo = nomeTipos.Fogo,
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
            noImpacto = NoImpacto.impactoDeFogo,
            tipo = TipoDoProjetil.basico
        };
    }

}
