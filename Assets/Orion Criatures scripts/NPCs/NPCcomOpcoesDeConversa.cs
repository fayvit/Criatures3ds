using UnityEngine;
using System.Collections;

[System.Serializable]
public class NPCcomOpcoesDeConversa : NPCdeConversa
{
    [SerializeField] private ChaveDeTexto chaveDasOpcoes;
    [SerializeField] private ChaveDeTexto[] chavesDasConversas;

    private EstadoInterno estadoInterno = EstadoInterno.emEspera;
    private string[] textoDasOpcoes;
    

    private enum EstadoInterno
    {
        emEspera,
        escolhasAbertas,
        conversaInterna
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

    bool EntraNasEscolhas()
    {
        GameController.g.HudM.DisparaT.IniciarDisparadorDeTextos(true);

        string[] s =new string[textoDasOpcoes.Length + 1];
        for (int i = 0; i < textoDasOpcoes.Length; i++)
            s[i] = textoDasOpcoes[i];

        s[textoDasOpcoes.Length] = BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.Voltar);
        GameController.g.HudM.Menu_Basico.IniciarHud(OpcaoEscolhida, s);
        //ActionManager.ModificarAcao(GameController.g.transform, OpcaoEscolhida);
        estadoInterno = EstadoInterno.escolhasAbertas;
        return UpdateInterno();
    }

    /*
    void OpcaoEscolhida()
    {
        OpcaoEscolhida(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
    }*/

    void OpcaoEscolhida(int opcao)
    {
        if (opcao < textoDasOpcoes.Length)
        {
            conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(chavesDasConversas[opcao]).ToArray();
            estadoInterno = EstadoInterno.conversaInterna;
        }
        else
        {
            estadoInterno = EstadoInterno.emEspera;
            FinalizaConversa();
        }

        GameController.g.HudM.Menu_Basico.FinalizarHud();
    }

    bool UpdateInterno()
    {
        switch (estadoInterno)
        {
            case EstadoInterno.escolhasAbertas:
                
                GameController.g.HudM.Menu_Basico.MudarOpcao();

                if(CommandReader.ButtonDown(0,GameController.g.Manager.Control))
                    OpcaoEscolhida(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);


                if (CommandReader.ButtonDown(1,GameController.g.Manager.Control))
                {
                    FinalizaConversa();
                    GameController.g.HudM.Menu_Basico.FinalizarHud();
                }
            break;
            case EstadoInterno.conversaInterna:
                if (GameController.g.HudM.DisparaT.UpdateDeTextos(conversa, fotoDoNPC))
                {
                    EntraNasEscolhas();
                }
            break;
        }
        return false;
    }

    public void IniciarOpcoes()
    {
        textoDasOpcoes = BancoDeTextos.RetornaListaDeTextoDoIdioma(chaveDasOpcoes).ToArray();
    }

    protected override void FinalizaConversa()
    {
        estadoInterno = EstadoInterno.emEspera;
        SetarConversaOriginal();
        base.FinalizaConversa();
    }
}
