using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalDeCapitulo : AtivadorDeBotao {

    public override void FuncaoDoBotao()
    {
        FluxoDeBotao();
        GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(()=> {
            Debug.Log("aqui");
            StartCoroutine(ProxMens());
        }, "Esse é o ponto final da demonstração de Orion Criatures, o proximo passo é implementar a luta no estádio");
    }

    // Use this for initialization
    void Start () {
        textoDoBotao = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.textoBaseDeAcao)[1];
        SempreEstaNoTrigger();
	}

    // Update is called once per frame
    //	void Update () {

    //}

    IEnumerator ProxMens()
    {
        Debug.Log("antes do tempo");
        yield return new WaitForSeconds(0.25f);
        Debug.Log("depois do tempo");
        GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(()=> {
            GameController.g.Manager.AoHeroi();
        } , "Não esqueçam de deixar um feedback \n\r e obrigado por jogar Orion Criatures");
    }
}
