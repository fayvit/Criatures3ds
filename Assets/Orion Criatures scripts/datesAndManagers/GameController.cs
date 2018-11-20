using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController g;
    private CharacterManager manager;
    private MbUsoDeItem usoDeItens;
    private ReplaceManager replace;
    private KeyVar myKeys = new KeyVar();
    private SaveManager salvador = new SaveManager();
    private ImageMenuN3ds imgMenu;
    
    [SerializeField]private ContainerDosStatus contStatus = new ContainerDosStatus();

    [SerializeField] private bool comCriature = false;
    [SerializeField]private ElementosDoJogo el;    
    [SerializeField]private MbEncontros encontros;
    [SerializeField] private HudManager hudManager;
    [SerializeField] private UsarTempoDeItem usarTempoDeItem = UsarTempoDeItem.emLuta;

    public UsarTempoDeItem UsarTempoDeItem
    {
        get { return usarTempoDeItem; }
    }

    public bool ComCriature
    {
        get { return ComCriature; }
        set { comCriature = value;
            MyKeys.MudaShift(KeyShift.estouNoTuto, comCriature);
        }
    }

    public bool UsandoItemOuTrocandoCriature
    {
        get { return 
                (usoDeItens == null ? false : usoDeItens.EstouUsandoItem) 
                || 
                (replace == null ? false : replace.EstouTrocandoDeCriature);
        }
    }

    public bool ContraTreinador
    {
        get { return encontros.ContaTreinador; }
    }

    public bool estaEmLuta
    {
        get { return encontros.Luta; }
    }

    public bool ContarPassos
    {
        get { return encontros.ContarPassos; }
        set { encontros.ContarPassos = value; }
    }

    public SaveManager Salvador
    {
        get { return salvador; }
    }
    /*
    public HudManager HudM
    {
        get { return hudM; }
    }*/

    public CreatureManager InimigoAtivo
    {
        get { return encontros.InimigoAtivo; }
    }

    public CharacterManager Manager
    {
        get {
            VerificaSetarManager();
            return manager;
        }
    }

    public KeyVar MyKeys
    {
        get { return myKeys; }
        set { myKeys = value; }
    }

    public ElementosDoJogo El
    {
        get { return el; }
    }

    public HudManager HudM
    {
        get { return hudManager; }
        set { hudManager = value; }
    }

    public ContainerDosStatus ContStatus
    {
        get { return contStatus; }
    }

    public void FinalizaHuds()
    {
        imgMenu.Esconde();
        HudM.Shoulder.ChangeL(false);
        HudM.Shoulder.ChangeR(false);
    }

    /*
    private void OnApplicationFocus(bool focus)
    {
        if(focus)
            Cursor.lockState = CursorLockMode.Locked;
    }*/

    // Use this for initialization
    void Start()
    {
        
        // BancoDeTextos.VerificaChavesFortes(idioma.pt_br, idioma.en_google);
        g = this;
       // Cursor.lockState = CursorLockMode.Locked;
        //Para ajudar a programar o tuto

        MyKeys.MudaShift(KeyShift.estouNoTuto, comCriature);

        /*************************************************/

        
        usoDeItens = new MbUsoDeItem();
        VerificaSetarManager();
        encontros.Start();
        HudM.StatusHud.Start();
        imgMenu = FindObjectOfType<ImageMenuN3ds>();//Instantiate((GameObject)Resources.Load("HUD_Itens")).GetComponent<ImageMenu>();

        EventAgregator.AddListener(EventKey.returnForFreeAfterFight, EventoSalvador);

        ModificacoesDaCena();
        
    }

    
    public void ModificacoesDaCena()
    {
        string nomeCena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if ( nomeCena!= "CenaDeCarregamento" && nomeCena != "comunsDeFase")
        {
            
            SceneConfigs sc = GetSceneConfigs.Get(StringParaEnum.ObterEnum<NomesCenas>(nomeCena));
            Camera.main.backgroundColor = sc.CamColor;
            Debug.Log(sc.SceneMusic);
            GlobalController.g.Musica.IniciarMusica(LocalResources.l.Mlocal.Musica, LocalResources.l.Mlocal.Volume);
        }
    }


    private void OnDestroy()
    {
        EventAgregator.RemoveListener(EventKey.returnForFreeAfterFight, EventoSalvador);
    }

    void EventoSalvador(IGameEvent e)
    {
        Salvador.SalvarAgora();
    }

    // Update is called once per frame
    void Update()
    {
        usoDeItens.Update();
        encontros.Update();
        //HudM.MenuDeI.Update();
        HudM.Shop_Manager.Update();
        HudM.StatusHud.Update();
        ContStatus.Update();
        

        if (manager.Estado == EstadoDePersonagem.aPasseio)
        {
            salvador.Update();
        }

        if (replace != null)
            if (replace.Update())
            {
                RetornoDeReplace();
            }

        if (EmEstadoDeAcao(true))
        {
            if (CommandReader.ButtonDown(4, manager.Control)&&!CommandReader.ButtonDown(5, manager.Control))
                HudM.Shoulder.ChangeL(true);
            if (CommandReader.ButtonDown(5, manager.Control)&&!CommandReader.ButtonDown(4, manager.Control))
                HudM.Shoulder.ChangeR(true);
            if (CommandReader.ButtonUp(4, manager.Control))
                HudM.Shoulder.ChangeL(false);
            if (CommandReader.ButtonUp(5, manager.Control))
                HudM.Shoulder.ChangeR(false);
        }
    }

    void RetornoDeReplace()
    {

        if (replace.Fluxo == FluxoDeRetorno.criature || replace.Fluxo == FluxoDeRetorno.menuCriature)
        {
            if (estaEmLuta)
                encontros.InimigoAtivo.Estado = CreatureManager.CreatureState.selvagem;

            manager.AoCriature(encontros.InimigoAtivo);

        }

        if (replace.Fluxo == FluxoDeRetorno.menuCriature || replace.Fluxo == FluxoDeRetorno.menuHeroi)
        {
            HudM.MenuDePause.PausarJogo();
            HudM.MenuDePause.BotaoCriature();
            HudM.Menu_Basico.FinalizarHud();
        }

        replace = null;
        HudM.AtualizaDadosDaHudVida(false);
        //HudM.AtualizaHudHeroi(manager.CriatureAtivo.MeuCriatureBase);
    }

    void VerificaSetarManager()
    {
        if(manager==null)
            manager = FindObjectOfType<CharacterManager>();
    }

    public void BotaoPulo()
    {
        Manager.IniciaPulo(); 
    }

    public void BotaoAlternar()
    {
        imgMenu.Esconde();
        Manager.BotaoAlternar();
    }

    public void BotaoAtaque()
    {
        Manager.BotaoAtacar();
    }

    bool PodeAbrirMenuDeImagem()
    {
        if (manager.Estado != EstadoDePersonagem.aPasseio)
            if (manager.CriatureAtivo)
            {
                if (manager.CriatureAtivo.Estado == CreatureManager.CreatureState.parado
                    ||
                    manager.CriatureAtivo.Estado == CreatureManager.CreatureState.morto)
                    return false;
            }
            else
                return false;


        if (usoDeItens.EstouUsandoItem)
            return false;

        if (replace != null)
            return !replace.EstouTrocandoDeCriature;

        return true;
    }

    public void BotaoMaisAtaques(int i)
    {
        if (PodeAbrirMenuDeImagem())
        {
            VerificaSetarManager();
            //hudM.MenuDeI.IniciarHud(manager.Dados, TipoDeDado.golpe, manager.Dados.CriaturesAtivos[0].GerenteDeGolpes.meusGolpes.Count,

            GerenciadorDeGolpes gg = manager.Dados.CriaturesAtivos[0].GerenteDeGolpes;
            if (i > 0)
            {
                if (gg.meusGolpes.Count > gg.golpeEscolhido + i)
                    gg.golpeEscolhido += i;
                else gg.golpeEscolhido = 0;
            }
            else if (i < 0)
            {
                if (gg.golpeEscolhido + i >= 0)
                    gg.golpeEscolhido += i;
                else
                    gg.golpeEscolhido = gg.meusGolpes.Count - 1;
            }
            
            imgMenu.Acionada(TipoHud.golpes);
            HudM.AtualizeImagemDeAtivos();
                    //hudM.MenuDeI.FinalizarHud();
                    //hudM.Btns.ImagemDoAtaque(manager);
                    //},5
                    //);

        }
    }

    public void RetornarParaFluxoDoHeroi(bool treinador = false)
    {
        encontros.FinalizaEncontro(treinador);
        usoDeItens.FinalizaUsaItemDeFora();
    }

    public void BotaUsarItem()
    {
        if (Time.time > GameController.g.Manager.Dados.TempoDoUltimoUsoDeItem + MbItens.INTERVALO_DO_USO_DE_ITEM)
        {
            FinalizaHuds();
            usoDeItens.Start(manager,
                manager.Estado == EstadoDePersonagem.comMeuCriature ?
                FluxoDeRetorno.criature :
                FluxoDeRetorno.heroi
                );
        }
        else {
            HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.itemEmTempoDeRecarga),25,5);
        }
    }

    public void BotaTrocarCriature()
    {
        DadosDoPersonagem dados = manager.Dados;
        if (dados.CriaturesAtivos[dados.CriatureSai + 1].CaracCriature.meusAtributos.PV.Corrente > 0)
        {
            FinalizaHuds();
            EventAgregator.Publish(new StandardSendStringEvent(gameObject, "chamadaParaAcao", EventKey.disparaSom));
            replace = new ReplaceManager(manager, manager.CriatureAtivo.transform,
                manager.Estado == EstadoDePersonagem.comMeuCriature ?
                FluxoDeRetorno.criature :
                FluxoDeRetorno.heroi
                );
        }
        else
            HudM.Painel.AtivarNovaMens(
                string.Format(
                    BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.criatureParaMostrador)[1],
                    dados.CriaturesAtivos[dados.CriatureSai+1].NomeEmLinguas
                    ),25,5
                );
    }


    public void BotaItens(int i)
    {
        DadosDoPersonagem dados = GameController.g.manager.Dados;
        if (PodeAbrirMenuDeImagem() && dados.Itens.Count>0)
        {
            VerificaSetarManager();
            
            if (i > 0)
            {
                
                if (dados.Itens.Count>dados.itemSai+i)
                    dados.itemSai += i;
                else dados.itemSai = 0;
            }
            else if (i < 0)
            {
                if (dados.itemSai + i >= 0)
                    dados.itemSai += i;
                else
                    dados.itemSai = dados.Itens.Count - 1;
            }
            imgMenu.Acionada(TipoHud.items);
            HudM.AtualizeImagemDeAtivos();
            //hudM.MenuDeI.IniciarHud(manager.Dados, TipoDeDado.item, manager.Dados.Itens.Count,FuncaoDoUseiItem, 15);
        }
    }

    
    public void BotaoMaisCriature(int i)
    {
        if (PodeAbrirMenuDeImagem())
        {
            VerificaSetarManager();
            DadosDoPersonagem dados = manager.Dados;
            if (dados.CriaturesAtivos.Count-1 > dados.CriatureSai + i)
                dados.CriatureSai += i;
            else dados.CriatureSai = 0;

            imgMenu.Acionada(TipoHud.criatures);
            HudM.AtualizeImagemDeAtivos();
        }
    }

    public bool EmEstadoDeAcao(bool chao = false)
    {
        bool foi = false;
        EstadoDePersonagem estadoP = Manager.Estado;

        if (estadoP == EstadoDePersonagem.aPasseio && !chao)
            chao = Manager.Mov.NoChao(0.1f);

        if (myKeys.VerificaAutoShift(KeyShift.estouNoTuto)&&manager.CriatureAtivo!=null)
        {
            CreatureManager.CreatureState estadoC = manager.CriatureAtivo.Estado;

            if (estadoP == EstadoDePersonagem.comMeuCriature && !chao)
                chao = Manager.CriatureAtivo.Mov.NoChao(Manager.CriatureAtivo.MeuCriatureBase.CaracCriature.distanciaFundamentadora);
            
            if (estadoP == EstadoDePersonagem.comMeuCriature &&
                chao &&
                (estadoC == CreatureManager.CreatureState.emLuta
                || estadoC == CreatureManager.CreatureState.aPasseio)
                )
                foi = true;
        }

        if (estadoP == EstadoDePersonagem.aPasseio && chao)
            foi = true;

        return foi;
    }

    void FuncaoTrocarCriatureSemMenu(int indice)
    {
        if (Manager.Dados.CriaturesAtivos[indice + 1].CaracCriature.meusAtributos.PV.Corrente > 0)
        {
            FluxoDeRetorno fluxo = manager.Estado == EstadoDePersonagem.comMeuCriature ? FluxoDeRetorno.criature : FluxoDeRetorno.heroi;
            FuncaoTrocarCriature(indice + 1, fluxo);
        }
        else
        {
            GameController.g.HudM.Painel.AtivarNovaMens(string.Format(
                BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.criatureParaMostrador)[1],
                Manager.Dados.CriaturesAtivos[indice + 1].NomeEmLinguas
                ),30,5);
            GameController.g.FinalizaHuds();
        }
    }

    public void FuncaoTrocarCriature(int indice, FluxoDeRetorno fluxo, bool bugDoTesteChao = false)
    {
        if (EmEstadoDeAcao(bugDoTesteChao))
        {
            if (estaEmLuta)
            {
                encontros.InimigoAtivo.PararCriatureNoLocal();
            }

            manager.Dados.CriatureSai = indice;
            
            replace = new ReplaceManager(manager, manager.CriatureAtivo.transform, fluxo);
        }

    }

    public void FuncaoDoUseiItem(int indice, FluxoDeRetorno fluxo)
    {
        if (EmEstadoDeAcao())
        {
            if (!usoDeItens.EstouUsandoItem)
            {
                manager.Dados.itemSai = indice;

               // hudM.MenuDeI.FinalizarHud();

                usoDeItens.Start(manager,fluxo);

                if(fluxo!=FluxoDeRetorno.menuCriature && fluxo!=FluxoDeRetorno.menuHeroi)
                    manager.Estado = EstadoDePersonagem.parado;

            }
        }
    }

    void FuncaoDoUseiItem(int indice)
    {
        FluxoDeRetorno fluxo = manager.Estado == EstadoDePersonagem.comMeuCriature
                    ? FluxoDeRetorno.criature
                    : FluxoDeRetorno.heroi;

        FuncaoDoUseiItem(indice, fluxo);
    }

    public void ReiniciarContadorDeEncontro()
    {
        encontros.ZeraPosAnterior();
    }

    public static void EntrarNoFluxoDeTexto()
    {
        HudManager hudM = g.HudM;
        //AndroidController.a.DesligarControlador();
        //hudM.DesligaControladores();
        g.imgMenu.Esconde();

        ColetorDeLixo.Coleta();
        g.Manager.Estado = EstadoDePersonagem.parado;
    }

    public void EncontroAgoraCom(CreatureManager c,bool treinador = false,string nomeTreinador="")
    {
        encontros.IniciarEncontroCom(c,treinador,nomeTreinador);
    }

    #region botões de teste
    public void EncontroAgora()
    {
        encontros.ZerarPassosParaProxEncontro();
    }

    public void InimigoComUmPV()
    {
        encontros.ColocarUmPvNoInimigo();
        HudM.AtualizaDadosDaHudVida(true);
    }

    public void MeuCriatureComUmPV()
    {
        Manager.Dados.CriaturesAtivos[0].CaracCriature.meusAtributos.PV.Corrente = 1;
        HudM.AtualizaDadosDaHudVida(false);
    }
    public void MeuCriatureComUZeroPE()
    {
        Manager.Dados.CriaturesAtivos[0].CaracCriature.meusAtributos.PE.Corrente = 0;
        HudM.AtualizaDadosDaHudVida(false);
    }

    public void UmXpParaNivel()
    {
        IGerenciadorDeExperiencia gXP = Manager.Dados.CriaturesAtivos[0].CaracCriature.mNivel;
        gXP.XP = gXP.ParaProxNivel - 1;
    }

    public void TesteSave()
    {
        salvador.SalvarAgora();
    }

    public void ColocaQuatroGolpesNosCriatures()
    {
        CriatureBase[] Cs = Manager.Dados.CriaturesAtivos.ToArray();

        for (int i = 0; i < Cs.Length; i++)
        {
            GolpeBase duplicado = Cs[i].GerenteDeGolpes.meusGolpes[0];
            while (Cs[i].GerenteDeGolpes.meusGolpes.Count< 4)
            {
                Cs[i].GerenteDeGolpes.meusGolpes.Add(duplicado);
            }
        }
    }

    public void InsereProps()
    {
        //FindObjectOfType<PropsPorScript>().Insere();
    }

    public void CarregarSaveZero()
    {
        GameObject G = new GameObject();
        SceneLoader loadScene = G.AddComponent<SceneLoader>();
        loadScene.CenaDoCarregamento(0);
    }
    #endregion
}

public enum UsarTempoDeItem
{
    sempre,
    emLuta,
    nunca
}