using UnityEngine;
using System.Collections;

[System.Serializable]
public class Sabre : ProjetilBase
{
    public Sabre(nomesGolpes nome) : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nome,
        tipo = nomeTipos.Normal,
        carac = caracGolpe.colisao,
        custoPE = 0,
        potenciaCorrente = 5,
        potenciaMaxima = 11,
        potenciaMinima = 2,
        tempoDeReuso = 5,
        tempoDeMoveMax = 0.65f,
        tempoDeMoveMin = 0.25f,
        tempoDeDestroy = 1,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 5
    }
        )
    {
        carac = new CaracteristicasDeProjetil()
        {
            noImpacto = NoImpacto.impactoComum,
            tipo = TipoDoProjetil.basico
        };

        AnimaEmissor = false;
    }

}
