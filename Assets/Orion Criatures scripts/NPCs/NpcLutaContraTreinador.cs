using UnityEngine;
using System.Collections;

[System.Serializable]
public class NpcLutaContraTreinador : NPCdeConversa
{
    [SerializeField] private CriatureParaEncontro[] criaturesDoTreinador;
    [SerializeField] private KeyShift chaveDaLuta;
    [SerializeField] private ItemDeBau[] recompensas;
    [SerializeField] private ChaveDeTexto chaveDaFinalizacao;
    [SerializeField] private string chaveDaFinalizacaoString = "";
    [SerializeField] private string chaveDepoisDeFinalizado="";
    [SerializeField] private string nomeDoTreinador = "";
    [SerializeField] private bool perguntaParaIniciar = false;


    private float tempoDecorrido = 0;
    private int indiceDoEnviado = 0;
    
    private AnimaBraco animaB;
    private DisparaTexto disparaT;
    private EstadoInterno estadoInterno = EstadoInterno.emEspera;

    private enum EstadoInterno
    {
        emEspera,
        perguntaParaIniciar,
        esperandoResposta,
        animacaoDeEncontro,
        cameraNoTreinador,
        frasePreInicio,
        animandoBraco,
        leituraDeLuta,
        fraseDaFinalizacao,
        novoJogoDeCamera,
        verificandoMaisItens,
        finalizacao
    }

    void IniciarLuta()
    {
        tempoDecorrido = 0;
        AplicadorDeCamera.cam.InicializaCameraExibicionista(MeuTransform, 1, true);
        InsereElementosDoEncontro.EncontroDeTreinador(GameController.g.Manager, MeuTransform);
        estadoInterno = EstadoInterno.animacaoDeEncontro;
    }

    public override bool Update()
    {
        if (estadoInterno == EstadoInterno.emEspera)
        {
            if (estado == EstadoDoNPC.conversando
                    && GameController.g.HudM.DisparaT.IndiceDaConversa == conversa.Length - 1
                    && !GameController.g.MyKeys.VerificaAutoShift(chaveDaLuta)
                    )
            {
                if (perguntaParaIniciar)
                {
                    GameController.g.HudM.DisparaT.DesligarPaineis();
                    GameController.g.HudM.DisparaT.IniciarDisparadorDeTextos();
                    Debug.Log("texto não traduzido detectdo");
                    GameController.g.HudM.DisparaT.Dispara(
                       "Você está preparado para me enfrentar?", fotoDoNPC);
                    estadoInterno = EstadoInterno.perguntaParaIniciar;
                }
                else
                {
                    IniciarLuta();
                }
                return false;
            }
            else
                return base.Update();
        }
        else
            return UpdateInterno();
    }

    bool UpdateInterno()
    {
        switch (estadoInterno)
        {
            case EstadoInterno.animacaoDeEncontro:
                tempoDecorrido += Time.deltaTime;
                if (tempoDecorrido > 0.5F)
                {
                    estadoInterno = EstadoInterno.cameraNoTreinador;
                }
            break;
            case EstadoInterno.perguntaParaIniciar:
                if (!GameController.g.HudM.DisparaT.LendoMensagemAteOCheia())
                {
                    GameController.g.HudM.Menu_Basico.IniciarHud(IniciarOuNao,
                        BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.simOuNao).ToArray());
                    estadoInterno = EstadoInterno.esperandoResposta;
                }
            break;
            case EstadoInterno.esperandoResposta:
                GameController.g.HudM.Menu_Basico.MudarOpcao();

                if (CommandReader.ButtonDown(0, GameController.g.Manager.Control))
                {
                    Debug.Log("controlador de acao removido");
                    estadoInterno = EstadoInterno.emEspera;
                    IniciarOuNao(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
                }
            break;
            case EstadoInterno.cameraNoTreinador:
                if (AplicadorDeCamera.cam.FocarPonto(-2*Vector3.up,1, 6, 4, true))
                {
                    disparaT = GameController.g.HudM.DisparaT;
                    disparaT.IniciarDisparadorDeTextos();
                    conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.frasesDaLutaContraTreinador).ToArray();
                    conversa = new string[2] {
                        string.Format(conversa[0],criaturesDoTreinador.Length), conversa[1] };
                    estadoInterno = EstadoInterno.frasePreInicio;
                }
            break;
            case EstadoInterno.frasePreInicio:
                if (disparaT.UpdateDeTextos(conversa))
                {
                    animaB = new AnimaBraco(MeuTransform,GameController.g.Manager.transform,true);
                    Transform aux = GameController.g.Manager.CriatureAtivo.transform;
                    animaB.PosCriature = aux.position + 3 * aux.forward;
                    
                    estadoInterno = EstadoInterno.animandoBraco;
                    AplicadorDeCamera.cam.DesligarMoveCamera();
                }
            break;
            case EstadoInterno.animandoBraco:
                if (!animaB.AnimaEnvia(criaturesDoTreinador[indiceDoEnviado].C,"criatureDeTreinador"))
                {
                    GameController.g.EncontroAgoraCom(
                        criaturesDoTreinador[indiceDoEnviado].PrepararInicioDoCriature(
                            GameObject.Find("criatureDeTreinador").GetComponent<CreatureManager>()),true,nomeDoTreinador);
                    estadoInterno = EstadoInterno.leituraDeLuta;
                }
            break;
            case EstadoInterno.leituraDeLuta:
                if (GameController.g.InimigoAtivo == null)
                {
                    indiceDoEnviado++;
                    if (indiceDoEnviado < criaturesDoTreinador.Length)
                    {
                        conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.frasesDaLutaContraTreinador).ToArray();
                        conversa = new string[2] { conversa[2], conversa[3] };
                        disparaT.IniciarDisparadorDeTextos();
                        AplicadorDeCamera.cam.InicializaCameraExibicionista(MeuTransform, 1, true);
                        estadoInterno = EstadoInterno.novoJogoDeCamera;
                    }
                    else
                    {
                        AplicadorDeCamera.cam.InicializaCameraExibicionista(MeuTransform, 1, true);
                        conversa = StringParaEnum.SetarConversaOriginal(chaveDaFinalizacaoString, ref chaveDaFinalizacao);
                        disparaT.IniciarDisparadorDeTextos();                        
                        estadoInterno = EstadoInterno.fraseDaFinalizacao;
                    }
                }
            break;
            case EstadoInterno.novoJogoDeCamera:
                if (AplicadorDeCamera.cam.FocarPonto(1, 6, 4, true))
                {
                    estadoInterno = EstadoInterno.frasePreInicio;
                }
            break;
            case EstadoInterno.fraseDaFinalizacao:
                if (AplicadorDeCamera.cam.FocarPonto(1, 6, 4, true))
                {
                    if (disparaT.UpdateDeTextos(conversa))
                    {
                        if (recompensas.Length <= 0)
                        {
                            estadoInterno = EstadoInterno.finalizacao;
                        }
                        else
                        {
                            conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.bau).ToArray();
                            indiceDoEnviado = 0;
                            VerificaItem();
                            estadoInterno = EstadoInterno.verificandoMaisItens;
                        }
                    }
                }
            break;
            case EstadoInterno.verificandoMaisItens:
                if (CommandReader.ButtonDown(0, GameController.g.Manager.Control))
                {
                    Debug.Log("controlador de acao removido");
                    if (indiceDoEnviado + 1 > recompensas.Length)
                    {
                        GameController.g.HudM.Painel.EsconderMensagem();
                        GameController.g.HudM.MostrarItem.DesligarPainel();
                        estadoInterno = EstadoInterno.finalizacao;
                    }
                    else
                    {
                        VerificaItem();
                    }
                    ActionManager.anularAcao = true;
                }
            break;
            case EstadoInterno.finalizacao:
                estado = EstadoDoNPC.finalizadoComBotao;
                estadoInterno = EstadoInterno.emEspera;
                GameController.g.MyKeys.MudaShift(chaveDaLuta, true);
                GameController.g.RetornarParaFluxoDoHeroi(true);
                return true;
            //break;
        }
        return false;
    }

    void IniciarOuNao(int val)
    {
        switch (val)
        {
            case 0:
                IniciarLuta();
            break;
            case 1:
                estado = EstadoDoNPC.finalizadoComBotao;
                estadoInterno = EstadoInterno.emEspera;
            break;
        }

        GameController.g.HudM.Menu_Basico.FinalizarHud();
        GameController.g.HudM.DisparaT.DesligarPaineis();
    }

    void VerificaItem()
    {        
        ItemDeBau ii = recompensas[indiceDoEnviado];
        GameController.g.HudM.Painel.AtivarNovaMens(string.Format(conversa[3], ii.Quantidade, MbItens.NomeEmLinguas(ii.Item)), 25);
        GameController.g.HudM.MostrarItem.IniciarPainel(ii.Item, ii.Quantidade);
        GameController.g.Manager.Dados.AdicionaItem(ii.Item, ii.Quantidade);

        indiceDoEnviado++;
        
    }

    public override void IniciaConversa()
    {
        if (GameController.g.MyKeys.VerificaAutoShift(chaveDaLuta))
        {
            if (chaveDepoisDeFinalizado != "")
                conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(StringParaEnum.ObterEnum(chaveDepoisDeFinalizado, chaveDaFinalizacao)).ToArray();
        }
            
        base.IniciaConversa();
    }

}

[System.Serializable]
public class CriatureParaEncontro
{
    [SerializeField] private CriatureBase c;
    [SerializeField] private bool golpeDeInspector = false;
    [SerializeField] private bool pvDeInspector = false;

    private CreatureManager cm;

    public CriatureBase C
    {
        get { return c; }
    }

    public CreatureManager PrepararInicioDoCriature(CreatureManager cm)
    {
        if (golpeDeInspector)
            cm.MeuCriatureBase.GerenteDeGolpes.meusGolpes = C.GerenteDeGolpes.meusGolpes;

        if (pvDeInspector)
        {
            cm.MeuCriatureBase.CaracCriature.meusAtributos.PV.Maximo = C.CaracCriature.meusAtributos.PV.Maximo;
            cm.MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente = C.CaracCriature.meusAtributos.PV.Corrente;
        }

        //GameController.g.EncontroAgoraCom(cm);
        return cm;
    }
}
