using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ProjetilEletricoBase : GolpeBase
{
    [System.NonSerialized]protected bool addView = false;
    [System.NonSerialized]protected float tempoDecorrido = 0;
    [System.NonSerialized]private List<Transform> projeteis = new List<Transform>();

    protected CaracteristicasDeProjetil carac = new CaracteristicasDeProjetil()
    {
        noImpacto = NoImpacto.impactoEletrico,
        tipo = TipoDoProjetil.rigido
    };

    public ProjetilEletricoBase(ContainerDeCaracteristicasDeGolpe container) : base(container){ }

    public override void IniciaGolpe(GameObject G)
    {
        addView = false;
        tempoDecorrido = 0;
        carac.posInicial = Emissor.UseOEmissor(G, this.Nome);
        DirDeREpulsao = G.transform.forward;
        projeteis = new List<Transform>();
        AnimadorCriature.AnimaAtaque(G, "emissor");
    }

    protected void instanciaEletricidade(GameObject G, Vector3 paraOnde, float tempoMax = 10)
    {
        GolpePersonagem golpeP = GolpePersonagem.RetornaGolpePersonagem(G, Nome);

        if (golpeP.TempoDeInstancia > 0)
            carac.posInicial = Emissor.UseOEmissor(G, Nome);

        GameObject KY = AuxiliarDeInstancia.InstancieEDestrua(DoJogo.raioEletrico, carac.posInicial, DirDeREpulsao, TempoDeDestroy);
        Transform KXY = KY.transform.GetChild(0);
        MonoBehaviour.Destroy(KXY.gameObject, 4.9f);

        KXY.parent = G.transform.Find(golpeP.Colisor.osso).transform;
        KXY.localPosition = Vector3.zero;
        projeteis.Add(KY.transform);
        MbProjetilEletrico proj = KY.transform.GetChild(2).gameObject.AddComponent<MbProjetilEletrico>();
        proj.transform.position += golpeP.DistanciaEmissora * G.transform.forward + golpeP.AcimaDoChao * Vector3.up;
        proj.KXY = KXY;
        proj.criatureAlvo = CriaturesPerto.procureUmBomAlvo(G, 350);
        proj.forcaInicial = paraOnde.normalized;
        proj.velocidadeProjetil = 9;
        proj.tempoMax = tempoMax;
        proj.noImpacto = carac.noImpacto.ToString();
        proj.dono = G;
        proj.esseGolpe = this;
        addView = true;
    }
}
