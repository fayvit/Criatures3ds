using UnityEngine;
using System.Collections;

[System.Serializable]
public class NpcDoPrimeiroMiniArmagedom : NPCdeConversa
{
    
    [SerializeField] private PainelDeEscolhaINicial pInicial;
    [SerializeField] private Transform alvoDoFim;
    [SerializeField] private EscondeCoisa escondePedra;
    [SerializeField] private GameObject[] infoLigavel;

    private EstadoDesseNpc estadoInterno = EstadoDesseNpc.emEspera;
    
    private enum EstadoDesseNpc
    {
        emEspera,
        escolha,
        cameraParaFim,
        pedraDescendo,
        confirmarEscolha
    }

    // Use this for initialization
    public override void Start(Transform T)
    {
        escondePedra.Start();
        base.Start(T);
    }

    // Update is called once per frame
    public override bool Update()
    {
        if (estadoInterno == EstadoDesseNpc.emEspera)
        {
            if (GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
            {
                return base.Update();
            }
            else
            {
                if (estado == EstadoDoNPC.conversando
                    && GameController.g.HudM.DisparaT.IndiceDaConversa == conversa.Length - 1
                    )
                {
                    //GameController.g.HudM.Botaozao.FinalizarBotao();
                    GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.qualEscolher), 30);
                    pInicial.gameObject.SetActive(true);
                    estadoInterno = EstadoDesseNpc.escolha;
                    ActionManager.ModificarAcao(GameController.g.transform, AcaoDeEscolha);
                    return UpdateInterno();
                }
                else
                    return base.Update();
            }
        }
        else
            return UpdateInterno();
    }

    public void AcaoDeEscolha()
    {
        if (pInicial.TemIndiceEscolhido && estadoInterno == EstadoDesseNpc.escolha)
        {
            pInicial.BtnEscolher(this);
            estadoInterno = EstadoDesseNpc.confirmarEscolha;
        }
    }

    bool UpdateInterno()
    {
        switch (estadoInterno)
        {
            case EstadoDesseNpc.escolha:
                int val = CommandReader.ValorDeGatilhos("HDpad",GameController.g.Manager.Control);

                if (val == 0)
                    val = CommandReader.ValorDeGatilhosTeclado("horizontal",GameController.g.Manager.Control);

                if (val != 0)
                {
                    escondePedra.Start();
                    pInicial.MudarSelecionado(val);
                }

                if (CommandReader.ButtonDown(1, GameController.g.Manager.Control))
                {
                    pInicial.BtnVoltar();
                    ActionManager.useiCancel = true;
                    GameController.g.Manager.AoHeroi();
                    GameController.g.HudM.ModoLimpo();
                    GameController.g.HudM.Painel.EsconderMensagem();
                    estadoInterno = EstadoDesseNpc.emEspera;
                    estado = EstadoDoNPC.finalizadoComBotao;
                }

                if (pInicial.TemIndiceEscolhido && CommandReader.ButtonDown(0,GameController.g.Manager.Control))
                {
                    pInicial.BtnEscolher(this);
                    estadoInterno = EstadoDesseNpc.confirmarEscolha;
                }

                if (!pInicial.gameObject.activeSelf)
                {
                   
                }
            break;
            case EstadoDesseNpc.cameraParaFim:
                if (AplicadorDeCamera.cam.FocarPonto(7.5f,12,10,true))
                {
                    escondePedra.AtivarParticula();
                    estadoInterno = EstadoDesseNpc.pedraDescendo;
                }
            break;
            case EstadoDesseNpc.pedraDescendo:
                if (escondePedra.Update())
                {
                    for (int i = 0; i < infoLigavel.Length; i++)
                        infoLigavel[i].SetActive(true);
                    EstadoFinalizado();
                    return base.Update();
                }
            break;
            
        }
        return false;
    }

    public void EstadoDeEscolha()
    {
        estadoInterno = EstadoDesseNpc.escolha;
    }

    public void EstadoFinalizado()
    {
        GameController.g.HudM.Painel.EsconderMensagem();
        FinalizaConversa();
        estadoInterno = EstadoDesseNpc.emEspera;
        GameController.g.Manager.AoHeroi();
        //return base.Update();
    }

    public void EstadoDeMostrarCaminho()
    {
        estadoInterno = EstadoDesseNpc.cameraParaFim;
        AplicadorDeCamera.cam.InicializaCameraExibicionista(alvoDoFim, 10);
    }

    protected override void FinalizaConversa()
    {
        base.FinalizaConversa();
        if (!GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
            GameController.g.HudM.ModoLimpo();
            //GameController.g.HudM.HudCriatureAtivo.container.transform.parent.gameObject.SetActive(false);
    }
}
