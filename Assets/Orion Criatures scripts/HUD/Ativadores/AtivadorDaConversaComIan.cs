using UnityEngine;
using System.Collections;

public class AtivadorDaConversaComIan : AtivadorDoBotaoConversa
{
    [SerializeField] private ConversaComNpcMovimentandoCamera npcMov;
    [SerializeField] private NpcIan npcIan;

    new void Start()
    {
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {

            KeyVar keys = GameController.g.MyKeys;


            Debug.Log("Conversou primeiro com Derek: " + keys.VerificaAutoShift(KeyShift.conversouPrimeiroComDerek) +
                " - venceu Derek: " + keys.VerificaAutoShift(KeyShift.venceuDerekPrimeiraVez));


            if (keys.VerificaAutoShift(KeyShift.conversouPrimeiroComDerek) && keys.VerificaAutoShift(KeyShift.venceuDerekPrimeiraVez))
            {
                if (keys.VerificaAutoShift(KeyShift.entreouCanetaDeIan))
                {
                    npc = npcIan;
                }
                else
                {
                    MbItens.RetirarUmItem(GameController.g.Manager, PegaUmItem.Retorna(nomeIDitem.canetaSagradaDeLog),1);
                }
            }
            else if (!keys.VerificaAutoShift(KeyShift.conversouPrimeiroComDerek) && !keys.VerificaAutoShift(KeyShift.venceuDerekPrimeiraVez))
            {
                Debug.Log("Ué....");
                npc = npcMov;
            }

            base.Start();
        }
    }

    public override void FuncaoDoBotao()
    {
        
        KeyVar keys = GameController.g.MyKeys;
            
        keys.MudaShift(KeyShift.conversouPrimeiroComIan, true);


        if (keys.VerificaAutoShift(KeyShift.conversouPrimeiroComDerek)
            &&
            keys.VerificaAutoShift(KeyShift.venceuDerekPrimeiraVez)
            &&
            !keys.VerificaAutoShift(KeyShift.entreouCanetaDeIan)
            )
        {
            npc = new NPCdeConversa();
            Start();
            npc.MudaChaveDaConversa(ChaveDeTexto.IanComCaneta);
            GameController.g.MyKeys.MudaShift(KeyShift.entreouCanetaDeIan, true);
        }
        else if (keys.VerificaAutoShift(KeyShift.conversouPrimeiroComDerek) && !keys.VerificaAutoShift(KeyShift.venceuDerekPrimeiraVez))
        {
            npc = new NPCdeConversa();
            Start();
            npc.MudaChaveDaConversa(ChaveDeTexto.IanDepoisDeDerek);
        } else
            Start();

        base.FuncaoDoBotao();
    }
}
