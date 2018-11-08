using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PainelDeItens : MonoBehaviour
{
    [SerializeField]private Text infos;
    [SerializeField]private MenuDeImagens insereI;
    [SerializeField]private Button voltar;
    [SerializeField]private Button usar;
    [SerializeField]private Text txtBtnOrganizar;
    [SerializeField]private Sprite deselecionado;
    [SerializeField]private Sprite selecionado;

    private PainelMensCriature p;
    private MbItens[] meusItens;
    private int oSelecionado = -1;
    private bool modoOrganizar = false;

    private System.Action acao;
    private System.Action voltarEspecifico;

    private EstdoDaqui estado = EstdoDaqui.emEspera;

    private enum EstdoDaqui
    {
        emEspera,
        selecaoDeItem,
        painelSuspensoAberto
    }

    public void Ativar(System.Action a,System.Action v = null)
    {
        gameObject.SetActive(true);
        acao += a;
        voltarEspecifico += v;
    }

    void OnEnable()
    {
        p = GameController.g.HudM.Painel;
        estado = EstdoDaqui.selecaoDeItem;
        SetarMenuDeIetns();
        oSelecionado = -1;
        if (meusItens.Length > 0)
            infos.text = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.shopInfoItem)[(int)(meusItens[0].ID)];// bancoDeTextos.textosDeInterface[heroi.linguaChave][InterfaceTextKey.toqueSobreUmItem];
        else
            infos.text = "";
    }

    void OnDisable()
    {        
        insereI.FinalizarHud();        
    }

    void SetarMenuDeIetns()
    {
        meusItens = GameController.g.Manager.Dados.Itens.ToArray();
        insereI.IniciarHud(GameController.g.Manager.Dados,
            TipoDeDado.item, meusItens.Length, AoClique, 0, TipoDeRedimensionamento.emGrade
            );
    }

    public void LateUpdate()
    {
        switch (estado)
        {
            case EstdoDaqui.selecaoDeItem:


                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    
                    if (modoOrganizar)
                        AoClique(insereI.OpcaoEscolhida);
                    else
                    {
                        oSelecionado = insereI.OpcaoEscolhida;
                        BtnUsarItem();
                    }
                }
                else if (ActionManager.ButtonUp(1, GameController.g.Manager.Control))
                {
                    
                    BtnVoltar();
                }
                else if (ActionManager.ButtonUp(3,GameController.g.Manager.Control))
                    BtnOrganizar();


                int quanto = -insereI.LineCellCount() *CommandReader.ValorDeGatilhos("VDpad", GameController.g.Manager.Control);

                if (quanto == 0)
                    quanto = -insereI.LineCellCount()*CommandReader.ValorDeGatilhosTeclado("vertical", GameController.g.Manager.Control);

                if(quanto==0)
                    quanto = CommandReader.ValorDeGatilhos("HDpad", GameController.g.Manager.Control) + CommandReader.ValorDeGatilhos("horizontal", GameController.g.Manager.Control);

                insereI.MudarOpcaoComVal(quanto,insereI.LineCellCount());

                if (quanto != 0 && meusItens.Length > 0)
                    infos.text
                        = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.shopInfoItem)
                        [(int)(meusItens[insereI.OpcaoEscolhida].ID)];
                else if (meusItens.Length <= 0)
                    infos.text = "";
            break;
        }
    }

    void AoClique(int i)
    {
        if (!modoOrganizar)
        {
            infos.text = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.shopInfoItem)[(int)(meusItens[i].ID)];
            MudaSpriteDoItem(i);
        }
        else
        {
            if (oSelecionado >= 0)
            {
                
                insereI.FinalizarHud();
                TrocarDePosicao(oSelecionado, i);
                infos.text = BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.selecioneParaOrganizar);

                gameObject.SetActive(true);
                    
            }
            else
            {                
                MudaSpriteDoItem(i);
                infos.text = string.Format(
                                BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.selecioneParaOrganizar)[1],
                                MbItens.NomeEmLinguas(meusItens[oSelecionado].ID));
            }
        }
    }

    void MudaSpriteDoItem(int j)
    {
        DadosDeMiniaturas[] d = GetComponentsInChildren<DadosDeMiniaturas>();
        for (int i = 0; i < d.Length; i++)
        {
            d[i].DaSelecao.sprite = deselecionado;
        }

        oSelecionado = j;
        if (j > -1)           
            d[j].DaSelecao.sprite = selecionado;       

    }

    void TrocarDePosicao(int a, int b)
    {
        MbItens temp = (MbItens)meusItens[a].Clone();
        meusItens[a] = (MbItens)meusItens[b].Clone();
        meusItens[b] = temp;

        GameController.g.Manager.Dados.Itens = new System.Collections.Generic.List<MbItens>();
        GameController.g.Manager.Dados.Itens.AddRange(meusItens);
    }

    public void AtualizaHudDeItens()
    {
        OnDisable();
        gameObject.SetActive(true);
    }

    public void BtnUsarItem()
    {
        if (!GameController.g.estaEmLuta)
        {
            if (GameController.g.EmEstadoDeAcao() && oSelecionado > -1)
            {
                BtnsManager.DesligarBotoes(gameObject);
                GameController.g.FuncaoDoUseiItem(oSelecionado, FluxoDeRetorno.menuHeroi);
                estado = EstdoDaqui.painelSuspensoAberto;
            }
            else if (oSelecionado <= -1)
            {
                p.AtivarNovaMens(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.naoPodeEssaAcao)[2], 30);
                StartCoroutine(PauseMenu.VoltaTextoPause());
            }
            else
            {
                p.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.naoPodeEssaAcao), 30);
                StartCoroutine(PauseMenu.VoltaTextoPause());
            }
        }
        else
        {
            BtnsManager.DesligarBotoes(gameObject);
            GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(()=> {
                //int guarda = oSelecionado;
                insereI.FinalizarHud();
                gameObject.SetActive(true);
                
                BtnsManager.ReligarBotoes(gameObject);
            }, BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[10]);
        }
    }

    public void BtnVoltar()
    {
        if (voltarEspecifico != null)
            voltarEspecifico();

        voltarEspecifico = null;

        gameObject.SetActive(false);
        acao();
        acao = null;
        SairDoModoOrganizar();
        infos.text = "Toque sobre um item para seleciona-lo";
        ColetorDeLixo.Coleta();
        GameController.g.HudM.MenuDePause.PausarJogo();
    }

    public void BtnOrganizar()
    {
        if (!modoOrganizar)
        {
            if (oSelecionado < 0)
            {
                infos.text = BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.selecioneParaOrganizar);
            }
            else
            {
                infos.text = string.Format(
                    BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.selecioneParaOrganizar)[1],
                    MbItens.NomeEmLinguas(meusItens[oSelecionado].ID));
            }
            EntrarNoModoOrganizar();
        }
        else
        {
            SairDoModoOrganizar();
            if (oSelecionado > -1)
            {
                infos.text =  BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.shopInfoItem)[(int)(meusItens[oSelecionado].ID)];
            }
            else
                infos.text = "Toque sobre um item para seleciona-lo";
        }
    }

    void EntrarNoModoOrganizar()
    {
        modoOrganizar = true;
        txtBtnOrganizar.text = "Sair do Modo Organizar";
        txtBtnOrganizar.fontSize = 18;
        voltar.interactable = false;
        usar.interactable = false;
    }

    void SairDoModoOrganizar()
    {
        modoOrganizar = false;
        txtBtnOrganizar.text = "Organizar";
        txtBtnOrganizar.fontSize = 24;
        voltar.interactable = true;
        usar.interactable = true;
    }

    public void EstadoAtivo()
    {
        estado = EstdoDaqui.selecaoDeItem;
    }
}
