using UnityEngine;
using System.Collections;

[System.Serializable]
public class ProjetilBase : GolpeBase
{
    private bool addView = false;
    private bool animaEmissor = true;
    private float tempoDecorrido = 0;

    protected CaracteristicasDeProjetil carac = new CaracteristicasDeProjetil()
    {
        noImpacto = NoImpacto.impactoComum,
        tipo = TipoDoProjetil.basico
    };

    protected bool AnimaEmissor
    {
        get { return animaEmissor; }
        set { animaEmissor = value; }
    }

    public ProjetilBase(ContainerDeCaracteristicasDeGolpe C) : base(C) { }

    public override void IniciaGolpe(GameObject G)
    {
        addView = false;
        tempoDecorrido = 0;
        carac.posInicial = Emissor.UseOEmissor(G, this.Nome);
        DirDeREpulsao = G.transform.forward;
        if(AnimaEmissor)
            AnimadorCriature.AnimaAtaque(G, "emissor");
        else
            AnimadorCriature.AnimaAtaque(G,this.Nome.ToString());
    }

    public override void UpdateGolpe(GameObject G)
    {

        tempoDecorrido += Time.deltaTime;
        if (!addView )
        {
            addView = true;
            AplicadorDeProjeteis.AplicaProjetil(G, this, carac);
        }
    }
}
