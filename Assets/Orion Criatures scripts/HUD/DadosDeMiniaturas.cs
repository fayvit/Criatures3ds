using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DadosDeMiniaturas : UmaOpcao
{
    [SerializeField] private RawImage imgDoDado;
    [SerializeField] private Image daSelecao;
    [SerializeField] private Text txtDoDado;
    [SerializeField] private Text quantidade;
    [SerializeField] private GameObject containerDaQuantidade;
    //[SerializeField] private Text txtDoBtn;

    public Image DaSelecao
    {
        get { return daSelecao; }
        set { daSelecao = value; }
    }

    public void SetarGolpe(nomesGolpes nomeG)
    {
        containerDaQuantidade.SetActive(false);
        imgDoDado.texture = GameController.g.El.RetornaMini(nomeG);
        txtDoDado.text = GolpeBase.NomeEmLinguas(nomeG);
    }

    public void SetarAcao(System.Action<int> acao)
    {
        Acao += acao;
    }

    public void LimparAcao()
    {
        Acao = null;
    }

    public void SetarDados(DadosDoPersonagem dados, int indice, TipoDeDado tipo, System.Action<int> ao)
    {
        Acao += ao;
        switch (tipo)
        {
            case TipoDeDado.item:
                imgDoDado.texture = GameController.g.El.RetornaMini(dados.Itens[indice].ID);
                txtDoDado.text = MbItens.NomeEmLinguas(dados.Itens[indice].ID);
                quantidade.text = dados.Itens[indice].Estoque.ToString();
            break;
            case TipoDeDado.golpe:
                nomesGolpes nomeG = dados.CriaturesAtivos[0].GerenteDeGolpes.meusGolpes[indice].Nome;
                SetarGolpe(nomeG);
            break;
            case TipoDeDado.criature:
                containerDaQuantidade.SetActive(false);
                imgDoDado.texture = GameController.g.El.RetornaMini(dados.CriaturesAtivos[indice + 1].NomeID);
                txtDoDado.text = dados.CriaturesAtivos[indice + 1].NomeEmLinguas;
            break;
        }

       // if (indice < 5 /*&& !GameController.g.HudM.PauseM.gameObject.activeSelf*/)
          //  txtDoBtn.text = "n" + (indice + 1);
        //else
            //txtDoBtn.transform.parent.gameObject.SetActive(false);
    }

    /*
    public void AtualizaTxtDoBtn(int deslocamento)
    {
       // if (transform.GetSiblingIndex() < deslocamento || transform.GetSiblingIndex() > deslocamento + 5)
            txtDoBtn.transform.parent.gameObject.SetActive(false);
        /*else
        {
            txtDoBtn.transform.parent.gameObject.SetActive(true);
            txtDoBtn.text = "n" + (transform.GetSiblingIndex() - deslocamento);
        }*/
   // }
}

public enum TipoDeDado
{
    item,
    golpe,
    criature
}
