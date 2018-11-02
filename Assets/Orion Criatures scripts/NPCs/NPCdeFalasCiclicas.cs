using UnityEngine;
using System.Collections;

[System.Serializable]
public class NPCdeFalasCiclicas : NPCdeConversa
{
    [SerializeField] private string[] falas;

    private int indiceDaFala = 0;
    void VerificaQualFala()
    {

        ChaveDeTexto inutil = ChaveDeTexto.bomDia;
        conversa = StringParaEnum.SetarConversaOriginal(falas[indiceDaFala], ref inutil);
        // conversa é uma variavel protected da classe pai
        indiceDaFala++;
        if (indiceDaFala >= falas.Length)
            indiceDaFala = 0;
    }

    override public void IniciaConversa()
    {
        VerificaQualFala();
        base.IniciaConversa();
    }
}
