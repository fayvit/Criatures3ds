using UnityEngine;
using System.Collections;

[System.Serializable]
public class NPCdeConversa
{
    [SerializeField]private Transform[] pontosAlvo;
    [SerializeField]private ChaveDeTexto chaveDaConversa = ChaveDeTexto.bomDia;
    [SerializeField] private string chaveDaConversaGambiarraString = "";

    [SerializeField]protected Sprite fotoDoNPC;
    [SerializeField]private int modificarIndiceDeInicio = 0;
    [SerializeField] protected bool podeFechar = true;

    protected EstadoDoNPC estado = EstadoDoNPC.parado;
    protected string[] conversa;

    private Transform meuTransform;
    private Vector3 dirGuardada = Vector3.zero;
    private SigaOLider siga;
    
    private Vector3 alvo = Vector3.zero;
    private float tempoParado = 0.5f;
    private float contadorDeTempo = 0;
    

    private const float TEMPO_MAX_PARADO = 20;
    private const float TEMPO_MIN_PARADO = 8;
    protected enum EstadoDoNPC
    {
        caminhando,
        parado,
        conversando,
        finalizadoComBotao
    }

    /*
    protected Transform Destrutivel
    {
        get { return destrutivel; }
    }*/

    public void MudaChaveDaConversa(ChaveDeTexto chave)
    {
        conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(chave).ToArray();
    }

    protected Transform MeuTransform
    {
        get { return meuTransform; }
    }

    void OnEnable()
    {
        if(pontosAlvo!=null)
            if(pontosAlvo.Length>0)
                alvo = pontosAlvo[Random.Range(0, pontosAlvo.Length)].position;
    }

    public void SetarConversaOriginal()
    {
        if (chaveDaConversaGambiarraString != "")
        {
            try
            {
                chaveDaConversa = (ChaveDeTexto)System.Enum.Parse(typeof(ChaveDeTexto), chaveDaConversaGambiarraString);
            }
            catch (System.ArgumentException e)
            {
                Debug.LogError("string para texto invalida no enum \n"+e.StackTrace);
            }
        }

        conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(chaveDaConversa).ToArray();
    }
    
    public virtual void Start(Transform T)
    {
        
        meuTransform = T;
        SetarConversaOriginal();
        //tempoParado = Random.Range(TEMPO_MIN_PARADO, TEMPO_MAX_PARADO);

       // if (T.gameObject.name == "Derek_paladinoDeLaurense")
        //    Debug.Log(T+" : "+siga);
        //if (siga == null)
        {
            if (T.gameObject.name == "Derek_paladinoDeLaurense")
                Debug.Log(T);

            siga = new SigaOLider(T, 0.75f ,2,0.01f);
        }

        if (pontosAlvo==null)
            pontosAlvo = new Transform[1] { T };
    }

    // Update is called once per frame
    public virtual bool Update()
    {
        switch (estado)
        {
            case EstadoDoNPC.parado:
                contadorDeTempo += Time.deltaTime;
                if (contadorDeTempo > tempoParado && pontosAlvo.Length>0)
                {
                    alvo = pontosAlvo[Random.Range(0, pontosAlvo.Length)].position;
                    contadorDeTempo = 0;
                    estado = EstadoDoNPC.caminhando;
                }
            break;
            case EstadoDoNPC.caminhando:
                siga.Update(alvo);

                if (Vector3.Distance(alvo, meuTransform.position) < 2)
                {
                    siga.PareAgora();
                    estado = EstadoDoNPC.parado;
                    tempoParado = Random.Range(TEMPO_MIN_PARADO, TEMPO_MAX_PARADO);
                }
            break;
            case EstadoDoNPC.conversando:
                //AplicadorDeCamera.cam.FocarPonto(5);
                if (GameController.g.HudM.DisparaT.UpdateDeTextos(conversa, fotoDoNPC))
                {
                    FinalizaConversa();
                }
            break;
            case EstadoDoNPC.finalizadoComBotao:
                estado = EstadoDoNPC.parado;
                return true;
        }

        return false;
    }

    protected virtual void FinalizaConversa()
    {
        estado = EstadoDoNPC.finalizadoComBotao;
        meuTransform.rotation = Quaternion.LookRotation(dirGuardada);
        //MonoBehaviour.Destroy(destrutivel.gameObject);
        //GameController.g.HudM.ligarControladores();
        //GameController.g.HudM.Botaozao.FinalizarBotao();
        GameController.g.HudM.DisparaT.DesligarPaineis();
        //AndroidController.a.LigarControlador();
    }

    public virtual void IniciaConversa(/*Transform Destrutivel*/)
    {
        //destrutivel = Destrutivel;
        siga.PareAgora();

        dirGuardada = meuTransform.forward;
        meuTransform.rotation = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(GameController.g.Manager.transform.position - meuTransform.position, Vector3.up)
            );
        GameController.g.HudM.DisparaT.IniciarDisparadorDeTextos(podeFechar);
        GameController.g.HudM.DisparaT.IndiceDaConversa = modificarIndiceDeInicio;
        //GameController.g.HudM.Botaozao.IniciarBotao(FinalizaConversa, 
         //   bancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.ObrigadoComPressa)
            //);
        estado = EstadoDoNPC.conversando;
    }
}
