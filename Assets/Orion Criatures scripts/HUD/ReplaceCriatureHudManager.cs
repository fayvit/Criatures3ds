using UnityEngine;
using System.Collections;

[System.Serializable]
public class ReplaceCriatureHudManager : UiDeOpcoes
{
    private CriatureBase[] listaDeCriatures;
    private System.Action<int> aoClique;
    private bool podeMudar = true;
    private bool armagedom = false;

    public bool PodeMudar
    {
        get { return podeMudar; }
        set { podeMudar = value; }
    }

    public void IniciarEssaHUD(CriatureBase[] listaDeCriatures,System.Action<int> AoEscolherUmCriature,bool armagedom = false)
    {
        this.armagedom = armagedom;
        this.listaDeCriatures = listaDeCriatures;
        PodeMudar = true;
        aoClique += AoEscolherUmCriature;
        IniciarHUD(listaDeCriatures.Length);
        ActionManager.ModificarAcao(GameController.g.transform, AcaoDeOpcaoEscolhida);
    }

    public void AcaoDeOpcaoEscolhida()
    {
        if (painelDeTamanhoVariavel.transform.parent.parent.gameObject.activeSelf)
        {
            painelDeTamanhoVariavel.GetChild(OpcaoEscolhida + 1).GetComponent<UmaOpcao>().FuncaoDoBotao();
            podeMudar = false;
            Debug.Log("A opção escolhida é: " + OpcaoEscolhida);
        }
        else
        {
            Debug.LogWarning("AcaoDeOpcaoEscolhida chamada indevidamente!!! é preciso saber o porque");
            ActionManager.ModificarAcao(GameController.g.transform, null);
        }
    }

    public void Update()
    {
        if(PodeMudar)
            MudarOpcao();
    }

    public override void SetarComponenteAdaptavel(GameObject G,int indice)
    {
        G.GetComponent<CriatureParaMostrador>().SetarCriature(listaDeCriatures[indice], aoClique,armagedom);
    }

    protected override void FinalizarEspecifico()
    {
        aoClique = null;
    }
}
