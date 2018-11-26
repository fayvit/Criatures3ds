using UnityEngine;
using System.Collections;

public class AtivadorDoBotaoArmagedom : AtivadorDeBotao
{
    [SerializeField] private Sprite fotoDoNPC;
    [SerializeField] private IndiceDeArmagedoms indiceDesseArmagedom = IndiceDeArmagedoms.daCavernaInicial;

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
        possoAjudar,
        armagedadosAberto,
        fazendoUmaTroca,
        mensDetrocaAberta,
        escolhaDePergaminho,
        vendendoPergaminho,
        menuSuspenso
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
                AplicadorDeCamera.cam.FocarPonto(2, 8, -1, true);
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

                if (ActionManager.ButtonUp(1, GameController.g.Manager.Control))
                {
                    ActionManager.useiCancel = true;
                    OpcaoEscolhida(txtDeOpcoes.Length - 1);
                }
                else if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    OpcaoEscolhida(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
                }
                break;
            case fasesDoArmagedom.curando:

                tempoDecorrido += Time.deltaTime;
                if (tempoDecorrido > TEMPO_DE_CURA || ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    fase = fasesDoArmagedom.fraseQueAntecedePossoAjudar;
                    dispara.ReligarPaineis();
                    dispara.Dispara(frasesDeArmagedom[0], fotoDoNPC);
                }
                break;
            case fasesDoArmagedom.fraseQueAntecedePossoAjudar:
                if (!dispara.LendoMensagemAteOCheia())
                {
                    /*
                    ActionManager.ModificarAcao(GameController.g.transform, () =>
                     {
                         LigarMenu();
                         EntraFrasePossoAjudar();
                     });*/

                    fase = fasesDoArmagedom.possoAjudar;
                }
                break;
            case fasesDoArmagedom.possoAjudar:
                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    LigarMenu();
                    EntraFrasePossoAjudar();
                }
                break;
            case fasesDoArmagedom.armagedadosAberto:
                if (!dispara.LendoMensagemAteOCheia())
                    GameController.g.HudM.EntraCriatures.MudarOpcao();

                if (ActionManager.ButtonUp(1, GameController.g.Manager.Control))
                {
                    ActionManager.useiCancel = true;
                    GameController.g.HudM.EntraCriatures.FinalizarHud();
                    GameController.g.HudM.Painel.EsconderMensagem();
                    LigarMenu();
                    EntraFrasePossoAjudar();
                }
                else if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    ActionManager.VerificaAcao();
                    //AoEscolherumCriature(GameController.g.HudM.EntraCriatures.OpcaoEscolhida);
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
                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    EscolhaDeComprarPergaminho(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
                }
                else
                if (ActionManager.ButtonUp(1, GameController.g.Manager.Control))
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

        switch (opcao)
        {
            case 0:
            case 1:
            case 2:
                EventAgregator.Publish(
                    new StandardSendStringEvent(
                        GameController.g.gameObject,
                        SoundEffectID.XP_Swing03.ToString(),
                        EventKey.disparaSom));
                break;
        }
    }

    void ComprarPergaminhos()
    {
        dispara.ReligarPaineis();
        dispara.Dispara(string.Format(frasesDeArmagedom[8], new MbPergaminhoDeArmagedom().Valor.ToString()), fotoDoNPC);
        GameController.g.HudM.Menu_Basico.IniciarHud(EscolhaDeComprarPergaminho,
            BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.simOuNao).ToArray());
        fase = fasesDoArmagedom.escolhaDePergaminho;
        /*
ActionManager.ModificarAcao(
GameController.g.transform,
() => { EscolhaDeComprarPergaminho(GameController.g.HudM.Menu_Basico.OpcaoEscolhida); }
);*/
    }

    void EscolhaDeComprarPergaminho(int escolha)
    {
        GameController.g.HudM.Menu_Basico.FinalizarHud();
        GameController.g.HudM.DisparaT.DesligarPaineis();

        switch (escolha)
        {
            case 0:
                EventAgregator.Publish(EventKey.positiveUiInput, null);
                fase = fasesDoArmagedom.vendendoPergaminho;
                GameController.g.HudM.PainelQuantidades.IniciarEssaHud(PegaUmItem.Retorna(nomeIDitem.pergArmagedom));
                break;
            case 1:
                EventAgregator.Publish(EventKey.negativeUiInput, null);
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
        EventAgregator.Publish(EventKey.positiveUiInput, null);
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
            fase = fasesDoArmagedom.menuSuspenso;
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
            fase = fasesDoArmagedom.menuSuspenso;
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

        Debug.Log(dados.CriaturesAtivos[indice].NomeID + " : " + dados.CriaturesArmagedados[indiceDoSubstituido].NomeID + " : " + temp.NomeID);

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
            .IniciarEssaHUD(GameController.g.Manager.Dados.CriaturesAtivos.ToArray(), SubstituiArmagedado,
            () => { fase = fasesDoArmagedom.armagedadosAberto; Debug.Log("esse é o meu ao desistir"); }, true);
        fase = fasesDoArmagedom.armagedadosAberto;

        ActionManager.ModificarAcao(GameController.g.transform, () => {
            fase = fasesDoArmagedom.menuSuspenso;
            GameController.g.HudM.EntraCriatures.AcaoDeOpcaoEscolhida();
        });
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

        Destroy(Instantiate(GameController.g.El.retorna(DoJogo.curaDeArmagedom), manager.transform.position, Quaternion.identity), 10);
        EventAgregator.Publish(new StandardSendStringEvent(gameObject, SoundEffectID.XP_Heal02.ToString(), EventKey.disparaSom));

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

        EventAgregator.Publish(EventKey.requestMusicBackupReturn, null);
        EventAgregator.Publish(new StandardSendStringEvent(GameController.g.gameObject, SoundEffectID.XP_Swing03.ToString(), EventKey.disparaSom));
    }

    void EntraFrasePossoAjudar()
    {
        dispara.ReligarPaineis();
        dispara.Dispara(t[t.Length - 1], fotoDoNPC);
        fase = fasesDoArmagedom.escolhaInicial;
        /*
        ActionManager.ModificarAcao(
            GameController.g.transform,
            ()=>{ OpcaoEscolhida(GameController.g.HudM.Menu_Basico.OpcaoEscolhida); }
            );*/
    }

    public void BotaoArmagedom()
    {
        FluxoDeBotao();
        AplicadorDeCamera.cam.InicializaCameraExibicionista(transform, 1);
        GameController.g.HudM.ModoLimpo();
        if (!GameController.g.MyKeys.LocalArmag.Contains(indiceDesseArmagedom))
            GameController.g.MyKeys.LocalArmag.Add(indiceDesseArmagedom);

        dispara.IniciarDisparadorDeTextos();
        GameController.g.Manager.Dados.UltimoArmagedom = indiceDesseArmagedom;
        fase = fasesDoArmagedom.mensInicial;

        SomDoIniciar();
        EventAgregator.Publish(new StandardSendStringAndFloatEvent(gameObject, 1, "BlackMarket", EventKey.requestMusicWithBackup));
    }

    public override void FuncaoDoBotao()
    {
        BotaoArmagedom();
    }
}
