using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterManager : MonoBehaviour {
    [SerializeField]private CaracteristicasDeMovimentacao caracMov;
    [SerializeField]private ElementosDeMovimentacao elementos;
    [SerializeField]private MovimentacaoBasica mov;
    [SerializeField]private DadosDoPersonagem dados;
    [SerializeField]private EstadoDePersonagem estado = EstadoDePersonagem.naoIniciado;
    [SerializeField] private Controlador control = Controlador.N3DS;

    //private CommandReader comandR;
    //private BrowserController bControle = new BrowserController();
    private CreatureManager criatureAtivo;

    public bool eLoad = false;

    public DadosDoPersonagem Dados
    {
        get { return dados; }
        set { dados = value; }
    }

    public EstadoDePersonagem Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    public CreatureManager CriatureAtivo
    {
        get {
            if (criatureAtivo == null)
            {
                GameObject G = GameObject.Find("CriatureAtivo");
            //    Debug.Log("procurou Criature Ativo = " + G);
                if(G)
                    criatureAtivo = G.GetComponent<CreatureManager>();
            }
            return criatureAtivo;
        }
    }

    public MovimentacaoBasica Mov
    {
        get { return mov; }
    }

    public Controlador Control
    {
        get { return control; }
        set { control = value; }
    }

    // Use this for initialization
    void Start () {

        if (ExistenciaDoController.AgendaExiste(Start, this))
            {
            
            mov = new MovimentacaoBasica(caracMov, elementos);
            //controle = FindObjectOfType<AndroidController>();
            if (Estado == EstadoDePersonagem.naoIniciado)
            {
                dados.InicializadorDosDados();

                if(CriatureAtivo==null)
                    SeletaDeCriatures();

            }            
        }
    }

    public void SeletaDeCriatures()
    {

        if (GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
        {
            if (!eLoad)
            {
                InserirCriatureEmJogo();
                ////GameController.g.HudM.HudCriatureAtivo.container.transform.parent.gameObject.SetActive(true);
            }

            GameController.g.HudM.ModoHeroi();
        }
        else
        {
            if (CriatureAtivo)
                Destroy(CriatureAtivo.gameObject);

            GameController.g.HudM.ModoLimpo();

            ////GameController.g.HudM.HudCriatureAtivo.container.transform.parent.gameObject.SetActive(false);
            ////GameController.g.HudM.InicializaPaineisCriature(this);
        }

        

        Estado = EstadoDePersonagem.aPasseio;
    }
    
    public void InserirCriatureEmJogo()
    {
        if (dados.CriaturesAtivos.Count > 0)
        {
            GameObject G = InicializadorDoJogo.InstanciaCriature(transform, dados.CriaturesAtivos[0]);
            InicializadorDoJogo.InsereCriatureEmJogo(G, this);
            ////GameController.g.HudM.InicializaPaineisCriature(this);
        }
    }

    Vector3 dir = Vector3.zero;
    // Update is called once per frame
    void Update () {

        
        //bControle.Update();

        switch (estado)
        {
            case EstadoDePersonagem.aPasseio:
                if (!GameController.g.HudM.MenuDePause.EmPause)
                {
                    dir = CommandReader.VetorDirecao(control).sqrMagnitude == 0
                        ?
                        Vector3.zero :
                        Vector3.Lerp(dir, CommandReader.VetorDirecao(control), 5f * Time.deltaTime);
                    mov.AplicadorDeMovimentos(dir);


                    MyN3dsCommandDefines.StandardButtons();
                    //  VerificaIniciaPulo();
                }
            break;
            case EstadoDePersonagem.comMeuCriature:
            case EstadoDePersonagem.parado:
                mov.AplicadorDeMovimentos(Vector3.zero);
            break;
        }
        
	}
    public void VerificaIniciaPulo()
    {
       if (estado == EstadoDePersonagem.aPasseio && CommandReader.ButtonDown(1, (int)control))
            mov.VerificaComandoDePulo(0.01f);
    }


    public void AoCriature(CreatureManager inimigo = null)
    {
        estado = EstadoDePersonagem.comMeuCriature;
        criatureAtivo = GameObject.Find("CriatureAtivo").GetComponent<CreatureManager>();
        ////GameController.g.HudM.Btns.BotoesDoCriature(this);
        MbAlternancia.AoCriature(criatureAtivo,inimigo);
    }

    public void AoHeroi()
    {
        MbAlternancia.AoHeroi(this/*,retornaCamera*/);
       // //GameController.g.HudM.Btns.BotoesDoHeroi(this);
        estado = EstadoDePersonagem.aPasseio;
    }

    public void BotaoAlternar()
    {
        if (estado == EstadoDePersonagem.aPasseio && GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
        {
            AoCriature();
        }
        else if (estado == EstadoDePersonagem.comMeuCriature)
        {
            AoHeroi();
        }
    }

    public void BotaoAtacar()
    {
        
        if(estado==EstadoDePersonagem.comMeuCriature)
            criatureAtivo.ComandoDeAtacar();
    }

    public void IniciaPulo()
    {
        if (!caracMov.caracPulo.estouPulando && estado == EstadoDePersonagem.aPasseio)
            mov._Pulo.IniciaAplicaPulo();
        else if (estado == EstadoDePersonagem.comMeuCriature)
            criatureAtivo.IniciaPulo();
    }
}

public enum EstadoDePersonagem
{
    naoIniciado = -1,
    aPasseio,
    parado,
    comMeuCriature,    
    movimentoDeFora
}
