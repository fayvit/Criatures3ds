using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PainelDeConfirmacao : MonoBehaviour
{
    public delegate void Confirmacao();
    public event Confirmacao botaoSim;
    public event Confirmacao botaoNao;

    [SerializeField] private Text textoDoBotaoSim;
    [SerializeField] private Text textoDoBotaoNao;
    [SerializeField] private Text textoDoPainel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CommandReader.ButtonUp(0,GlobalController.g.Control))
        {
            BotaoSim();
        }else
        if (CommandReader.ButtonUp(1, GlobalController.g.Control))
        {
            BotaoNao();
        }
    }

    void AcaoDoBotaoSim()
    {
        if (CommandReader.ButtonUp(0, GlobalController.g.Control))
        {
            BotaoSim();
        }
    }

    public void AtivarPainelDeConfirmacao(Confirmacao sim,Confirmacao nao,string textoDoPainel)
    {
        ActionManager.ModificarAcao(transform,AcaoDoBotaoSim);
        gameObject.SetActive(true);
        botaoSim += sim;
        botaoNao += nao;
        this.textoDoPainel.text = textoDoPainel;
    }

    public void AlteraTextoDoBotaoSim(string s)
    {
        textoDoBotaoSim.text = s;
    }

    public void AlteraTextoDoBotaoNao(string s)
    {
        textoDoBotaoNao.text = s;
    }

    public void AlteraTextoDoPainel(string s)
    {
        textoDoPainel.text = s;
    }

    public void AlteraTextos(string textoDoBotaoSim, string textoDoBotaoNao, string textoDoPainel)
    {
        this.textoDoPainel.text = textoDoPainel;
        this.textoDoBotaoNao.text = textoDoBotaoNao;
        this.textoDoBotaoSim.text = textoDoBotaoSim;
    }

    void LimpaBotoes()
    {
        botaoSim = null;
        botaoNao = null;
    }

    public void BotaoSim()
    {
        botaoSim();
        gameObject.SetActive(false);
        LimpaBotoes();
        EventAgregator.Publish(EventKey.positiveUiInput, null);
    }

    public void BotaoNao()
    {
        botaoNao();
        gameObject.SetActive(false);
        LimpaBotoes();
        EventAgregator.Publish(EventKey.negativeUiInput, null);
    }
}
