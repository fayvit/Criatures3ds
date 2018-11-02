using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MbEletricidade : ProjetilEletricoBase
{
    [System.NonSerialized]private int raios = 0;

    public MbEletricidade() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.eletricidade,
        tipo = nomeTipos.Eletrico,
        carac = caracGolpe.projetil,
        custoPE = 1,
        potenciaCorrente = 3,
        potenciaMaxima = 7,
        potenciaMinima = 1,
        tempoDeReuso = 5,
        tempoDeMoveMax = 1,
        tempoDeMoveMin = 0,
        tempoDeDestroy = 2,
        TempoNoDano = 1.75f,
        velocidadeDeGolpe = 30
    }
        )
    {

    }

    public override void IniciaGolpe(GameObject G)
    {
        base.IniciaGolpe(G);
        raios = 0;
    }

    public override void UpdateGolpe(GameObject G)
    {

        tempoDecorrido += Time.deltaTime;
        if (raios < 5 && tempoDecorrido > (TempoDeMoveMin + raios * (TempoDeMoveMax - TempoDeMoveMin) / 35))
        {
            
            float tempinho = TempoDeMoveMax - tempoDecorrido;
            switch (raios)
            {
                case 0:
                    carac.posInicial += Vector3.up * 0.25f;
                    instanciaEletricidade(G, G.transform.forward, tempinho);
                break;
                case 1:
                    carac.posInicial = G.transform.position + G.transform.right + 5 * Vector3.up;
                    instanciaEletricidade(G,G.transform.right + Vector3.up + G.transform.forward, tempinho);
                break;
                case 2:
                    carac.posInicial = G.transform.position + G.transform.right + Vector3.up;
                    instanciaEletricidade(G,G.transform.right + Vector3.up + G.transform.forward, tempinho);
                break;
                case 3:
                    carac.posInicial = G.transform.position - G.transform.right + 5 * Vector3.up;
                    instanciaEletricidade(G,-G.transform.right + Vector3.up + G.transform.forward, tempinho);
                break;
                case 4:
                    carac.posInicial = G.transform.position - G.transform.right + Vector3.up;
                    instanciaEletricidade(G,-G.transform.right + Vector3.up + G.transform.forward, tempinho);
                break;
            }

            raios++;
        }
    }   
}
