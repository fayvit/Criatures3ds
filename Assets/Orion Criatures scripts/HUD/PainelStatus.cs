using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PainelStatus : MonoBehaviour
{
    [SerializeField]private RawImage[] abas;
    [SerializeField]private Image[] btnAbas;
    [SerializeField]private PainelDeGolpe[] pGolpe;
    [SerializeField]private Sprite selecionado;
    [SerializeField]private Sprite deselecionado;
    [SerializeField]private PainelDeCriature principal;
    //[SerializeField]private DadosDoPainelPrincipal principal;
    [SerializeField]private RectTransform containerDeTamanhoVariavel;
    [SerializeField]private ScrollRect sRect;
    [SerializeField] private StatusHudManager statusM;

    protected int indiceDoSelecionado = 0;
    private EstadosDaqui estado = EstadosDaqui.emEspera;
    private System.Action<int> acaoDeUsoDeItem;

    private enum EstadosDaqui
    {
        emEspera,
        selecionavel,
        janelaSuspensa
    }

    [System.Serializable]
    public class DadosDoPainelPrincipal
    {
        public RawImage imgDoPersonagem;
        public Text numPV;
        public Text numPE;
        public Text numAtk;
        public Text numDef;
        public Text numPod;
        public Text numXp;
        public Text txtMeusTipos;
        public Text txtNomeC;
        public Text numNivel;
    }

    void OnEnable()
    {
        CriatureBase[] ativos = GameController.g.Manager.Dados.CriaturesAtivos.ToArray();
        for (int i = 0; i < abas.Length; i++)
        {
            if (i < ativos.Length)
            {
                abas[i].transform.parent.gameObject.SetActive(true);
                abas[i].texture = GameController.g.El.RetornaMini(ativos[i].NomeID);
                btnAbas[i].sprite = deselecionado;
            }
            else
            {
                abas[i].transform.parent.gameObject.SetActive(false);
            }
        }

        btnAbas[0].sprite = selecionado;
        indiceDoSelecionado = 0;

        InserirDadosNoPainelPrincipal(ativos[0]);

        estado = EstadosDaqui.selecionavel;
        
    }

    protected void InserirDadosNoPainelPrincipal(CriatureBase C)
    {
        statusM.IniciarHudStatus(C);
        principal.InserirDadosNoPainelPrincipal(C);
        InsereGolpes(C.GerenteDeGolpes.meusGolpes.ToArray());
    }

    void InsereGolpes(GolpeBase[] golpes)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < golpes.Length)
            {
                pGolpe[i].Aciona(golpes[i]);
            }
            else
                pGolpe[i].gameObject.SetActive(false);
        }

        CalculaTamanhoDoContainer(golpes.Length);
    }

    void CalculaTamanhoDoContainer(int numGOlpes)
    {
        containerDeTamanhoVariavel.sizeDelta
               = new Vector2(0, numGOlpes * pGolpe[0].GetComponent<LayoutElement>().preferredHeight
               +principal.transform.GetComponent<LayoutElement>().preferredHeight
               );
    }

    float TamanhoDoContainer()
    {
        return containerDeTamanhoVariavel.sizeDelta.y; 
    }

    protected void AbaSelecionada(int indice)
    {
        for (int i = 0; i < btnAbas.Length; i++)
        {
            btnAbas[i].sprite = deselecionado;
        }

        sRect.verticalScrollbar.value = 1;
        GameController.g.StartCoroutine(ScrollPos());
        
        btnAbas[indice].sprite = selecionado;
        indiceDoSelecionado = indice;
    }
    private void LateUpdate()
    {
        switch (estado)
        {
            case EstadosDaqui.selecionavel:
                
                statusM.Update();
                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    Debug.Log("disparaacao");
                    if (acaoDeUsoDeItem != null)
                    {
                        UsarNeste();
                    }
                    else
                        BtnSubstituir();
                }
                else if (ActionManager.ButtonUp(1, GameController.g.Manager.Control))
                {
                    Debug.Log("disparacancel");
                    if (acaoDeUsoDeItem != null)
                    {
                        VoltarDosItens();
                    }
                    else
                        BtnVoltar();
                }

                float multiply = 50 / TamanhoDoContainer();
                float quanto = multiply * CommandReader.GetAxis("VDpad", GameController.g.Manager.Control);// Input.GetAxisRaw("EscolhaV");

                if (quanto == 0)
                    quanto = multiply * CommandReader.GetAxis("vertical", GameController.g.Manager.Control);// Input.GetAxisRaw("VerticalTeclado");

                
                int outroTanto = CommandReader.ValorDeGatilhos("HDpad", GameController.g.Manager.Control) 
                    + CommandReader.ValorDeGatilhosTeclado("horizontal", GameController.g.Manager.Control);

                if (outroTanto != 0)
                {
                    int numCriatures = GameController.g.Manager.Dados.CriaturesAtivos.Count;
                    if (indiceDoSelecionado + outroTanto >= numCriatures)
                        BtnMeuOutro(0);
                    else if (indiceDoSelecionado + outroTanto < 0)
                        BtnMeuOutro(numCriatures - 1);
                    else
                        BtnMeuOutro(indiceDoSelecionado + outroTanto);
                }

                sRect.verticalScrollbar.value += quanto;

                break;
        }
    }

    IEnumerator ScrollPos()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        if (sRect != null)
            if (sRect.verticalScrollbar)
                sRect.verticalScrollbar.value = 1;

    }

    public void AtivarParaItem(System.Action<int> a)
    {
        gameObject.SetActive(true);
        acaoDeUsoDeItem += a;
    }
    public void DesativarParaItem()
    {
        acaoDeUsoDeItem = null;
        gameObject.SetActive(false);
        GameController.g.HudM.MenuDePause.ReligarBotoesDoPainelDeItens();
    }

    public void DesligarMeusBotoes()
    {
        estado = EstadosDaqui.janelaSuspensa;
        BtnsManager.DesligarBotoes(gameObject);
    }

    public void ReligarMeusBotoes()
    {
        estado = EstadosDaqui.selecionavel;
        BtnsManager.ReligarBotoes(gameObject);
    }

    public virtual void BtnMeuOutro(int indice)
    {
        InserirDadosNoPainelPrincipal(GameController.g.Manager.Dados.CriaturesAtivos[indice]);
        AbaSelecionada(indice);
    }

    public void BtnVoltar()
    {
        gameObject.SetActive(false);
        GameController.g.HudM.MenuDePause.PausarJogo();
    }

    public void BtnSubstituir()
    {
        int vida = GameController.g.Manager.Dados.CriaturesAtivos[indiceDoSelecionado].CaracCriature.meusAtributos.PV.Corrente;
        if (GameController.g.EmEstadoDeAcao() && indiceDoSelecionado != 0 && vida > 0)
        {
            sRect.verticalScrollbar.value = 1;
            FindObjectOfType<PauseMenu>().VoltarAoJogo();
            gameObject.SetActive(false);
            GameController.g.FuncaoTrocarCriature(indiceDoSelecionado - 1,
                (GameController.g.Manager.Estado == EstadoDePersonagem.comMeuCriature) ?
                FluxoDeRetorno.menuCriature : FluxoDeRetorno.menuHeroi, true
                );
        }
        else if (vida <= 0)
        {
            DesligarMeusBotoes();
            GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(ReligarMeusBotoes,                 
                string.Format(
                BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.criatureParaMostrador)[2],
                GameController.g.Manager.Dados.CriaturesAtivos[indiceDoSelecionado].NomeEmLinguas));
        }
        else if (indiceDoSelecionado != 0)
        {
            GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.naoPodeEssaAcao), 30);
            //StartCoroutine(PauseMenu.VoltaTextoPause());
        }
        else if (indiceDoSelecionado == 0)
        {
            GameController.g.HudM.Painel.AtivarNovaMens(
                string.Format(
                    BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.naoPodeEssaAcao)[1],
                    GameController.g.Manager.CriatureAtivo.MeuCriatureBase.NomeEmLinguas)
                    , 30);
            //StartCoroutine(PauseMenu.VoltaTextoPause());
        }        
    }

    public void VoltarDosItens()
    {
        DesativarParaItem();
        estado = EstadosDaqui.emEspera;
        GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.jogoPausado), 30);
    }

    public void UsarNeste()
    {        
        acaoDeUsoDeItem(indiceDoSelecionado);        
    }
}
