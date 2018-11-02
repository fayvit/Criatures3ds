using UnityEngine;
using System.Collections;

public class MiniArmagedomPortatilNaCaverna : AtivadorDoBotaoConversa
{
    [SerializeField]private ChaveDeTexto chaveDeTextoSecundaria = ChaveDeTexto.comoVaiSuaJornada;
    [SerializeField]private KeyShift chave = KeyShift.estouNoTuto;
    [SerializeField]private NpcDoPrimeiroMiniArmagedom npcMini;
    [SerializeField]private IndiceDeArmagedoms indiceDeArmagedom;

    // Use this for initialization
    new void Start()
    {
        textoDoBotao = BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.textoBaseDeAcao);
        npc = npcMini;
        base.Start();
    }

    /* Update is called once per frame
    new void Update()
    {
        base.Update();
    }*/

    public override void FuncaoDoBotao  ()
    {
        if (GameController.g.MyKeys.VerificaAutoShift(chave))
        {
            if (!GameController.g.MyKeys.LocalArmag.Contains(indiceDeArmagedom))
                GameController.g.MyKeys.LocalArmag.Add(indiceDeArmagedom);

            Destroy(
            Instantiate(
                GameController.g.El.retorna(DoJogo.curaDeArmagedom),
                GameController.g.Manager.transform.position,
                Quaternion.identity),5);
            GameController.g.Manager.Dados.TodosCriaturesPerfeitos();
            GameController.g.HudM.AtualizaDadosDaHudVida(false);
            GameController.g.Manager.Dados.UltimoArmagedom = indiceDeArmagedom;
            npc.MudaChaveDaConversa(chaveDeTextoSecundaria);
            BotaoConversa();
        }
        else
            BotaoConversa();
    }
}
