using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]
public class BarrierEventManager : EventoComGolpe
{    
    [Space(5)]
    [SerializeField]private GameObject barreira;
    [SerializeField]private GameObject acaoEfetivada;
    [SerializeField]private GameObject finalizaAcao;
    
    
    [SerializeField]private int indiceDaMensagem = 0;
    [SerializeField] private bool usarForwardDoObjeto = false;

    private BarrierEventsState estado = BarrierEventsState.emEspera;
    private bool jaIniciaou = false;
    private float tempoDecorrido = 0;
    private float tempoDeEfetivaAcao = 2.5f;
    private float tempoDoFinalizaAcao = 1.75f;

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
            if (GameController.g.MyKeys.VerificaAutoShift(Chave))
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
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(Chave)&& gameObject.scene.name != null)
        {
            // ID = BuscadorDeID.GetUniqueID(gameObject, ID.ToString());

            Chave = GetInstanceID() + "_" + gameObject.scene.name + "_Barreira";
            //ID = System.Guid.NewGuid().ToString();
            BuscadorDeID.SetUniqueIdProperty(this, Chave, "chave");
        }
#endif
    }

    new void Update()
    {
        if (jaIniciaou)
        {
            switch (estado)
            {
                case BarrierEventsState.mensAberta:

                    break;
                case BarrierEventsState.ativou:
                    tempoDecorrido += Time.deltaTime;
                    if (tempoDecorrido > tempoDeEfetivaAcao)
                    {
                        tempoDecorrido = 0;
                        finalizaAcao.SetActive(true);
                        barreira.SetActive(false);
                        estado = BarrierEventsState.apresentaFinalizaAcao;
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

    public override void DisparaEvento(nomesGolpes nomeDoGolpe)
    {
        Debug.Log(nomeDoGolpe+" : "+ GameController.g.MyKeys.VerificaAutoShift(Chave)+" : "+
        GameController.g.MyKeys.VerificaAutoShift(ChaveEspecial));

        if (EsseGolpeAtiva(nomeDoGolpe))        
            estado = BarrierEventsState.ativou;
        
        

        if (estado == BarrierEventsState.ativou)
        {
            FluxoDeBotao();
            acaoEfetivada.SetActive(true);
            tempoDecorrido = 0;
            GameController.g.MyKeys.MudaAutoShift(Chave, true);
            GameController.g.MyKeys.MudaShift(ChaveEspecial, true);
            AplicadorDeCamera.cam.NovoFocoBasico(transform,10,10,true,usarForwardDoObjeto);
        }
    }

    public void BotaoInfo()
    {
        FluxoDeBotao();
        GameController.g.HudM.Painel.AtivarNovaMens(
            BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.barreirasDeGolpes)[indiceDaMensagem]
            , 25);
        estado = BarrierEventsState.mensAberta;

        ActionManager.ModificarAcao(GameController.g.transform,AcaoDeMensAberta);
        
    }

    public override void FuncaoDoBotao()
    {
        BotaoInfo();
    }
}
