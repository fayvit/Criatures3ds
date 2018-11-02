using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UmaOpcaoDeShop : UmaOpcaoDeMenu
{
    [SerializeField] private RawImage imgDoItem;
    [SerializeField] private Text precoDoItem;
    private int indice = -1;

    public void SetarOpcao(System.Action<int> acaoDaOpcao, string txtDaOpcao, bool comprar = true,int indice = -1)
    {
        this.indice = indice;
        nomeIDitem nomeID = (nomeIDitem)System.Enum.Parse(typeof(nomeIDitem), txtDaOpcao);
        Acao += acaoDaOpcao;
        TextoOpcao.text = MbItens.NomeEmLinguas(nomeID);
        imgDoItem.texture = GameController.g.El.RetornaMini(nomeID);
        if (comprar)
        {
            precoDoItem.text = PegaUmItem.Retorna(nomeID).Valor.ToString();
            indice = transform.GetSiblingIndex() - 1;
        }
        else
        {
            int valor = PegaUmItem.Retorna(nomeID).Valor;
            if (valor > 0)
            {
                precoDoItem.text = (Mathf.Max(valor / 4, 1)).ToString();
            }
        }
    }
    
    public override void FuncaoDoBotao()
    {
        Acao(transform.GetSiblingIndex()-1);
    }
}
