using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CriatureParaMostrador : UmaOpcao
{
    [SerializeField]private RawImage imgDoCriature;
    [SerializeField]private Text txtPVnum;
    [SerializeField]private Text txtPEnum;
    [SerializeField]private Text nomeCriature;
    [SerializeField]private Text txtNivelNum;
    [SerializeField]private Text txtListaDeStatus;

    private bool armagedom = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void QueroColocarEsse()
    {
        if (Acao != null)
            Acao(transform.GetSiblingIndex() - 1);
        else
            Debug.LogError("A função hedeira não foi setada corretamente");
    }

    void DeVoltaAoMenu()
    {
        GameController.g.HudM.EntraCriatures.PodeMudar = true;
        ActionManager.ModificarAcao(GameController.g.transform, GameController.g.HudM.EntraCriatures.AcaoDeOpcaoEscolhida);
        //BtnsManager.ReligarBotoes(transform.parent.gameObject);
    }

    public override void FuncaoDoBotao()
    {
        //BtnsManager.DesligarBotoes(transform.parent.gameObject);

        if (int.Parse(txtPVnum.text.Split('/')[0]) > 0)
        {
            string texto =
                !armagedom ? string.Format(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.criatureParaMostrador)[0], nomeCriature.text)
                : string.Format(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.frasesDeArmagedom)[7], nomeCriature.text,txtNivelNum.text);



            GameController.g.HudM.Confirmacao.AtivarPainelDeConfirmacao(QueroColocarEsse, DeVoltaAoMenu,
                texto
                );
            //if (cliqueDoPersonagem != null)
            //    cliqueDoPersonagem(transform.GetSiblingIndex() - 1);
        }
        else
        {
            GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(DeVoltaAoMenu,
                string.Format(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.criatureParaMostrador)[1], nomeCriature.text)
                );
        }
    }

    public void SetarCriature(CriatureBase C, System.Action<int> ao, bool armagedom = false)
    {
        this.armagedom = armagedom;
        Acao += ao;

        Atributos A = C.CaracCriature.meusAtributos;

        imgDoCriature.texture = GameController.g.El.RetornaMini(C.NomeID);
        nomeCriature.text = C.NomeEmLinguas;
        txtNivelNum.text = C.CaracCriature.mNivel.Nivel.ToString();
        txtPVnum.text = A.PV.Corrente+" / "+A.PV.Maximo;
        txtPEnum.text = A.PE.Corrente + " / " + A.PE.Maximo;
        txtListaDeStatus.text = "";

        if (A.PV.Corrente <= 0)
        {
            Text[] txtS = GetComponentsInChildren<Text>();

            for (int i = 1; i < txtS.Length - 2; i++)
                txtS[i].color = Color.gray;

            txtS[0].color = new Color(1, 1, 0.75f);

            txtListaDeStatus.text = "derrotado";
        }
        else
            txtListaDeStatus.text = "preparado";
    }
}
