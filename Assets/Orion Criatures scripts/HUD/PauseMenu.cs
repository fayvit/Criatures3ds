using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PainelStatus pStatus;
    [SerializeField] private PainelDeItens pItens;

    private bool emPause = false;
    private EstadosDePause estado = EstadosDePause.emEspera;

    private enum EstadosDePause
    {
        emEspera,
        menuAberto,
        janelaSuspensaAbertas
    }

    public bool EmPause
    {
        get { return emPause; }
    }

    public static IEnumerator VoltaTextoPause()
    {
        yield return new WaitForSecondsRealtime(2);
        if (GameController.g.HudM.MenuDePause.EmPause)
            GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.jogoPausado), 30);
    }

    void OnEnable()
    {

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (estado)
        {
            case EstadosDePause.menuAberto:
                GameController.g.HudM.Menu_Basico.MudarOpcao();

                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    EuFizUmaEscolha(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
                }

                if (ActionManager.ButtonUp(1, GameController.g.Manager.Control)
                    ||
                    ActionManager.ButtonUp(7, GameController.g.Manager.Control))
                {
                    VoltarAoJogo();
                }
                break;
        }
    }

    void EuFizUmaEscolha(int escolha)
    {
        switch (escolha)
        {
            case 0:
                BotaoCriature();
                break;
            case 1:
                if (GameController.g.Manager.Dados.Itens.Count > 0)
                    BotaoItens();
                else
                {
                    GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(
                        RetornoDoNaoTemItem,
                    BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[11]);
                    estado = EstadosDePause.janelaSuspensaAbertas;
                }
                break;
            case 2:

                break;
            case 3:
                VoltarAoTitulo();
                break;
            case 4:
                VoltarAoJogo();
                break;
            case 5:
                GameController.g.EncontroAgora();
                break;
            case 6:
                GameController.g.InimigoComUmPV();
                break;
            case 7:
                GameController.g.MeuCriatureComUmPV();
                break;
            case 8:
                GameController.g.ColocaQuatroGolpesNosCriatures();
                break;
            case 9:
                GameController.g.UmXpParaNivel();
                break;
            case 10:
                GameController.g.MeuCriatureComUZeroPE();
                break;
            case 11:
                GameController.g.TesteSave();
                break;
            case 12:
                GameController.g.CarregarSaveZero();
                break;
            case 13:
                AplicadorDeCamera.cam.usarDirecionavel = !AplicadorDeCamera.cam.usarDirecionavel;
                break;
        }

        GameController.g.HudM.Menu_Basico.FinalizarHud();
    }

    private void RetornoDoNaoTemItem()
    {
        PausarJogo();
    }

    void ReligarBotoes()
    {
        BtnsManager.ReligarBotoes(gameObject);
    }

    public void ReligarBotoesDoPainelDeItens()
    {
        pItens.AtualizaHudDeItens();
        BtnsManager.ReligarBotoes(pItens.gameObject);
        pItens.EstadoAtivo();
    }

    public void PausarJogo()
    {
        if (GameController.g.EmEstadoDeAcao() && GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
        {
            ColetorDeLixo.Coleta();
            EventAgregator.Publish(EventKey.enterInPause, null);

            GameController.g.HudM.Menu_Basico.IniciarHud(EuFizUmaEscolha,
                BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.menuDePause).ToArray()
            );

            Time.timeScale = 0;
            emPause = true;
            estado = EstadosDePause.menuAberto;

            /*
            GameController g = GameController.g;
            
            g.FinalizaHuds();
            g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.jogoPausado), 30);
             */
            //gameObject.SetActive(true);


            //g.HudM.DesligaControladores();
            //g.HudM.MenuDeI.FinalizarHud();
            //AndroidController.a.DesligarControlador();
        }

    }

    public void VoltarAoJogo()
    {
        EventAgregator.Publish(EventKey.exitPause, null);
        Time.timeScale = 1;
        emPause = false;


        estado = EstadosDePause.emEspera;

        /*
        GameController.g.HudM.Painel.EsconderMensagem();
        GameController.g.HudM.Menu_Basico.FinalizarHud();*/

        //GameController.g.HudM.ligarControladores();
        //AndroidController.a.LigarControlador();
    }

    public void BotaoCriature()
    {
        estado = EstadosDePause.janelaSuspensaAbertas;
        pStatus.gameObject.SetActive(true);
    }

    public void BotaoItens()
    {
        estado = EstadosDePause.janelaSuspensaAbertas;
        BtnsManager.DesligarBotoes(gameObject);
        pItens.Ativar(ReligarBotoes);
    }

    public void EsconderPainelDeItens()
    {
        pItens.gameObject.SetActive(false);
    }

    public void VoltarAoTitulo()
    {
        emPause = false;
        GameController.g.Salvador.SalvarAgora();
        GlobalController.g.FadeV.IniciarFadeOut();
        EventAgregator.AddListener(EventKey.fadeOutComplete, OnFadeOutComplete);
    }

    void OnFadeOutComplete(IGameEvent e)
    {
        GlobalController.g.FadeV.IniciarFadeIn();
        SceneManager.LoadScene("inicial 1");
    }

    private void OnDestroy()
    {
        EventAgregator.RemoveListener(EventKey.fadeOutComplete, OnFadeOutComplete);
    }
}
