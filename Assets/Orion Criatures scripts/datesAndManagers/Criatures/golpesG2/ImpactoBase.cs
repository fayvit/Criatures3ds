using UnityEngine;
using System.Collections;

[System.Serializable]
public class ImpactoBase : GolpeBase
{
    [System.NonSerialized]private AtualizadorDeImpactos aImpacto;
    protected CaracteristicasDeImpacto carac = new CaracteristicasDeImpacto()
    {
        noImpacto = NoImpacto.impactoComum.ToString(),
        nomeTrail = Trails.tresCubos.ToString(),
        parentearNoOsso = true
    };

    public ImpactoBase(ContainerDeCaracteristicasDeGolpe C) : base(C) { }

    public override void IniciaGolpe(GameObject G)
    {
        aImpacto = new AtualizadorDeImpactos();
        aImpacto.ReiniciaAtualizadorDeImpactos();
        AnimadorCriature.AnimaAtaque(G, Nome.ToString());
    }

    public override void UpdateGolpe(GameObject G)
    {
        aImpacto.ImpatoAtivo(G, this, carac);
    }
}
