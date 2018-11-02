using UnityEngine;
using System.Collections;

public class AtivadorDaConversaComDerek : AtivadorDoBotaoConversa
{
    [SerializeField] private NpcLutaContraTreinador npcLuta;

    new void Start()
    {
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            KeyVar keys = GameController.g.MyKeys;
            if (keys.VerificaAutoShift(KeyShift.conversouPrimeiroComIan)&&!keys.VerificaAutoShift(KeyShift.venceuDerekPrimeiraVez))
            {
                npc = npcLuta;
            }

            base.Start();
        }
    }

    public override void FuncaoDoBotao()
    {
        KeyVar keys = GameController.g.MyKeys;

        if (keys.VerificaAutoShift(KeyShift.venceuDerekPrimeiraVez))
        {
            npc = new NPCdeConversa();
            base.Start();
            npc.MudaChaveDaConversa(ChaveDeTexto.DerekDerrotado);
        }
        else
            Start();

        keys.MudaShift(KeyShift.conversouPrimeiroComDerek, true);
        base.FuncaoDoBotao();
    }
}
