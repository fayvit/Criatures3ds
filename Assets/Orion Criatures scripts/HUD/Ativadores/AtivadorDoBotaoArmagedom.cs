using UnityEngine;
using System.Collections;

public class AtivadorDoBotaoArmagedom : AtivadorDeBotao
{
    [SerializeField]private Sprite fotoDoNPC;     
    [SerializeField]private IndiceDeArmagedoms indiceDesseArmagedom = IndiceDeArmagedoms.daCavernaInicial;

    private fasesDoArmagedom fase = fasesDoArmagedom.emEspera;
    private DisparaTexto dispara;
    private ReplaceManager replace;
    private int indiceDoSubstituido = -1;
    private float tempoDecorrido = 0;
    private string tempString;
    private string[] t;
    private string[] frasesDeArmagedom = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.frasesDeArmagedom).ToArray();
    private string[] txtDeOpcoes = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.menuDeArmagedom).ToArray();

    private enum fasesDoArmagedom
    {
        emEspera,
        mensInicial,
        escolhaInicial,
        curando,
        fraseQueAntecedePossoAjudar,
        armagedadosAberto,
        fazendoUmaTroca,
        mensDetrocaAberta,
        escolhaDePergaminho,
        vendendoPergaminho
    }

    private const float TEMPO_DE_CURA = 2.5F;
    void Start()
    {
        textoDoBotao = BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.textoBaseDeAcao);
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            dispara = GameController.g.HudM.DisparaT;
            t = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.primeiroArmagedom).ToArray();
        }
    }

    new void Update()
    {
        base.Update();

        switch (fase)
        {
            case fasesDoArmagedom.mensInicial:
                AplicadorDeCamera.cam.FocarPonto(2, 8,-1,true);
                if (dispara.UpdateDeTextos(t, fotoDoNPC)
                    ||
                    dispara.IndiceDaConversa > t.Length - 2
                    )
                {
                    EntraFrasePossoAjudar();
                    LigarMenu();
                }
            break;
            case fasesDoArmagedom.escolhaInicial:
                AplicadorDeCamera.cam.FocarPonto(2, 8, -1, true);
                if (!dispara.LendoMensagemAteOCheia())
                    GameController.g.HudM.Menu_Basico.MudarOpcao();

                if (Input.GetButtonDown("Cancel"))
                {
                    ActionManager.useiCancel = true;
                    OpcaoEscolhida(txtDeOpcoes.Length - 1);
                }
            break;
            case fasesDoArmagedom.curando:

                tempoDecorrido += Time.deltaTime;
                if (tempoDecorrido > TEMPO_DE_CURA || Input.GetButtonDown("Acao"))
                {
                    fase = fasesDoArmagedom.fraseQueAntecedePossoAjudar;
                    dispara.ReligarPaineis();
                    dispara.Dispara(frasesDeArmagedom[0], fotoDoNPC);
                }
            break;
            case fasesDoArmagedom.fraseQueAntecedePossoAjudar:
                if (!dispara.LendoMensagemAteOCheia())
                {
                    ActionManager.ModificarAcao(GameController.g.transform, () =>
                     {
                         LigarMenu();
                         EntraFrasePossoAjudar();
                     });

                    fase = fasesDoArmagedom.emEspera;
                }
            break;
            case fasesDoArmagedom.armagedadosAberto:
                if (!dispara.LendoMensagemAteOCheia())
                    GameController.g.HudM.EntraCriatures.MudarOpcao();

                if (Input.GetButtonDown("Cancel"))
                {
                    ActionManager.useiCancel = true;
                    GameController.g.HudM.EntraCriatures.FinalizarHud();
                    GameController.g.HudM.Painel.EsconderMensagem();
                    LigarMenu();
                    EntraFrasePossoAjudar();
                }
            break;
            case fasesDoArmagedom.fazendoUmaTroca:
                if (replace.Update())
                {
                    GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(() => {
                        VoltarDoEntraArmagedado();
                        fase = fasesDoArmagedom.escolhaInicial;
                    }, tempString);
                    AplicadorDeCamera.cam.InicializaCameraExibicionista(transform, 1);
                    fase = fasesDoArmagedom.mensDetrocaAberta;
                    GameController.g.Manager.Dados.CriatureSai = 0;
                }
            break;
            case fasesDoArmagedom.escolhaDePergaminho:
                AplicadorDeCamera.cam.FocarPonto(2, 8, -1, true);
                if (!dispara.LendoMensagemAteOCheia())
                    GameController.g.HudM.Menu_Basico.MudarOpcao();

                if (Input.GetButtonDown("Cancel"))
                {
                    ActionManager.useiCancel = true;
                    EscolhaDeComprarPergaminho(1);
                }
            break;
            case fasesDoArmagedom.vendendoPergaminho:
                if (!GameController.g.HudM.PainelQuantidades.gameObject.activeSelf)
                {
                    EntraFrasePossoAjudar();
                    LigarMenu();
                }    
            break;
        }
    }

    void LigarMenu()
    {
        GameController.g.HudM.Menu_Basico.IniciarHud(OpcaoEscolhida, txtDeOpcoes);
    }

    void OpcaoEscolhida(int opcao)
    {
        ActionManager.ModificarAcao(GameController.g.transform, () => { });
        Debug.Log("ola");
        GameController.g.HudM.Menu_Basico.FinalizarHud();

        switch (opcao)
        {
            case 0:
                Curar();
            break;
            case 1:
                CriaturesArmagedados();
            break;
            case 2:
                ComprarPergaminhos();
            break;
            case 3:
                VoltarAoJogo();
            break;
            
        }
    }

    void ComprarPergaminhos()
    {
        dispara.ReligarPaineis();
        dispara.Dispara(string.Format(frasesDeArmagedom[8],new MbPergaminhoDeArmagedom().Valor.ToString()), fotoDoNPC);
        GameController.g.HudM.Menu_Basico.IniciarHud(EscolhaDeComprarPergaminho, 
            BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.simOuNao).ToArray());
        fase = fasesDoArmagedom.escolhaDePergaminho;

        ActionManager.ModificarAcao(
            GameController.g.transform,
            () => { EscolhaDeComprarPergaminho(GameController.g.HudM.Menu_Basico.OpcaoEscolhida); }
            );
    }

    void EscolhaDeComprarPergaminho(int escolha)
    {
        GameController.g.HudM.Menu_Basico.FinalizarHud();
        switch (escolha)
        {
            case 0:
                GameController.g.HudM.PainelQuantidades.IniciarEssaHud(PegaUmItem.Retorna(nomeIDitem.pergArmagedom));
            break;
            case 1:
                LigarMenu();
                EntraFrasePossoAjudar();
            break;
        }

        
    }

    public void CriaturesArmagedados()
    {
        GameController g = GameController.g;
        //ApagarMenu();
        dispara.DesligarPaineis();
        CriatureBase[] armagedados = g.Manager.Dados.CriaturesArmagedados.ToArray();
        if (armagedados.Length > 0)
        {
            g.HudM.EntraCriatures.IniciarEssaHUD(armagedados, AoEscolherumCriature);
            GameController.g.HudM.Painel.AtivarNovaMens(frasesDeArmagedom[2], 30);
            fase = fasesDoArmagedom.armagedadosAberto;

            ActionManager.ModificarAcao(GameController.g.transform, () => {
                AoEscolherumCriature(GameController.g.HudM.EntraCriatures.OpcaoEscolhida);
            });
        }
        else
        {
            dispara.DesligarPaineis();
            dispara.ReligarPaineis();
            dispara.Dispara(frasesDeArmagedom[1], fotoDoNPC);
            fase = fasesDoArmagedom.fraseQueAntecedePossoAjudar;
        }
    }

    public void VoltarDoEntraArmagedado()
    {
        LigarMenu();
        EntraFrasePossoAjudar();
        GameController.g.HudM.EntraCriatures.FinalizarHud();
        GameController.g.HudM.Painel.EsconderMensagem();
    }

    void AoEscolherumCriature(int indice)
    {

        GameController g = GameController.g;
        DadosDoPersonagem dados = g.Manager.Dados;
        HudManager hudM = g.HudM;
        if (dados.CriaturesAtivos.Count < dados.maxCarregaveis)
        {
            CriatureBase C = dados.CriaturesArmagedados[indice];
            hudM.UmaMensagem.ConstroiPainelUmaMensagem(VoltarDoEntraArmagedado,
                string.Format(frasesDeArmagedom[3], C.NomeEmLinguas, C.CaracCriature.mNivel.Nivel)
                );
            dados.CriaturesArmagedados.Remove(C);
            dados.CriaturesAtivos.Add(C);
        }
        else
        {
            CriatureBase C = dados.CriaturesArmagedados[indice];
            Debug.Log(indice);
            indiceDoSubstituido = indice;
            Debug.Log(indiceDoSubstituido);
            hudM.UmaMensagem.ConstroiPainelUmaMensagem(MostraOsQueSaem,
                string.Format(frasesDeArmagedom[4], C.NomeEmLinguas, C.CaracCriature.mNivel.Nivel)
                );
            GameController.g.HudM.EntraCriatures.FinalizarHud();
        }

    }

    void SubstituiArmagedado(int indice)
    {
        GameController g = GameController.g;
        DadosDoPersonagem dados = g.Manager.Dados;
        Debug.Log(indiceDoSubstituido);
        CriatureBase temp = dados.CriaturesArmagedados[indiceDoSubstituido];

        dados.CriaturesArmagedados[indiceDoSubstituido] = dados.CriaturesAtivos[indice];
        dados.CriaturesAtivos[indice] = temp;

        Debug.Log(dados.CriaturesAtivos[indice].NomeID+" : "+ dados.CriaturesArmagedados[indiceDoSubstituido].NomeID+" : "+temp.NomeID);

        tempString = string.Format(frasesDeArmagedom[6], temp.NomeEmLinguas, temp.CaracCriature.mNivel.Nivel,
                dados.CriaturesArmagedados[indiceDoSubstituido].NomeEmLinguas,
                dados.CriaturesArmagedados[indiceDoSubstituido].CaracCriature.mNivel.Nivel
                );

        if (indice == 0)
        {
            dados.CriatureSai = -1;
            g.HudM.EntraCriatures.FinalizarHud();
            GameController.g.HudM.Painel.EsconderMensagem();
            replace = new ReplaceManager(g.Manager, g.Manager.CriatureAtivo.transform, FluxoDeRetorno.armagedom);
            fase = fasesDoArmagedom.fazendoUmaTroca;
        }
        else
        {
            g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(VoltarDoEntraArmagedado, tempString);
        }
    }

    void MostraOsQueSaem()
    {
        GameController.g.HudM.Painel.AtivarNovaMens(frasesDeArmagedom[5], 30);
        GameController.g.HudM.EntraCriatures
            .IniciarEssaHUD(GameController.g.Manager.Dados.CriaturesAtivos.ToArray(), SubstituiArmagedado,true);
    }

    void InstanciaVisaoDeCura()
    {
        CharacterManager manager = GameController.g.Manager;

        Vector3 V = manager.CriatureAtivo.transform.position;
        Vector3 V2 = manager.transform.position;
        Vector3 V3 = new Vector3(1, 0, 0);
        Vector3[] Vs = new Vector3[] { V, V2 + V3, V2 + 2 * V3, V2 - V3, V2 - 2 * V3, V2 + 3 * V2, V2 - 3 * V3 };
        GameObject animaVida = GameController.g.El.retorna(DoJogo.acaoDeCura1);
        GameObject animaVida2;

        for (int i = 0; i < manager.Dados.CriaturesAtivos.Count; i++)
        {
            if (i < Vs.Length)
            {
                animaVida2 = Instantiate(animaVida, Vs[i], Quaternion.identity);
                Destroy(animaVida2, 1);
            }
        }

        Destroy(Instantiate(GameController.g.El.retorna(DoJogo.curaDeArmagedom),manager.transform.position,Quaternion.identity),10);

    }

    public void Curar()
    {
        //ApagarMenu();
        InstanciaVisaoDeCura();

        GameController.g.Manager.Dados.TodosCriaturesPerfeitos();

        tempoDecorrido = 0;
        dispara.DesligarPaineis();
        fase = fasesDoArmagedom.curando;
    }

    public void VoltarAoJogo()
    {
        GameController g = GameController.g;
        //AndroidController.a.LigarControlador();

        g.Manager.AoHeroi();
        g.HudM.ModoHeroi();
        dispara.DesligarPaineis();
        //gameObject.SetActive(false);
        fase = fasesDoArmagedom.emEspera;
        ActionManager.anularAcao = false;
        GameController.g.Salvador.SalvarAgora();
    }

    void EntraFrasePossoAjudar()
    {
        dispara.ReligarPaineis();
        dispara.Dispara(t[t.Length - 1], fotoDoNPC);
        fase = fasesDoArmagedom.escolhaInicial;
        ActionManager.ModificarAcao(
            GameController.g.transform,
            ()=>{ OpcaoEscolhida(GameController.g.HudM.Menu_Basico.OpcaoEscolhida); }
            );
    }

    public void BotaoArmagedom()
    {
        FluxoDeBotao();
        AplicadorDeCamera.cam.InicializaCameraExibicionista(transform, 1);
        GameController.g.HudM.ModoLimpo();
        if(!GameController.g.MyKeys.LocalArmag.Contains(indiceDesseArmagedom))
            GameController.g.MyKeys.LocalArmag.Add(indiceDesseArmagedom);

        dispara.IniciarDisparadorDeTextos();
        GameController.g.Manager.Dados.UltimoArmagedom = indiceDesseArmagedom;
        fase = fasesDoArmagedom.mensInicial;
    }

    public override void FuncaoDoBotao()
    {
        BotaoArmagedom();
    }
}
