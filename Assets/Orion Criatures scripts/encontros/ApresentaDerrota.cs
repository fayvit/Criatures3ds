using UnityEngine;
using System.Collections;

public class ApresentaDerrota
{
    private float contadorDeTempo = 0;
    private string[] textos;

    private FaseDaDerrota fase = FaseDaDerrota.emEspera;
    private CharacterManager manager;
    private CreatureManager inimigoDerrotado;
    private ReplaceManager replace;

    private const float TEMPO_PARA_MOSTRAR_MENSAGEM_INICIAL = 0.25F;
    private const float TEMPO_PARA_ESCURECER = 2;

    private enum FaseDaDerrota
    {
        emEspera,
        abreMensagem,
        esperandoFecharMensagemDeDerrota,
        entrandoUmNovo,
        mensDoArmagedom,
        tempoParaCarregarCena,
        hudEntraCriatureAberta
    }

    public enum RetornoDaDerrota
    {
        atualizando,
        voltarParaPasseio,
        deVoltaAoArmagedom
    }

    public ApresentaDerrota(CharacterManager manager, CreatureManager inimigoDerrotado)
    {
        this.manager = manager;
        this.inimigoDerrotado = inimigoDerrotado;
        textos = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.apresentaDerrota).ToArray();
        fase = FaseDaDerrota.abreMensagem;
    }

    void AcaoDaMensagemDerrota()
    {
        fase = FaseDaDerrota.esperandoFecharMensagemDeDerrota;
    }

    public RetornoDaDerrota Update()
    {
        switch (fase)
        {
            case FaseDaDerrota.abreMensagem:
                contadorDeTempo += Time.deltaTime;
                if (contadorDeTempo > TEMPO_PARA_MOSTRAR_MENSAGEM_INICIAL)
                {
                    GameController.g.HudM.Painel.AtivarNovaMens(string.Format(textos[0],
                        manager.CriatureAtivo.MeuCriatureBase.NomeEmLinguas),30);
                    fase = FaseDaDerrota.esperandoFecharMensagemDeDerrota;
                }
            break;
            case FaseDaDerrota.esperandoFecharMensagemDeDerrota:
                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    Debug.Log("controlador de acao removido");
                    GameController.g.HudM.Painel.EsconderMensagem();
                    if (manager.Dados.TemCriatureVivo())
                    {
                        IniciarHudEntra();
                    }
                    else
                    {
                        GameController.g.HudM.Painel.AtivarNovaMens(textos[2],24);
                        fase = FaseDaDerrota.mensDoArmagedom;
                        // Aqui vamos de volta para o armagedom
                        //return RetornoDaDerrota.deVoltaAoArmagedom;
                    }
                }
            break;
            case FaseDaDerrota.mensDoArmagedom:
                if (ActionManager.ButtonUp(0,GameController.g.Manager.Control))
                {
                    Debug.Log("gerenciador de acoes removido");
                    GameController.g.HudM.Painel.EsconderMensagem();
                    //GameController.g.gameObject.AddComponent<FadeView>();
                    GlobalController.g.FadeV.IniciarFadeOut();
                    contadorDeTempo = 0;
                    fase = FaseDaDerrota.tempoParaCarregarCena;
                }
            break;
            case FaseDaDerrota.tempoParaCarregarCena:
                contadorDeTempo += Time.deltaTime;
                if (contadorDeTempo > TEMPO_PARA_ESCURECER)
                {
                    CharacterManager manager = GameController.g.Manager;
                    VisitasParaArmagedom V = LocalizacaoDeArmagedoms.L[manager.Dados.UltimoArmagedom];
                    manager.transform.position = V.Endereco;//manager.Dados.UltimoArmagedom.posHeroi;
                    manager.transform.rotation = V.Rot;
                    manager.Dados.TodosCriaturesPerfeitos();
                    AplicadorDeCamera.cam.GetComponent<Camera>().farClipPlane = 1000;
                    GameController.g.Salvador.SalvarAgora(V.nomeDasCenas);
                    GameObject G = new GameObject();
                    SceneLoader loadScene = G.AddComponent<SceneLoader>();
                    loadScene.CenaDoCarregamento(GameController.g.Salvador.IndiceDoJogoAtual);
                    GameController.g.Manager.AoHeroi();
                    fase = FaseDaDerrota.emEspera;
                }
            break;
            case FaseDaDerrota.entrandoUmNovo:
                if (replace.Update())
                {
                    if (GameController.g.InimigoAtivo)
                    {
                        manager.AoCriature(inimigoDerrotado);
                        GameController.g.HudM.AtualizaDadosDaHudVida(true);
                    }

                    fase = FaseDaDerrota.emEspera;
                    return RetornoDaDerrota.voltarParaPasseio;
                }
            break;
            case FaseDaDerrota.hudEntraCriatureAberta:
                GameController.g.HudM.EntraCriatures.Update();

                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    AoEscolherUmCriature(GameController.g.HudM.EntraCriatures.OpcaoEscolhida);
                }
            break;
        }

        return RetornoDaDerrota.atualizando;
    }

    public void AoEscolherUmCriature(int qual)
    {
        GameController.g.HudM.EntraCriatures.FinalizarHud();
        
        if (manager.Dados.CriaturesAtivos[qual].CaracCriature.meusAtributos.PV.Corrente > 0)
        {
            manager.Dados.CriatureSai = qual - 1;
            fase = FaseDaDerrota.entrandoUmNovo;
            replace = new ReplaceManager(manager, manager.CriatureAtivo.transform, FluxoDeRetorno.criature);
            GameController.g.HudM.Painel.EsconderMensagem();
        }
        else
        {
            fase = FaseDaDerrota.emEspera;
            GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(IniciarHudEntra,string.Format(
               BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.criatureParaMostrador)[1],
               manager.Dados.CriaturesAtivos[qual].NomeEmLinguas
                ));
        }
    }

    void IniciarHudEntra()
    {
        GameController.g.HudM.Painel.AtivarNovaMens(textos[1], 20);
        GameController.g.HudM.EntraCriatures.IniciarEssaHUD(manager.Dados.CriaturesAtivos.ToArray(), AoEscolherUmCriature);
        fase = FaseDaDerrota.hudEntraCriatureAberta;
    }
}
