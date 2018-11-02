using UnityEngine;
using System.Collections;

[System.Serializable]
public class ImpactoAereoBase : GolpeBase
{
    protected CaracteristicasDeImpactoComSalto carac;
    [System.NonSerialized]protected AtualizadorDeImpactoAereo aImpacto = new AtualizadorDeImpactoAereo();

    public ImpactoAereoBase(ContainerDeCaracteristicasDeGolpe C) : base(C) { }

    public override void IniciaGolpe(GameObject G)
    {

        if (aImpacto == null)
            aImpacto = new AtualizadorDeImpactoAereo();

        aImpacto.ReiniciaAtualizadorDeImpactos(G);
        DirDeREpulsao = G.transform.forward;
        AnimadorCriature.AnimaAtaque(G, "emissor");

        GameObject instancia = GameController.g.El.retorna(carac.prepara.ToString());
        MonoBehaviour.Destroy(
        MonoBehaviour.Instantiate(instancia, G.transform.position, Quaternion.LookRotation(G.transform.forward)), 5);
    }

    public override void UpdateGolpe(GameObject G)
    {
        aImpacto.ImpactoAtivo(G, this, carac);
    }

    public override void  FinalizaEspecificoDoGolpe()
    {
        aImpacto.ReligarNavMesh();
        aImpacto.DestruirAo();
    }
}


[System.Serializable]
public struct CaracteristicasDeImpactoComSalto
{
    public NoImpacto noImpacto;
    public Trails trail;
    public ToqueAoChao toque;
    public PreparaSalto prepara;
    public ImpactoAereoFinal final;
    public bool parentearNoOsso;

    public CaracteristicasDeImpactoComSalto(
        NoImpacto noImpacto,
        Trails trail,
        ToqueAoChao toque,
        PreparaSalto prepara,
        ImpactoAereoFinal final,
        bool parentearNoOsso = true
        )
    {
        this.noImpacto = noImpacto;
        this.trail = trail;
        this.toque = toque;
        this.prepara = prepara;
        this.final = final;
        this.parentearNoOsso = parentearNoOsso;
    }

    public CaracteristicasDeImpacto deImpacto
    {
        get
        {
            return new CaracteristicasDeImpacto()
            {
                noImpacto = this.noImpacto.ToString(),
                nomeTrail = this.trail.ToString(),
                parentearNoOsso = this.parentearNoOsso
            };
        }
    }
}

public enum ToqueAoChao
{
    nulo,
    impactoAoChao,
    impactoDePedraAoChao,
    aguaAoChao,
    impactoDeFogo,
    eletricidadeAoChao,
    poeiraAoVento
}

public enum PreparaSalto
{
    nulo,
    preparaImpactoAoChao,
    impactoBaixo,
    impactoBaixoDeFolhas,
    preparaImpactoDeAguaAoChao,
    eletricidadeAoChao,
    preparaImpactoDeFogoAoChao,
    impulsoVenenoso
}
