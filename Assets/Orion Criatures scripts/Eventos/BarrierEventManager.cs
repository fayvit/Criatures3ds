using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]
public class BarrierEventManager : EventoComGolpe
{
    [Space(5)]
    [SerializeField] private GameObject barreira;
    [SerializeField] private GameObject acaoEfetivada;
    [SerializeField] private GameObject finalizaAcao;


    [SerializeField] private int indiceDaMensagem = 0;
    [SerializeField] private bool usarForwardDoObjeto = false;
    [SerializeField] private string somDaEfetivacao;
    [SerializeField] private string somDaFinalizacao;

    private BarrierEventsState estado = BarrierEventsState.emEspera;
    private bool jaIniciaou = false;
    private float tempoDecorrido = 0;
    private float tempoDeEfetivaAcao = 2.5f;
    private float tempoDoFinalizaAcao = 1.75f;
    private int numJaRepetidos = 0;
    private int numRepeticoesDoSom = 8;

    private enum BarrierEventsState
    {
        emEspera,
        mensAberta,
        ativou,
        apresentaFinalizaAcao
    }

    // Use this for initialization
    void Start()
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

    void VoltarAoFLuxoDeJogo()
    {
        GameController g = GameController.g;
        //AndroidController.a.LigarControlador();

        g.Manager.AoHeroi();
        //g.HudM.ligarControladores();
    }

    public void AcaoDeMensAberta()
    {
        estado = BarrierEventsState.emEspera;
        GameController.g.HudM.Painel.EsconderMensagem();

        VoltarAoFLuxoDeJogo();
        //ButtonsViewsManager.anularAcao = true;
    }

    private void OnValidate()
    {
        BuscadorDeID.Validate(ref ID, this);
    }

    new void Update()
    {
        if (jaIniciaou)
        {
            switch (estado)
            {
                case BarrierEventsState.mensAberta:
                    if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                    {
                        AcaoDeMensAberta();
                    }
                    break;
                case BarrierEventsState.ativou:
                    tempoDecorrido += Time.deltaTime;
                    VeririqueSom();
                    if (tempoDecorrido > tempoDeEfetivaAcao)
                    {
                        tempoDecorrido = 0;
                        finalizaAcao.SetActive(true);
                        barreira.SetActive(false);
                        estado = BarrierEventsState.apresentaFinalizaAcao;
                        EventAgregator.Publish(new StandardSendStringEvent(gameObject, somDaFinalizacao, EventKey.disparaSom));
                    }
                    break;
                case BarrierEventsState.apresentaFinalizaAcao:
                    tempoDecorrido += Time.deltaTime;
                    if (tempoDecorrido > tempoDoFinalizaAcao)
                    {
                        gameObject.SetActive(false);
                        VoltarAoFLuxoDeJogo();
                    }
                    break;
            }
            base.Update();
        }
        else
        {
            //  if (Application.isEditor)
            //    Chave = BuscadorDeID.GetUniqueID(gameObject) + "_" + gameObject.scene.name;
            Start();
        }
    }

    void VeririqueSom()
    {
        if (tempoDecorrido > numJaRepetidos * (tempoDeEfetivaAcao / numRepeticoesDoSom))
        {
            EventAgregator.Publish(new StandardSendStringEvent(gameObject, somDaEfetivacao, EventKey.disparaSom));
            numJaRepetidos++;
        }
    }

    public override void DisparaEvento(nomesGolpes nomeDoGolpe)
    {
        Debug.Log(nomeDoGolpe + " : " + GameController.g.MyKeys.VerificaAutoShift(ID));

        if (EsseGolpeAtiva(nomeDoGolpe))
            estado = BarrierEventsState.ativou;



        if (estado == BarrierEventsState.ativou)
        {
            FluxoDeBotao();
            acaoEfetivada.SetActive(true);
            tempoDecorrido = 0;
            GameController.g.MyKeys.MudaAutoShift(ID, true);
            //GameController.g.MyKeys.MudaShift(ChaveEspecial, true);
            AplicadorDeCamera.cam.NovoFocoBasico(transform, 10, 10, true, usarForwardDoObjeto);
        }
    }

    public void BotaoInfo()
    {
        FluxoDeBotao();
        GameController.g.HudM.Painel.AtivarNovaMens(
            BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.barreirasDeGolpes)[indiceDaMensagem]
            , 25);
        estado = BarrierEventsState.mensAberta;

        //ActionManager.ModificarAcao(GameController.g.transform,AcaoDeMensAberta);

    }

    public override void FuncaoDoBotao()
    {
        BotaoInfo();
    }
}
