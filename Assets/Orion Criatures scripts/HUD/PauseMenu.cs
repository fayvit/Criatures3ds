using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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

    public bool EmPause {
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
                    Debug.Log("disparaacao");
                    EuFizUmaEscolha(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
                }

                if (ActionManager.ButtonUp(1, GameController.g.Manager.Control)
                    ||
                    ActionManager.ButtonUp(7, GameController.g.Manager.Control))
                {
                    Debug.Log("disparacancel");
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
                BotaoItens();
            break;
            case 2:

            break;
            case 3:
                VoltarAoTitulo();
            break;
            case 4:
                VoltarAoJogo();
            break;
        }

        GameController.g.HudM.Menu_Basico.FinalizarHud();
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
            GameController g = GameController.g;
            g.FinalizaHuds();
            g.HudM.Menu_Basico.IniciarHud(EuFizUmaEscolha, 
                BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.menuDePause).ToArray()
            );
            //gameObject.SetActive(true);
            Time.timeScale = 0;
            emPause = true;
            estado = EstadosDePause.menuAberto;
            g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.jogoPausado), 30);
            //g.HudM.DesligaControladores();
            //g.HudM.MenuDeI.FinalizarHud();
            //AndroidController.a.DesligarControlador();
        }
        
    }

    public void VoltarAoJogo()
    {
        
        Time.timeScale = 1;
        emPause = false;
        
        GameController.g.HudM.Painel.EsconderMensagem();
        estado = EstadosDePause.emEspera;
        GameController.g.HudM.Menu_Basico.FinalizarHud();
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
