using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MbEletricidadeConcentrada : ProjetilEletricoBase
{

    public MbEletricidadeConcentrada() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.eletricidadeConcentrada,
        tipo = nomeTipos.Eletrico,
        carac = caracGolpe.projetil,
        custoPE = 2,
        potenciaCorrente = 4,
        potenciaMaxima = 10,
        potenciaMinima = 2,
        tempoDeReuso = 7.5f,
        tempoDeMoveMax = 1,
        tempoDeMoveMin = 0,
        tempoDeDestroy = 2,
        TempoNoDano = 1.75f,
        velocidadeDeGolpe = 30
    }
        )
    {

    }

    public override void UpdateGolpe(GameObject G)
    {
        carac.posInicial += Vector3.up * 0.25f;
        if (!addView)
        {
            instanciaEletricidade(G,G.transform.forward, 1);
            addView = true;
        }
    }
}
