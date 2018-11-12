using UnityEngine;
using System.Collections;

[System.Serializable]
public class NpcIan : NPCdeConversa
{
    private float contadorDeTempoX = 0;
    private bool indice1 = false;
    private string[] textoDasOpcoes = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.opcoesDeIan).ToArray();
    private string[][] conversasDeIan = new string[2][] {
        BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.conversaBasicaDeIan).ToArray(),
        BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.conversaBasicaDeIan2).ToArray(),
    };
    private DisparaTexto disparaT;
    private EstadoInterno estadoInterno = EstadoInterno.emEspera;

    private const float TEMP_COISAS_BOAS = 1;

    private enum EstadoInterno
    {
        emEspera,
        escolhasAbertas,
        conversaInterna,
        fraseDeFinalizacao,
        frasePreVenda,
        aguardandoSimOuNao,
        fraseInsuficiente,
        fraseDeBoaCompra,
        particulaDeCoisasBoas,
        fraseFinalDeCompra
    }

    public override void Start(Transform T)
    {
        disparaT = GameController.g.HudM.DisparaT;
        base.Start(T);
    }

    public override bool Update()
    {
        if (estadoInterno == EstadoInterno.emEspera)
        {
            if (estado == EstadoDoNPC.conversando
                    && GameController.g.HudM.DisparaT.IndiceDaConversa == conversa.Length - 1
                    )
            {
                return EntraNasEscolhas();
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
            case EstadoInterno.escolhasAbertas:

                GameController.g.HudM.Menu_Basico.MudarOpcao();

                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    Debug.Log("controlador de acao removido");
                    OpcaoEscolhida(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
                }


                if (ActionManager.ButtonUp(1, GameController.g.Manager.Control))
                {
                    PreFinal();
                }
                else
                    ActionManager.useiCancel = false;
            break;
            case EstadoInterno.conversaInterna:
                if (disparaT.UpdateDeTextos(conversa, fotoDoNPC))
                {
                    EntraNasEscolhas();
                }
            break;
            case EstadoInterno.fraseDeFinalizacao:
                if (disparaT.UpdateDeTextos(conversa, fotoDoNPC))
                {
                    FinalizaConversa();
                }
            break;
            case EstadoInterno.frasePreVenda:
                if (!disparaT.LendoMensagemAteOCheia())
                {
                    GameController.g.HudM.Menu_Basico.IniciarHud(ComprarOuNaoComprar,
                        BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.simOuNao).ToArray());
                    estadoInterno = EstadoInterno.aguardandoSimOuNao;
                }
            break;
            case EstadoInterno.aguardandoSimOuNao:
                GameController.g.HudM.Menu_Basico.MudarOpcao();

                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    Debug.Log("controlador de acao removido");
                    ComprarOuNaoComprar(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
                }
            break;
            case EstadoInterno.fraseInsuficiente:
                if (!disparaT.LendoMensagemAteOCheia())
                {
                    if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                    {
                        Debug.Log("controlador de acao removido");
                        disparaT.DesligarPaineis();
                        EntraNasEscolhas();
                    }
                }
            break;
            case EstadoInterno.fraseDeBoaCompra:
                if (!disparaT.LendoMensagemAteOCheia())
                {
                    if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                    {
                        Debug.Log("controlador de acao removido");
                        MonoBehaviour.Destroy(
                    MonoBehaviour.Instantiate(
                        GameController.g.El.retorna(DoJogo.particulaDaDefesaPergaminhoFora), MeuTransform.position, Quaternion.identity
                        ), 5);

                        disparaT.DesligarPaineis();
                        contadorDeTempoX = 0;
                        estadoInterno = EstadoInterno.particulaDeCoisasBoas;
                    }
                }
            break;
            case EstadoInterno.particulaDeCoisasBoas:
                contadorDeTempoX += Time.deltaTime;
                if (contadorDeTempoX > TEMP_COISAS_BOAS)
                {
                    disparaT.IniciarDisparadorDeTextos();
                    disparaT.Dispara(conversa[3], fotoDoNPC);
                    estadoInterno = EstadoInterno.fraseFinalDeCompra;
                    GameController.g.HudM.MostrarItem.IniciarPainel(!indice1 ? nomeIDitem.pergSinara : nomeIDitem.pergAlana,1);
                }
            break;
            case EstadoInterno.fraseFinalDeCompra:
                if (!disparaT.LendoMensagemAteOCheia())
                {

                    if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                    {
                        Debug.Log("controlador de acao removido");
                        GameController.g.HudM.MostrarItem.DesligarPainel();
                        disparaT.DesligarPaineis();
                        EntraNasEscolhas();
                    }
                }
            break;
        }
        return false;
    }

    void ComprarOuNaoComprar(int indice)
    {
        GameController.g.HudM.Menu_Basico.FinalizarHud();
        DisparaTexto disparaT = GameController.g.HudM.DisparaT;
        KeyVar keys = GameController.g.MyKeys;
        DadosDoPersonagem dados = GameController.g.Manager.Dados;

        disparaT.DesligarPaineis();


        int val = !indice1
            ? (int)Mathf.Pow(2, keys.VerificaAutoCont(KeyCont.pergSinaraComprados))
            : (int)Mathf.Pow(2, keys.VerificaAutoCont(KeyCont.pergAlanaComprados));

        switch (indice)
        {            
            case 0:
                if (dados.TemItem(nomeIDitem.tinteiroSagradoDeLog) >= val && dados.Cristais >= 100 * val)
                {
                    dados.Cristais -= 100 * val;
                    GameController.g.HudM.AtualizeImagemDeAtivos();
                    MbItens.RetirarUmItem(GameController.g.Manager, nomeIDitem.tinteiroSagradoDeLog, val);
                    dados.AdicionaItem(indice1 ? nomeIDitem.pergAlana : nomeIDitem.pergSinara);
                    disparaT.ReligarPaineis();
                    disparaT.Dispara(conversa[2], fotoDoNPC);
                    estadoInterno = EstadoInterno.fraseDeBoaCompra;
                    keys.SomaCont(indice1 ? KeyCont.pergAlanaComprados : KeyCont.pergSinaraComprados, 1);
                }
                else
                {
                    disparaT.ReligarPaineis();
                    disparaT.Dispara(conversa[1], fotoDoNPC);
                    estadoInterno = EstadoInterno.fraseInsuficiente;
                }
            break;
            case 1:
                EntraNasEscolhas();
            break;
        }        
    }

    void PreFinal()
    {
        estadoInterno = EstadoInterno.fraseDeFinalizacao;
        conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.despedidabasicaDeIan).ToArray();
        GameController.g.HudM.DisparaT.IniciarDisparadorDeTextos();
        GameController.g.HudM.Menu_Basico.FinalizarHud();
    }

    bool EntraNasEscolhas()
    {
        GameController.g.HudM.DisparaT.IniciarDisparadorDeTextos();
        GameController.g.HudM.Menu_Basico.IniciarHud(OpcaoEscolhida, textoDasOpcoes);
        //ActionManager.ModificarAcao(GameController.g.transform, OpcaoEscolhida);
        estadoInterno = EstadoInterno.escolhasAbertas;
        return UpdateInterno();
    }

    void OpcaoEscolhida(int opcao)
    {
        switch (opcao)
        {
            case 0:
            case 1:
                indice1 = opcao == 0 ? false : true;
                conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.frasesDeVendaDeIan).ToArray();
                GameController.g.HudM.Menu_Basico.FinalizarHud();
                GameController.g.HudM.DisparaT.IniciarDisparadorDeTextos();
                GameController.g.HudM.DisparaT.Dispara(
                   string.Format(conversa[0],RetornaArgumentosPreVenda()), fotoDoNPC);
                estadoInterno = EstadoInterno.frasePreVenda;
                
            break;
            case 2:
                conversa = conversasDeIan[indice1 ? 1 : 0];
                indice1 = !indice1;
                estadoInterno = EstadoInterno.conversaInterna;
                GameController.g.HudM.DisparaT.IniciarDisparadorDeTextos(true);
                GameController.g.HudM.Menu_Basico.FinalizarHud();
            break;
            case 3:
                PreFinal();
            break;
        }
    }

    string[] RetornaArgumentosPreVenda()
    {
        string[] retorno = new string[3];
        KeyVar keys = GameController.g.MyKeys;
        retorno[0] = !indice1 ? MbItens.NomeEmLinguas(nomeIDitem.pergSinara) : MbItens.NomeEmLinguas(nomeIDitem.pergAlana);
        retorno[1] = !indice1 
            ? Mathf.Pow(2, keys.VerificaAutoCont(KeyCont.pergSinaraComprados)).ToString() 
            : Mathf.Pow(2, keys.VerificaAutoCont(KeyCont.pergAlanaComprados)).ToString();
        retorno[2] = !indice1
            ? (100*Mathf.Pow(2, keys.VerificaAutoCont(KeyCont.pergSinaraComprados))).ToString()
            : (100*Mathf.Pow(2, keys.VerificaAutoCont(KeyCont.pergAlanaComprados))).ToString();
        return retorno;
    }

    protected override void FinalizaConversa()
    {
        estadoInterno = EstadoInterno.emEspera;
        SetarConversaOriginal();
        base.FinalizaConversa();
    }

}
