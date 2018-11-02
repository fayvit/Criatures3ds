using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class PainelMostrarItem
{
    [SerializeField]private RawImage imgDoItem;
    [SerializeField]private Text  numDeItens;
    [SerializeField]private Text nomeDoItem;
    [SerializeField]private Text descricaoDoItem;

    public void IniciarPainel(nomeIDitem ID,int quantidade)
    {
        descricaoDoItem.transform.parent.gameObject.SetActive(true);
        imgDoItem.texture = GameController.g.El.RetornaMini(ID);
        numDeItens.text = quantidade.ToString();
        nomeDoItem.text = MbItens.NomeEmLinguas(ID);
        descricaoDoItem.text = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.shopInfoItem)[(int)(ID)];
    }

    public void DesligarPainel()
    {
        descricaoDoItem.transform.parent.gameObject.SetActive(false);
    }
    
}
