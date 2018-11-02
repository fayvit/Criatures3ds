using UnityEngine;
using System.Collections;

public class PainelDeEscolhaINicial : PainelStatus
{
    [SerializeField]private GameObject painelDosElementos;

    private CriatureBase[] criaturesIniciais;
    private NpcDoPrimeiroMiniArmagedom npcMini;
    private int selecited = -1;

    public bool TemIndiceEscolhido
    {
        get { return selecited != -1; }
    }
    void OnEnable()
    {
        painelDosElementos.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        criaturesIniciais = new CriatureBase[3]
            {
                new CriatureBase(nomesCriatures.Florest),
                new CriatureBase(nomesCriatures.PolyCharm),
                new CriatureBase(nomesCriatures.Xuash)
            };
    }

    public void MudarSelecionado(int i)
    {
        if (i > 0)
        {
            if (selecited + i < 3)
                selecited += i;
            else
                selecited = 0;
        }
        else if (i < 0)
        {
            if (selecited + i > -1)
                selecited += i;
            else
                selecited = 2;
        }

        BtnMeuOutro(selecited);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void BtnMeuOutro(int indice)
    {
        indiceDoSelecionado = indice;
        painelDosElementos.SetActive(true);
        InserirDadosNoPainelPrincipal(criaturesIniciais[indice]);
        AbaSelecionada(indice);
    }

    public void BtnEscolher(NpcDoPrimeiroMiniArmagedom npcMini)
    {
        this.npcMini = npcMini;
        //BtnsManager.DesligarBotoes(gameObject);
        GameController.g.HudM.Confirmacao.AtivarPainelDeConfirmacao(SimEscolhiEsse, AindaNaoEscolhi,
            string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.certezaDeEscolhaInicial),
            criaturesIniciais[indiceDoSelecionado].NomeEmLinguas,
            tipos.NomeEmLinguas(criaturesIniciais[indiceDoSelecionado].CaracCriature.meusTipos[0]))
            );
    }

    void FinalizaEscolhaInicial()
    {
        gameObject.SetActive(false);
        GameController.g.HudM.Painel.EsconderMensagem();
        CharacterManager manager = GameController.g.Manager;
        manager.Dados.CriaturesAtivos = new System.Collections.Generic.List<CriatureBase>(){ criaturesIniciais[indiceDoSelecionado]};
//        GameController.g.MyKeys.MudaShift(KeyShift.estouNoTuto,false);
        GameObject G = InicializadorDoJogo.InstanciaCriature(manager.transform, manager.Dados.CriaturesAtivos[0]);
        InicializadorDoJogo.InsereCriatureEmJogo(G, manager);
        GameController.g.HudM.AtualizaDadosDaHudVida(false);
        npcMini.EstadoDeMostrarCaminho();

    }

    void SimEscolhiEsse()
    {
        GameController.g.MyKeys.MudaShift(KeyShift.estouNoTuto, true);
        GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(FinalizaEscolhaInicial,string.Format(
            BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.voceRecebeuCriature), criaturesIniciais[indiceDoSelecionado].NomeEmLinguas
            ));
        
    }

    void AindaNaoEscolhi()
    {
        npcMini.EstadoDeEscolha();
        ActionManager.ModificarAcao(GameController.g.transform, npcMini.AcaoDeEscolha);
        //BtnsManager.ReligarBotoes(gameObject);
    }

}
