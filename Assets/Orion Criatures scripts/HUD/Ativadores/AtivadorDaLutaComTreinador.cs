using UnityEngine;
using System.Collections;

public class AtivadorDaLutaComTreinador : AtivadorDoBotaoConversa
{
    [SerializeField] private NpcLutaContraTreinador npcLuta;

    // Use this for initialization
    new void Start()
    {
        npc = npcLuta;
        base.Start();
    }

    public NpcLutaContraTreinador NPC_Luta
    {
        get { return npcLuta; }
    }
}
