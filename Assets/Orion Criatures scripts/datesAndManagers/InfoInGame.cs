using UnityEngine;
using System.Collections;

public class InfoInGame : AtivadorDeBotao
{
    [SerializeField] private string paraChaveDeTexto = "";
    [SerializeField] private bool exigirTrigger = false;

    private string[] conversa;
    private EstadoDaInfoGame estado = EstadoDaInfoGame.emEspera;
    

    private enum EstadoDaInfoGame
    {
        emEspera,
        lendoTexto
    }

    void Start()
    {
        textoDoBotao = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.textoBaseDeAcao)[1];
        if(!exigirTrigger)
            SempreEstaNoTrigger();

        conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(StringParaEnum.ObterEnum<ChaveDeTexto>(paraChaveDeTexto)).ToArray();
    }

    new void Update()
    {
        switch (estado)
        {
            case EstadoDaInfoGame.emEspera:
                base.Update();
            break;
            case EstadoDaInfoGame.lendoTexto:
                if (GameController.g.HudM.DisparaT.UpdateDeTextos(conversa))
                {
                    //CommandReader.useiAcao = true;
                    EventAgregator.Publish(new StandardSendStringEvent(GameController.g.gameObject, "Book1", EventKey.disparaSom));
                    GameController.g.HudM.DisparaT.DesligarPaineis();
                    estado = EstadoDaInfoGame.emEspera;
                    GameController.g.Manager.AoHeroi();
                }
            break;
        }
    }
    public override void FuncaoDoBotao()
    {
        FluxoDeBotao();
        SomDoIniciar();
        GameController.g.HudM.DisparaT.IniciarDisparadorDeTextos(true);
        estado = EstadoDaInfoGame.lendoTexto;
    }
}
