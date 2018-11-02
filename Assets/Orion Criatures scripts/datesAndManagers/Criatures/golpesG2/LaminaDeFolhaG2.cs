using UnityEngine;
using System.Collections;

[System.Serializable]
public class LaminaDeFolhaG2 : ProjetilBase {

    public LaminaDeFolhaG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.laminaDeFolha,
        tipo = nomeTipos.Planta,
        carac = caracGolpe.projetil,
        custoPE = 1,
        potenciaCorrente = 3,
        potenciaMaxima = 7,
        potenciaMinima = 1,
        tempoDeReuso = 5,
        tempoDeMoveMax = 1 ,
        tempoDeMoveMin = 0.3f,
        tempoDeDestroy = 2,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 18 ,
        podeNoAr = true
    }
        )
    {
        carac = new CaracteristicasDeProjetil()
        {
            noImpacto = NoImpacto.impactoDeFolhas,
            tipo = TipoDoProjetil.basico
        };
    }

}
