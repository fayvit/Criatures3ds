using UnityEngine;
using System.Collections;

public class AtivadorDoBotaoConversa : AtivadorDeBotao
{
    [SerializeField]protected NPCdeConversa npc;

    // Use this for initialization
    protected void Start()
    {
        textoDoBotao = BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.textoBaseDeAcao);
        npc.Start(transform);
        SempreEstaNoTrigger();
    }

    new protected void Update()
    {
        base.Update();

        /*
        if (btn.activeSelf)
            btn.transform.parent.forward = forwardInicialDoBotao;
            */
        if (npc.Update())
        {
            GameController.g.Manager.AoHeroi();
        }
    }

    public void BotaoConversa()
    {
        FluxoDeBotao();

        //Transform T = TransformPosDeConversa.MeAjude(transform);

        npc.IniciaConversa();
        
        //AplicadorDeCamera.cam.InicializaCameraExibicionista(T, 1,true);
    }

    public override void FuncaoDoBotao()
    {
        BotaoConversa();
    }
}
