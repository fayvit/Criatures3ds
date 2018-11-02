using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TryToLearnHud : MonoBehaviour
{
    [SerializeField] private DadosDeMiniaturas[] btns;
    [SerializeField] private Text labelDaHud;

    //private CommandReader commandR;
    private CriatureBase C;
    private nomesGolpes golpeNovo;
    private FasesDaqui fase = FasesDaqui.emEspera;
    private System.Action<bool> FinalizadoTentaAprender;
    private int opcaoEscolhida = 0;

    int indiceParaEsquecer = -1;


    private enum FasesDaqui
    {
        emEspera,
        selecionavel,
        painelSuspenso
    }

    /*
public DadoDaHudRapida[] Btns
{
   get { return btns; }
}

public Text LabelDaHud
{
   get { return labelDaHud; }
}*/

    public void Aciona(CriatureBase C, nomesGolpes golpeNovo, string txt, System.Action<bool> aprendeuOuDeixouDeAprender)
    {
        opcaoEscolhida = 0;
        FinalizadoTentaAprender = null;
        FinalizadoTentaAprender += aprendeuOuDeixouDeAprender;
        fase = FasesDaqui.selecionavel;
        gameObject.SetActive(true);
        labelDaHud.text = txt;
        
        this.C = C;
        this.golpeNovo = golpeNovo;
        GolpeBase[] meusGolpes = C.GerenteDeGolpes.meusGolpes.ToArray();
        for (int i = 0; i < meusGolpes.Length; i++)
        {
            btns[i].SetarGolpe(meusGolpes[i].Nome);
            btns[i].SetarAcao(QualGolpeEsquecer);
        }

        btns[4].SetarGolpe(golpeNovo);
        btns[4].SetarAcao(QualGolpeEsquecer);

        Destacar(0);
    }

    void Destacar(int i)
    {
        btns[i].DaSelecao.sprite = GameController.g.El.uiDestaque;
    }

    public void Finalizar()
    {
        for (int i = 0; i < btns.Length; i++)
            btns[i].LimparAcao();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (fase==FasesDaqui.selecionavel)
        {
            int val = CommandReader.ValorDeGatilhos("HDpad", GameController.g.Manager.Control);
            if (val == 0)
                val = CommandReader.ValorDeGatilhosTeclado("horizontal", GameController.g.Manager.Control);

            if (val != 0)
            {
                btns[opcaoEscolhida].DaSelecao.sprite = GameController.g.El.uiDefault;


                if (val > 0)
                {
                    if (opcaoEscolhida + val < 5)
                        opcaoEscolhida += val;
                    else
                        opcaoEscolhida = 0;
                }
                else if (val < 0)
                {
                    if (opcaoEscolhida + val >= 0)
                        opcaoEscolhida += val;
                    else
                        opcaoEscolhida = 4;
                }

                

                Destacar(opcaoEscolhida);
            }else
             if (CommandReader.ButtonDown(0, GameController.g.Manager.Control))
            {
                fase = FasesDaqui.painelSuspenso;
                QualGolpeEsquecer(opcaoEscolhida);
            }
        }
    }


    public void AcaoLocal()
    {
       // podeMudar = false;
        btns[opcaoEscolhida].FuncaoDoBotao();
    }

    // adição ao trytolearn

    
    void QualGolpeEsquecer(int indice)
    {
        /*observo que o indice é relacionado com os irmãos dentro do GameObject 
            pq originalmente foi construida para um painel responsivo
            que tinha um elemento desligado que era duplicado de acordo com o número de itens
        */

        indiceParaEsquecer = indice;

        if (indiceParaEsquecer < 4)
        {
            GameController.g.HudM.Confirmacao.AtivarPainelDeConfirmacao(EsquecerEsseGolpe, VoltarAsOpcoesDeEsquecer,
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.certezaEsquecer),
                C.GerenteDeGolpes.meusGolpes[indice].NomeEmLinguas(), GolpeBase.NomeEmLinguas(golpeNovo))
                );
        }
        else if (indiceParaEsquecer == 4)
        {
            GameController.g.HudM.Confirmacao.AtivarPainelDeConfirmacao(NaoQueroAprenderEsse, VoltarAsOpcoesDeEsquecer,
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.naoQueroAprenderEsse),
                C.NomeEmLinguas, GolpeBase.NomeEmLinguas(golpeNovo))
                );
        }
    }

    void NaoQueroAprenderEsse()
    {
        //BtnsManager.ReligarBotoes(//GameController.g.HudM.H_Tenta.gameObject);
        HudManager h = GameController.g.HudM;
        h.H_Tenta.Finalizar();
        h.P_Golpe.gameObject.SetActive(false);
        h.Painel.EsconderMensagem();
        GameController.g.StartCoroutine(MensDeGolpeNaoAprendido());
    }

    IEnumerator MensDeGolpeNaoAprendido()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        GameController.g.HudM.Painel.AtivarNovaMens(
                        string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.naoAprendeuGolpeNovo),
                        C.NomeEmLinguas,
                        GolpeBase.NomeEmLinguas(golpeNovo))
                        , 30
                        );

        FinalizadoTentaAprender(false);
        fase = FasesDaqui.emEspera;
    }

    void EsquecerEsseGolpe()
    {

        //BtnsManager.ReligarBotoes(//GameController.g.HudM.H_Tenta.gameObject);
        GameController.g.HudM.H_Tenta.Finalizar();

        GameController.g.HudM.Painel.EsconderMensagem();
        GameController.g.StartCoroutine(MensDeGolpeTrocado());
    }

    IEnumerator MensDeGolpeTrocado()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        GameController.g.HudM.Painel.AtivarNovaMens(
                        string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpeEsquecendo),
                        C.NomeEmLinguas,
                        C.GerenteDeGolpes.meusGolpes[indiceParaEsquecer].NomeEmLinguas(),
                        GolpeBase.NomeEmLinguas(golpeNovo))
                        ,26
                        );

        C.GerenteDeGolpes.meusGolpes[indiceParaEsquecer] = PegaUmGolpeG2.RetornaGolpe(golpeNovo);

        FinalizadoTentaAprender(true);
        fase = FasesDaqui.emEspera;
    }

    void VoltarAsOpcoesDeEsquecer()
    {
        fase = FasesDaqui.selecionavel;
        //ActionManager.ModificarAcao(GameController.g.transform, GameController.g.HudM.H_Tenta.AcaoLocal);
        //BtnsManager.ReligarBotoes(//GameController.g.HudM.H_Tenta.gameObject);
    }

        //BtnsManager.DesligarBotoes(//GameController.g.HudM.H_Tenta.gameObject);
}
