using UnityEngine;
using System.Collections;

public class AtivadorDaConversaComOpcoes : AtivadorDoBotaoConversa
{
    [SerializeField] private NPCcomOpcoesDeConversa npcOpcoes;

    new void Start()
    {
        npc = npcOpcoes;
        base.Start();
    }

    public override void FuncaoDoBotao()
    {
        npcOpcoes.IniciarOpcoes();
        base.FuncaoDoBotao();
    }
}
