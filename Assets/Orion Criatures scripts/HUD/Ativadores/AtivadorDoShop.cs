
using UnityEngine;
using System.Collections;

public class AtivadorDoShop : AtivadorDeBotao
{
    [SerializeField] private Sprite fotoDoNPC;
    [SerializeField] private nomeIDitem[] aVenda;
    [SerializeField] private Transform focoDeCamera;

    private void Start()
    {
        textoDoBotao = BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.textoBaseDeAcao);
    }

    public void BotaoShop()
    {
        FluxoDeBotao();
        GameController.g.HudM.Shop_Manager.IniciarShop(aVenda, fotoDoNPC);
        AplicadorDeCamera.cam.InicializaCameraExibicionista(focoDeCamera, 1);
    }

    public override void FuncaoDoBotao()
    {
        BotaoShop();
    }
}
