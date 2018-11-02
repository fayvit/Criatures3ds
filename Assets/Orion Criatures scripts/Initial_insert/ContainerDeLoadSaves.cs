using System.Collections;
using UnityEngine;

[System.Serializable]
public class ContainerDeLoadSaves : UiDeOpcoes
{
    private System.Action<int> acao;
    private System.Action<int> acaoDelete;
    private PropriedadesDeSave[] lista;

    public void IniciarHud(
        System.Action<int> acao,
        System.Action<int> acaoDelete,
        PropriedadesDeSave[] lista
        )
    {
        //this.opcoes = txDeOpcoes;
        this.acao += acao;
        this.acaoDelete += acaoDelete;
        this.lista = lista;
        IniciarHUD(lista.Length, TipoDeRedimensionamento.horizontal);
    }
    
    public override void SetarComponenteAdaptavel(GameObject G, int indice)
    {
        G.GetComponent<LoadButton>().SetarBotao(acao,acaoDelete,lista[indice],indice);
    }

    protected override IEnumerator MovendoScroll(UmaOpcao[] umaS, int rowCellCount)
    {
        return base.MovendoScroll_H(umaS, rowCellCount);
    }

    public override void MudarOpcao()
    {
        MudarOpcao_H();
    }

    protected override void FinalizarEspecifico()
    {
        acao = null;
        acaoDelete = null;
    }
}
