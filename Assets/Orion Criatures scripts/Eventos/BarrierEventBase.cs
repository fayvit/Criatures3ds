using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BarrierEventBase : EventoComGolpe
{

    [SerializeField] private int indiceDaMensagem = 0;
    [SerializeField] private bool usarForwardDoObjeto = false;
    [SerializeField] private string somDaEfetivacao;
    [SerializeField] private float tempoDeEfetivaAcao = 2.5f;
    [SerializeField] private float tempoDoFinalizaAcao = 1.75f;

    private BarrierEventsState estado = BarrierEventsState.emEspera;
    private bool jaIniciaou = false;
    private float tempoDecorrido = 0;
    private int numJaRepetidos = 0;
    private int numRepeticoesDoSom = 8;

    protected bool JaIniciaou
    {
        get { return jaIniciaou; }
        set { jaIniciaou = value; }
    }

    protected BarrierEventsState Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    protected float TempoDecorrido
    {
        get { return tempoDecorrido; }
        set { tempoDecorrido = value; }
    }

    protected bool UsarForwardDoObjeto
    {
        get { return usarForwardDoObjeto; }
    }

    public float TempoDeEfetivaAcao
    {
        get { return tempoDeEfetivaAcao; }
    }

    public float TempoDoFinalizaAcao
    {
        get { return tempoDoFinalizaAcao; }
    }

    public int NumJaRepetidos
    {
        get { return numJaRepetidos; }
        set { numJaRepetidos = value; }
    }

    protected enum BarrierEventsState
    {
        emEspera,
        mensAberta,
        ativou,
        barrasDescendo,
        apresentaFinalizaAcao
    }

    // Use this for initialization
    protected virtual void Start()
    {
        textoDoBotao = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.textoBaseDeAcao)[1];
        if (GameController.g)
        {
            if (GameController.g.MyKeys.VerificaAutoShift(ID))
            {
                gameObject.SetActive(false);
            }
            jaIniciaou = true;
        }

        SempreEstaNoTrigger();
    }

    protected void VoltarAoFLuxoDeJogo()
    {
        GameController g = GameController.g;

        g.Manager.AoHeroi();
    }

    public void AcaoDeMensAberta()
    {
        Estado = BarrierEventsState.emEspera;
        GameController.g.HudM.Painel.EsconderMensagem();

        VoltarAoFLuxoDeJogo();
    }

    public void BotaoInfo()
    {
        FluxoDeBotao();
        GameController.g.HudM.Painel.AtivarNovaMens(
            BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.barreirasDeGolpes)[indiceDaMensagem]
            , 25);
        Estado = BarrierEventsState.mensAberta;

    }

    public override void FuncaoDoBotao()
    {
        BotaoInfo();
    }

    public override void DisparaEvento(nomesGolpes nomeDoGolpe)
    {

        if (EsseGolpeAtiva(nomeDoGolpe))
            Estado = BarrierEventsState.ativou;

        if (Estado == BarrierEventsState.ativou)
        {
            FluxoDeBotao();
            TempoDecorrido = 0;
            GameController.g.MyKeys.MudaAutoShift(ID, true);
            EfetivadorDaAcao();
        }
    }

    protected void VeririqueSom(float tempoDeEfetivaAcao, string som = "")
    {
        if (string.IsNullOrEmpty(som))
            som = somDaEfetivacao;

        if (TempoDecorrido > NumJaRepetidos * (tempoDeEfetivaAcao / numRepeticoesDoSom))
        {
            EventAgregator.Publish(new StandardSendStringEvent(gameObject, som, EventKey.disparaSom));
            NumJaRepetidos++;
        }
    }

    protected abstract void EfetivadorDaAcao();
}
