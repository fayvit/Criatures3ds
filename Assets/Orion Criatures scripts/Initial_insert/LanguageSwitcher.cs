using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField]private LanguageMenu languageMenu;
    [SerializeField] private Image bandeirinha;

    private EstadosDoSwitch estado = EstadosDoSwitch.emEspera;
    //private CommandReader command = new CommandReader();

    private enum EstadosDoSwitch
    {
        emEspera,
        menuSuspenso
    }

    // Use this for initialization
    void Start()
    {
        BancoDeTextos.VerificaChavesFortes(idioma.pt_br, idioma.en_google);
        bandeirinha.sprite = languageMenu.BandeirinhaAtualSelecionada();
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case EstadosDoSwitch.menuSuspenso:
                languageMenu.MudarOpcao();

                if (ActionManager.ButtonUp(0, GlobalController.g.Control))
                {
                    EventAgregator.Publish(EventKey.positiveUiInput, null);
                    Debug.Log("disparaacao");
                    OpcaoEscolhida(languageMenu.OpcaoEscolhida);
                    estado = EstadosDoSwitch.emEspera;
                }
            break;
        }
    }

    void OpcaoEscolhida(int indice)
    {
        BancoDeTextos.linguaChave = languageMenu.IdiomaNoIndice(indice);
        bandeirinha.sprite = languageMenu.BandeirinhaNoIndice(indice);
        languageMenu.FinalizarHud();
        BtnsManager.ReligarBotoes(gameObject);

        InterfaceLanguageConverter[] ilc = FindObjectsOfType<InterfaceLanguageConverter>();

        foreach (InterfaceLanguageConverter I in ilc)
        {
            I.MudaTexto();
        }

        SaveDatesForJolt.s.ChosenLanguage = BancoDeTextos.linguaChave;
        SaveAndLoadInJolt.Save();
        //new LoadAndSaveGame().SalvarArquivo("chosenLanguage.ori",heroi.linguaChave);

        InitialSceneManager.i.EstadoDeEscolhaInicial();
        InitialSceneManager.i.AtualizaLista();
        estado = EstadosDoSwitch.emEspera;
    }

    public void FuncaoDoBotao()
    {
        EventAgregator.Publish(EventKey.positiveUiInput, null);
        estado = EstadosDoSwitch.menuSuspenso;
        InitialSceneManager.i.EstadoDePainelSuspenso();
        languageMenu.IniciarHud(OpcaoEscolhida);
        BtnsManager.DesligarBotoes(gameObject);
    }
}
