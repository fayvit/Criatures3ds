using UnityEngine;
using System.Collections;

[System.Serializable]
public class NPCfalasCondicionais : ConversaComNpcMovimentandoCamera
{
    [SerializeField]private FalasCondicionais[] falas;

    [System.Serializable]
    private class FalasCondicionais
    {
        [SerializeField]private KeyShift chaveCondicionalDaConversa;
        [SerializeField] private string chaveCondicionalDaConversaMirrorString;
        [SerializeField]private ChaveDeTexto chaveDeTextoDaConversa;
        [SerializeField] private string chaveDeTextoDaConversaMirrorString;

        public KeyShift ChaveCondicionalDaConversa
        {
            get {
                chaveCondicionalDaConversa = StringParaEnum.ObterEnum(chaveCondicionalDaConversaMirrorString, chaveCondicionalDaConversa);
                return chaveCondicionalDaConversa; }
            set { chaveCondicionalDaConversa = value; }
        }

        public ChaveDeTexto ChaveDeTextoDaConversa
        {
            get {
                StringParaEnum.SetarConversaOriginal(chaveDeTextoDaConversaMirrorString,ref chaveDeTextoDaConversa);
                return chaveDeTextoDaConversa; }
            set { chaveDeTextoDaConversa = value; }
        }
    }

    void VerificaQualFala()
    {

        for (int i = falas.Length; i > 0; i--)
            if (!GameController.g.MyKeys.VerificaAutoShift(falas[i - 1].ChaveCondicionalDaConversa))
            {
                conversa = BancoDeTextos.RetornaListaDeTextoDoIdioma(falas[i - 1].ChaveDeTextoDaConversa).ToArray();
                IndiceDaConversaCondiciona = i - 1;
            }
        // conversa é uma variavel protected da classe pai
        
    }

    override public void IniciaConversa()
    {
        VerificaQualFala();
        base.IniciaConversa();
    }
}
